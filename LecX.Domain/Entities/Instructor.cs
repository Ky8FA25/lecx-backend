namespace LecX.Domain.Entities
{
    public class Instructor
    {
        public string InstructorID { get; set; }
        public string Bio { get; set; }
        public virtual User User { get; set; }
    }
}