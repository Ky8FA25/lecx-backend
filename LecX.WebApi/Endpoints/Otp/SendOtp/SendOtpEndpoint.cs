using FastEndpoints;
using LecX.Application.Abstractions.ExternalServices.Firebase;
using LecX.Application.Abstractions.ExternalServices.Mail;
using LecX.Domain.Entities;
using System.Security.Cryptography;

namespace LecX.WebApi.Endpoints.Otp.SendOtp
{
    public class SendOtpEndpoint(
        IFirebaseDbService firebase,
        IMailTemplateService mailTpl,
        IMailService mail
        ) : Endpoint<SendOtpRequest, SendOtpResponse>
    {
        public override void Configure()
        {
            Post("/api/auth/send-otp");
            Summary(s => s.Summary = "Generate otp for mobile app");
            Description(d => d.WithTags("Otp"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(SendOtpRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Email))
            {
                await SendAsync(new SendOtpResponse
                {
                    Success = false,
                    Message = "Email không hợp lệ.",
                }, cancellation: ct);
                return;
            }

            var now = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var path = $"VerificationTokens/{req.Email.Replace(".", "_")}";

            // 1️⃣ Lấy token cũ từ Firebase (nếu có)
            var existingToken = await firebase.GetAsync<VerificationToken>(path);

            // 2️⃣ Giới hạn 1 phút/lần
            if (existingToken != null && now - existingToken.CreatedAt < 60 * 1000)
            {
                await SendAsync(new SendOtpResponse
                {
                    Success = false,
                    Message = "Bạn đã gửi OTP trong vòng 1 phút. Vui lòng thử lại sau.",
                }, cancellation: ct);
                return;
            }

            // 3️⃣ Tạo OTP mới
            var otp = RandomNumberGenerator.GetInt32(100000, 999999).ToString();
            var token = new VerificationToken
            {
                OtpCode = otp,
                CreatedAt = now,
                ExpiryTime = now + 5 * 60 * 1000, // 5 phút
                FailedAttempts = 0,
            };

            await firebase.PutAsync(path, token);

            var emailBody = await mailTpl.BuildSendOtpEmailAsync(
                otpCode: otp,
                email: req.Email
            );

            // 4️⃣ Gửi mail
            await mail.SendMailAsync(new MailContent
            {
                To = req.Email,
                Subject = "Confirm OTP LecX",
                Body = emailBody
            });

            // 5️⃣ Trả về response
            await SendAsync(new SendOtpResponse
            {
                Success = true,
                Message = $"OTP đã được gửi tới {req.Email}.",
            }, cancellation: ct);
        }
    }
}