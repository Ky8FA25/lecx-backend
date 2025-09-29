using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class Refund
    {
        public int RefundId { get; set; }
        public string StudentId { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string FullName { get; set; }
        public decimal Amount { get; set; }
        public string Status { get; set; }
        public RefundStatus RefundStatus { get; set; }

        public virtual User Student { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.Now;
    }
}