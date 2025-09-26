namespace LecX.Domain.Entities
{
    public class RequestTranfer
    {
        public int TranferID { get; set; }
        public string StudentID { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
        public string FullName { get; set; }
        public double MoneyNumber { get; set; }
        public string Status { get; set; }
        
        public virtual User Student { get; set; }
        public DateTime CreateAt { get; set; }
    }
}