using LecX.Domain.Enums;
using MediatR;

namespace LecX.Application.Features.StudentCourses.GetStudentsFilteredByCourse
{
    public class GetStudentsFilteredByCourseRequest : IRequest<GetStudentsFilteredByCourseResponse>
    {
        public int CourseId { get; set; }
        public string? Keyword { get; set; }
        public CertificateStatus? CertificateStatus { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}