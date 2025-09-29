namespace LecX.Application.Features.Courses.CourseDtos
{
    public sealed record CourseDto(
        int Id,
        string Title,
        decimal Price
    );
}