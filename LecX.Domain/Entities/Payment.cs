using LecX.Domain.Enums;

namespace LecX.Domain.Entities
{
    public class Payment
    {
        public int PaymentID { get; set; } 
        public int CourseID { get; set; } 
        public string StudentID { get; set; } 
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.Now;
        public PaymentStatus Status { get; set; } = PaymentStatus.Pending;

        public virtual Course Course { get; set; }
        public virtual User Student { get; set; }
    }
}