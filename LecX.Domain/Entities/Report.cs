namespace LecX.Domain.Entities
{
    public class Report
    {
        public int ReportID { get; set; }
        public string UserID { get; set; }
        public string Subject { get; set; }
        public string Comment { get; set; }

        public DateTime FeedbackDate { get; set; } = DateTime.Now;
        public virtual User User { get; set; }
    }
}