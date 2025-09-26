namespace LecX.Domain.Entities
{
    public class Instructor
    {
        public string InstructorId { get; set; }
        public string Bio { get; set; }
        public virtual User User { get; set; }
    }
}