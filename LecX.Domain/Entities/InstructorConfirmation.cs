namespace LecX.Domain.Entities
{
    public class InstructorConfirmation
    {
        public int ConfirmationID { get; set; }
        public string UserID { get; set; }
        public string FileName { get; set; }
        public string Certificatelink { get; set; }
        public virtual User User { get; set; }
        public DateTime SendDate { get; set; }
        public string Description { get; set; }
    }
}