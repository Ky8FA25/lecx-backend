using LecX.Application.Abstractions.ExternalServices.Mail;
using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Payment.Common;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Net.payOS;

namespace LecX.Application.Features.Payment.PaymentSuccess
{
    public sealed class PaymentSuccessCommandHandler : IRequestHandler<PaymentSuccessCommand, PaymentResult>
    {
        private readonly PayOS _payOs;
        private readonly IAppDbContext _db;
        private readonly IMailTemplateService _mailTpl;
        private readonly IMailService _mail;
        private readonly IConfiguration _config;


        public PaymentSuccessCommandHandler(
            PayOS payOs,
            IAppDbContext db,
            IMailTemplateService mailTpl,
            IMailService mail,
            IConfiguration config)
        {
            _payOs = payOs;
            _db = db;
            _mailTpl = mailTpl;
            _mail = mail;
            _config = config;
        }

        public async Task<PaymentResult> Handle(PaymentSuccessCommand request, CancellationToken ct)
        {
            var info = await _payOs.getPaymentLinkInformation(request.OrderCode);

            var payment = await _db.Set<LecX.Domain.Entities.Payment>()
                .FirstOrDefaultAsync(p => p.OrderCode == request.OrderCode, ct)
                ?? throw new Exception("Payment not found");

            if (info.status == "PAID" && payment.Status != PaymentStatus.Completed)
            {
                payment.Status = PaymentStatus.Completed;
                payment.PaymentDate = DateTime.Now;
                await _db.SaveChangesAsync(ct);

                // Enroll student
                var alreadyEnrolled = await _db.Set<StudentCourse>()
                    .AnyAsync(sc => sc.StudentId == payment.StudentId && sc.CourseId == payment.CourseId, ct);

                if (!alreadyEnrolled)
                {
                    _db.Set<StudentCourse>().Add(new StudentCourse
                    {
                        StudentId = payment.StudentId,
                        CourseId = payment.CourseId,
                        EnrollmentDate = DateTime.Now,
                        Progress = 0,
                        CertificateStatus = CertificateStatus.Pending
                    });

                    var course = await _db.Set<Course>()
                        .FirstOrDefaultAsync(c => c.CourseId == payment.CourseId, ct);
                    if (course != null)
                        course.NumberOfStudents += 1;

                    await _db.SaveChangesAsync(ct);

                    var student = await _db.Set<User>()
                        .FirstOrDefaultAsync(u => u.Id == payment.StudentId, ct);

                    if (course != null && student != null)
                    {
                        var feBaseUrl = (_config["Frontend:BaseUrl"] ?? string.Empty).TrimEnd('/');

                        var emailBody = await _mailTpl.BuildWelcomeToCourseEmailAsync(
                            studentName: student.FirstName + student.LastName,
                            courseName: course.Title,
                            courseUrl: $"{feBaseUrl}/course/{course?.CourseId}",
                            email: student?.Email!
                        );

                        await _mail.SendMailAsync(new MailContent
                        {
                            To = student?.Email!,
                            Subject = $"You’re enrolled! Welcome to {course?.Title}",
                            Body = emailBody
                        });
                    }
                }
            }

            var description = info.transactions?.FirstOrDefault()?.description ?? "No description";

            return new PaymentResult
            {
                Message = "Payment success & student enrolled",
                OrderCode = request.OrderCode,
                Status = info.status,
                Description = description
            };
        }
    }
}
