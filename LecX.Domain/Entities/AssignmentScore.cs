namespace LecX.Domain.Entities
{
	public class AssignmentScore
	{
        public int AssignmentScoreId { get; set; }
        public string StudentId { get; set; }
        public int AssignmentId { get; set; }
        public double Score { get; set; }
        public virtual User Student { get; set; }
        public virtual Assignment Assignment { get; set; }
    }
}