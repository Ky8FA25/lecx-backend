using MediatR;
using System.Text.Json.Serialization;

namespace LecX.Application.Features.Certificates.CreateCertificate
{
    public sealed class CreateCertificateRequest : IRequest<CreateCertificateResponse>
    {
        [JsonIgnore]
        public string StudentId { get; set; }
        public int CourseId { get; set; }
    }
}