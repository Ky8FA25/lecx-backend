namespace LecX.WebApi.Endpoints.Assignments.Common
{
    public class AssignmentDto
    {
        public int AssignmentId { get; set; }
        public string Title { get; set; }
        public int CourseId { get; set; }
        public DateTime DueDate { get; set; }
    }
}
