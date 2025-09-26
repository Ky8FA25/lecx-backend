namespace LecX.Domain.Entities
{
    public class InstructorConfirmation
    {
        public int ConfirmationId { get; set; }
        public string UserId { get; set; }
        public string FileName { get; set; }
        public string Certificatelink { get; set; }
        public virtual User User { get; set; }
        public DateTime SendDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}