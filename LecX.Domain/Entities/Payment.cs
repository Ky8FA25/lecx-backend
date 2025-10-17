using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; } 
        public int CourseId { get; set; } 
        public string StudentId { get; set; } 
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        // 🔹 Thông tin từ PayOS
        public int OrderCode { get; set; }
        public string? GatewayTransactionId { get; set; }
        public string? CheckoutUrl { get; set; }
        public string? Description { get; set; }

        public virtual Course Course { get; set; }
        public virtual User Student { get; set; }
    }
}