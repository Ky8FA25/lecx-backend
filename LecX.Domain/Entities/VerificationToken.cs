namespace LecX.Domain.Entities
{
    public class VerificationToken
    {
        // Mã OTP 6 chữ số.
        public string OtpCode { get; set; } = string.Empty;

        // Thời điểm mã OTP hết hạn (Unix Timestamp, milliseconds).
        public long ExpiryTime { get; set; }

        // Thời điểm tạo mã (Unix Timestamp, milliseconds).
        public long CreatedAt { get; set; }

        // (Tùy chọn) Số lần nhập sai mã OTP
        public int FailedAttempts { get; set; } = 0;
    }
}
