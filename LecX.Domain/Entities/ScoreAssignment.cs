namespace LecX.Domain.Entities
{
	public class ScoreAssignment
	{
        public int ScoreAssignmentID { get; set; }
        public string StudentID { get; set; }
        public int AssignmentID { get; set; }
        public double Score { get; set; }
        public virtual User Student { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}