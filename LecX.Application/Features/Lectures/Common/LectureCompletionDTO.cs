namespace LecX.Application.Features.Lectures.Common
{
    public sealed class LectureCompletionDTO
    {
        public int? CompletionId { get; set; }
        public string StudentId { get; set; }
        public int LectureId { get; set; }
        public DateTime? CompletionDate { get; set; } = DateTime.Now;
        public StudentCompletedLectureDTO? Student { get; set; }
    }

    public sealed class StudentCompletedLectureDTO
    {
        public string? Id { get; set; }
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public string? ProfileImagePath { get; set; }
    }
}
