namespace LecX.Domain.Entities
{
    public class Notification
    {
        public int NotificationID { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public virtual User User { get; set; }
    }
}