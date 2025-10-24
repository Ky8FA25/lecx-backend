using LecX.Domain.Enums;
using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.StudentCourses.GetCoursesFilteredByStudent
{
    public sealed class GetCoursesFilteredByStudentRequest () : IRequest<GetCoursesFilteredByStudentResponse>
    {
        [JsonIgnore]
        public string StudentId { get; set; } 
        public string? Keyword { get; set; }
        public int? CategoryId { get; set; }
        public CertificateStatus? CertificateStatus { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}