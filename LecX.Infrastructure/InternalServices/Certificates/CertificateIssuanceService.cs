using LecX.Application.Abstractions.ExternalServices.GoogleStorage;
using LecX.Application.Abstractions.ExternalServices.Mail;
using LecX.Application.Abstractions.ExternalServices.Pdf;
using LecX.Application.Abstractions.InternalServices.Certificates;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Common.Utils;
using LecX.Application.Commons.Constants;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace LecX.Infrastructure.InternalServices.Certificates
{
    public class CertificateIssuanceService(
        IAppDbContext db,
        IPdfService pdfService,
        IConfiguration config,
        IGoogleStorageService storage,
        IMailTemplateService mailTpl,
        IMailService mail
        ) : ICertificateIssuanceService
    {
        /// <summary>
        /// Tạo certificate cho đúng 1 student-course nếu progress = 100% và chưa có certificate.
        /// Trả về bản ghi Certificates mới tạo; nếu đã tồn tại thì trả về certificate hiện có.
        /// </summary>
        public async Task<Certificate?> IssueAsync(
        string studentId, int courseId, CancellationToken ct = default)
        {
            bool passed = await HasStudentPassedCourseAsync(
                db, studentId, courseId, ct: ct);
            if (!passed) return null;

            // Lấy student-course kèm đầy đủ nav dùng cho template
            var sc = await db.Set<StudentCourse>()
                .Include(x => x.Student)
                .Include(x => x.Course)
                    .ThenInclude(c => c.Instructor)
                        .ThenInclude(i => i.User)
                .FirstOrDefaultAsync(x => x.StudentId == studentId && x.CourseId == courseId, ct);

            if (sc is null) return null;                 // không tìm thấy

            // Đã có certificate? => trả về luôn
            var existing = await db.Set<Certificate>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.StudentId == studentId && c.CourseId == courseId, ct);
            if (existing is not null) return existing;

            // Dữ liệu hiển thị
            var student = sc.Student;
            var course = sc.Course;
            var instr = course?.Instructor?.User;

            var studentName = $"{student?.FirstName} {student?.LastName}".Trim();
            if (string.IsNullOrWhiteSpace(studentName))
                studentName = "Student";

            var instructorName = $"{instr?.FirstName} {instr?.LastName}".Trim();
            if (string.IsNullOrWhiteSpace(instructorName))
                instructorName = "LecX Instructor";

            // 1) Sinh PDF từ template
            var pdfStream = await pdfService.GenerateCertificateAsync(
                studentName: studentName,
                courseName: course?.Title ?? "Course",
                completionDate: DateTime.Now.ToString("MMMM dd, yyyy"),
                instructorName: instructorName,
                instructorTitle: "Course Instructor",
                verifyUrl: (config["Frontend:BaseUrl"] ?? string.Empty).TrimEnd('/')
            );
            string savedName;
            await using (pdfStream) // <--- Thêm dòng này để đảm bảo giải phóng stream sau khi upload
            {
                if (pdfStream.CanSeek)
                    pdfStream.Position = 0;

                var safeStudent = Slugify($"{studentName}");
                var objectName = $"{GoogleStoragePaths.Private.Certificates}/{safeStudent}-{courseId}-{Guid.NewGuid():N}.pdf";

                savedName = await storage.UploadAsync(
                    pdfStream,
                    objectName,
                    "application/pdf",
                    ct
                );
            }

            await using var tx = await db.BeginTransactionAsync(ct);
            try
            {
                var existed = await db.Set<Certificate>()
                      .FirstOrDefaultAsync(c => c.StudentId == studentId && c.CourseId == courseId, ct);
                if (existed is not null)
                {
                    await tx.RollbackAsync(ct);
                    return existed;
                }

                sc.CertificateStatus = CertificateStatus.Completed;
                sc.CompletionDate = DateTime.Now;

                var certificate = new Certificate
                {
                    StudentId = studentId,
                    CourseId = courseId,
                    CompletionDate = DateTime.Now,
                    CertificateLink = savedName
                };


                await db.Set<Certificate>().AddAsync(certificate, ct);
                await db.SaveChangesAsync(ct);
                await tx.CommitAsync(ct);

                // 4) Gửi email thông báo kèm link certificate
                if (!string.IsNullOrWhiteSpace(student?.Email))
                {
                    var emailBody = await mailTpl.BuildCourseCompletedEmailAsync(
                        studentName: studentName!,
                        course?.Title ?? "Course",
                        certificateUrl: storage.GetSignedReadUrl(savedName, TimeSpan.FromDays(7)),
                        email: student!.Email
                    );

                    await mail.SendMailAsync(new MailContent
                    {
                        To = student.Email,
                        Subject = $"LecX Certificate - {course!.Title}",
                        Body = emailBody
                    });
                }

                return certificate;
            }
            catch (DbUpdateException)
            {
                await tx.RollbackAsync(ct);
                // Nếu có unique index, có thể rơi vào đây khi race → trả cert đã tồn tại
                return await db.Set<Certificate>()
                    .AsNoTracking()
                    .FirstOrDefaultAsync(c => c.StudentId == studentId && c.CourseId == courseId, ct);
            }
            catch
            {
                await tx.RollbackAsync(ct);
                throw;
            }
        }

        // --- helpers ---
        private static string Slugify(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return "user";
            var s = Regex.Replace(input, @"\s+", "-");               // spaces -> dash
            s = Regex.Replace(s, @"[^\w\-\.\p{L}\p{Nd}]+", "");      // remove special (giữ chữ cái số unicode)
            return s.ToLowerInvariant();
        }

        /// <summary>
        /// Kiểm tra progress và tính điểm trung bình cả Assignment và Test.
        /// Trả về true nếu học viên pass (đạt >= 5) và không có điểm 0, false nếu không.
        /// </summary>
        private async Task<bool> HasStudentPassedCourseAsync(
            IAppDbContext db,
            string studentId,
            int courseId,
            ILogger? logger = null,
            CancellationToken ct = default)
        {
            // 1️⃣ Lấy StudentCourse
            var studentCourse = await db.Set<StudentCourse>()
                .AsNoTracking()
                .Where(sc => sc.StudentId == studentId && sc.CourseId == courseId)
                .Select(sc => new { sc.Progress })
                .FirstOrDefaultAsync(ct);

            if (studentCourse == null)
            {
                logger?.LogWarning("StudentCourse not found: StudentId={StudentId}, CourseId={CourseId}", studentId, courseId);
                return false;
            }

            if (studentCourse.Progress < 100)
            {
                logger?.LogInformation("Student progress < 100: StudentId={StudentId}, CourseId={CourseId}, Progress={Progress}",
                    studentId, courseId, studentCourse.Progress);
                return false;
            }

            // 2️⃣ Lấy điểm Assignment chỉ với cột Score
            var assignmentScores = await (
                from a in db.Set<AssignmentScore>()
                join ass in db.Set<Assignment>() on a.AssignmentId equals ass.AssignmentId
                where a.StudentId == studentId && ass.CourseId == courseId
                select a.Score
            ).ToListAsync(ct);

            // 3️⃣ Lấy điểm Test chỉ với cột ScoreValue
            var testScores = await (
                from t in db.Set<TestScore>()
                join test in db.Set<Test>() on t.TestId equals test.TestId
                where t.StudentId == studentId && test.CourseId == courseId
                select t.ScoreValue
            ).ToListAsync(ct);

            // 4️⃣ Gộp tất cả điểm
            var allScores = assignmentScores.Concat(testScores).ToList();

            // 5️⃣ Check nếu có phần tử = 0 thì fail
            if (allScores.Any(s => s == 0))
            {
                logger?.LogInformation("Some scores are 0: StudentId={StudentId}, CourseId={CourseId}", studentId, courseId);
                return false;
            }

            // 6️⃣ Tính trung bình
            double averageScore = allScores.Any() ? allScores.Average() : 0;

            logger?.LogInformation("Average score: StudentId={StudentId}, CourseId={CourseId}, AvgScore={AverageScore}",
                studentId, courseId, averageScore);

            return averageScore >= 5;
        }
    }
}
