using LecX.Application.Abstractions.Persistence;
using LecX.Domain.Entities;
using LecX.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Net.payOS;
using Net.payOS.Types;

namespace LecX.Application.Features.Payment.CreatePayment
{
    public sealed class CreatePaymentHandler(
        IAppDbContext db,
        UserManager<User> userManager,
        PayOS payOS,
        IHttpContextAccessor httpContext)
        : IRequestHandler<CreatePaymentRequest, CreatePaymentResponse>
    {
        public async Task<CreatePaymentResponse> Handle(CreatePaymentRequest request, CancellationToken cancellationToken)
        {
            var course = await db.Set<Course>()
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.CourseId == request.CourseId, cancellationToken)
            ?? throw new Exception($"Course with ID {request.CourseId} not found");

            //Lấy user ID từ JWT claim
            var userId = httpContext.HttpContext?.User?.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier);            

            if (string.IsNullOrEmpty(userId.Value))
                throw new UnauthorizedAccessException("User not logged in");

            var currentUser = await userManager.FindByIdAsync(userId.Value)
                ?? throw new Exception("User not found");

            // Kiểm tra xem học viên đã ghi danh chưa
            var alreadyEnrolled = await db.Set<StudentCourse>()
                .AnyAsync(sc => sc.StudentId == currentUser.Id && sc.CourseId == course.CourseId, cancellationToken);

            if (alreadyEnrolled)
            {
                // Có thể trả về URL trang khóa học hoặc báo lỗi tuỳ logic
                return new CreatePaymentResponse(
                    CheckoutUrl: null,
                    OrderCode: 0,
                    Message: "You have already enrolled in this course."
                );
            }

            // Tạo mã đơn hàng duy nhất
            var orderCode = int.Parse($"{DateTime.UtcNow:HHmmss}{new Random().Next(100, 999)}");
            // Tạo đối tượng ItemData
            var item = new ItemData(course.Title, 1, (int)course.Price);
            // Tạo URL trả về và hủy
            var requestInfo = httpContext.HttpContext!.Request;
            var baseUrl = $"{requestInfo.Scheme}://{requestInfo.Host}";
            var returnUrl = $"{baseUrl}/api/payments/success?orderCode={orderCode}";
            var cancelUrl = $"{baseUrl}/api/payments/cancel?orderCode={orderCode}";
            // Tạo đối tượng PaymentData
            var paymentData = new PaymentData(
               orderCode,
               (int)course.Price,
               $"{course.Title}-{orderCode}",
               new List<ItemData> { item },
               cancelUrl,
               returnUrl,
               currentUser.FirstName,
               currentUser.Email
           );
            // Tạo liên kết thanh toán
            var createPayment = await payOS.createPaymentLink(paymentData);

            // Lưu DB
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

            return new CreatePaymentResponse(createPayment.checkoutUrl, orderCode, "Payment link created successfully");
        }
    }
}
