using FastEndpoints;
using LecX.Application.Abstractions.ExternalServices.Firebase;
using LecX.Domain.Entities;

namespace LecX.WebApi.Endpoints.Otp.VerifyOtp
{
    public class VerifyOtpEndpoint : Endpoint<VerifyOtpRequest, VerifyOtpResponse>
    {
        private readonly IFirebaseDbService _firebase;

        public VerifyOtpEndpoint(IFirebaseDbService firebase)
        {
            _firebase = firebase;
        }

        public override void Configure()
        {
            Post("/api/auth/verify-otp");
            Summary(s => s.Summary = "Verify otp for mobile app");
            Description(d => d.WithTags("Otp"));
            AllowAnonymous();
        }

        public override async Task HandleAsync(VerifyOtpRequest req, CancellationToken ct)
        {
            if (string.IsNullOrWhiteSpace(req.Email) || string.IsNullOrWhiteSpace(req.OtpCode))
            {
                await SendAsync(new VerifyOtpResponse
                {
                    Success = false,
                    Message = "Email không hợp lệ.",
                }, cancellation: ct);
                return;
            }

            var path = $"VerificationTokens/{req.Email.Replace(".", "_")}";
            var tokenFromDb = await _firebase.GetAsync<VerificationToken>(path);

            if (tokenFromDb == null)
            {
                await SendAsync(new VerifyOtpResponse
                {
                    Success = false,
                    Message = "Không tìm thấy mã OTP.",
                }, cancellation: ct);
                return;
            }

            if (tokenFromDb.ExpiryTime < DateTimeOffset.UtcNow.ToUnixTimeMilliseconds())
            {
                await SendAsync(new VerifyOtpResponse
                {
                    Success = false,
                    Message = "OTP đã hết hạn.",
                }, cancellation: ct);
                return;
            }

            if (tokenFromDb.FailedAttempts >= 5)
            {
                await _firebase.DeleteAsync(path);
                await SendAsync(new VerifyOtpResponse
                {
                    Success = false,
                    Message = "Bạn đã nhập OTP sai quá 5 lần. Vui lòng gửi OTP mới.",
                }, cancellation: ct);
                return;
            }

            if (tokenFromDb.OtpCode != req.OtpCode)
            {
                tokenFromDb.FailedAttempts += 1;
                await _firebase.PutAsync(path, tokenFromDb);

                await SendAsync(new VerifyOtpResponse
                {
                    Success = false,
                    Message = "OTP không hợp lệ.",
                }, cancellation: ct);
                return;
            }

            // ✅ Token hợp lệ → xóa token và kích hoạt account
            await _firebase.DeleteAsync(path);

            // TODO: Cập nhật trạng thái user trong DB (active = true)

            await SendAsync(new VerifyOtpResponse
            {
                Success = true,
                Message = "Xác thực thành công!",
            }, cancellation: ct);
        }
    }
}
