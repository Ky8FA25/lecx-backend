using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Net.payOS.Types;
using System.Security.Claims;

namespace LecX.Application.Features.Payment.CreatePayment
{
    public sealed class CreatePaymentHandler(
        IAppDbContext db,
        UserManager<User> userManager,
        PayOS payOS,
        IHttpContextAccessor httpContext
    ) : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            // Kiểm tra khóa học tồn tại
            var course = await db.Set<Course>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken)
                ?? throw new KeyNotFoundException($"Course with ID {request.CourseId} not found");

            // Lấy user ID từ JWT claim
            var userId = httpContext.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                throw new UnauthorizedAccessException("User not logged in");

            // Tìm user hiện tại
            var currentUser = await userManager.FindByIdAsync(userId)
                ?? throw new KeyNotFoundException("User not found");

            // Kiểm tra xem học viên đã ghi danh chưa
            var alreadyEnrolled = await db.Set<StudentCourse>()
                .AnyAsync(sc => sc.StudentId == currentUser.Id && sc.CourseId == course.CourseId, cancellationToken);

            if (alreadyEnrolled)
            {
                return new CreatePaymentResponse(
                    CheckoutUrl: null,
                    OrderCode: 0,
                    Message: "You have already enrolled in this course."
                );
            }

            // Tạo mã đơn hàng duy nhất
            var orderCode = int.Parse($"{DateTime.UtcNow:HHmmss}{new Random().Next(100, 999)}");

            // Tạo ItemData và PaymentData
            var item = new ItemData(course.Title, 1, (int)course.Price);
            var requestInfo = httpContext.HttpContext!.Request;
            var baseUrl = $"{requestInfo.Scheme}://{requestInfo.Host}";
            var returnUrl = $"{baseUrl}/api/payments/success?orderCode={orderCode}";
            var cancelUrl = $"{baseUrl}/api/payments/cancel?orderCode={orderCode}";

            var paymentData = new PaymentData(
                orderCode,
                (int)course.Price,
                $"{course.CourseCode}-{orderCode}",
                new List<ItemData> { item },
                cancelUrl,
                returnUrl,
                currentUser.FirstName,
                currentUser.Email
            );

            // Gọi PayOS API
            var createPayment = await payOS.createPaymentLink(paymentData);

            // Lưu thông tin Payment vào DB
            var payment = new LecX.Domain.Entities.Payment
            {
                CourseId = course.CourseId,
                StudentId = currentUser.Id,
                Amount = course.Price,
                Status = PaymentStatus.Pending,
                PaymentDate = DateTime.Now,
                OrderCode = orderCode,
                CheckoutUrl = createPayment.checkoutUrl,
                GatewayTransactionId = createPayment.paymentLinkId,
                Description = paymentData.description
            };

            db.Set<LecX.Domain.Entities.Payment>().Add(payment);
            await db.SaveChangesAsync(cancellationToken);

            // Trả response
            return new CreatePaymentResponse(createPayment.checkoutUrl, orderCode, "Payment link created successfully");
        }
    }
}
