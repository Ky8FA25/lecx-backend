using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Certificates.DeleteCertificate
{
    public class DeleteCertificateRequest : IRequest<Unit>
    {
        [JsonIgnore]
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}