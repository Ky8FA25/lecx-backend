using LecX.Application.Abstractions.Persistence;
using LecX.Application.Features.Payment.Common;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Net.payOS;

namespace LecX.Application.Features.Payment.PaymentSuccess
{
    public sealed class PaymentSuccessCommandHandler : IRequestHandler<PaymentSuccessCommand, PaymentResult>
    {
        private readonly PayOS _payOs;
        private readonly IAppDbContext _db;

        public PaymentSuccessCommandHandler(PayOS payOs, IAppDbContext db)
        {
            _payOs = payOs;
            _db = db;
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
