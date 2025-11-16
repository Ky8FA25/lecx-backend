using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LecX.Application.Common.Utils
{
    public static class CourseCompletionHelper
    {
        /// <summary>
        /// Kiểm tra progress và tính điểm trung bình cả Assignment và Test.
        /// Trả về true nếu học viên pass (đạt >= 5) và không có điểm 0, false nếu không.
        /// </summary>
        public static async Task<bool> HasStudentPassedCourseAsync(
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
