using AutoMapper;
using LecX.Application.Abstractions.InternalServices.Certificates;
using LecX.Application.Features.Certificates.Common;
using MediatR;

namespace LecX.Application.Features.Certificates.CreateCertificate
{
    public class CreateCertificateHandler(
        IMapper mapper,
        ICertificateIssuanceService certificateIssuanceService
        ) : IRequestHandler<CreateCertificateRequest, CreateCertificateResponse>
    {
        public async Task<CreateCertificateResponse> Handle(CreateCertificateRequest req, CancellationToken cancellationToken)
        {
            var certificate = await certificateIssuanceService.IssueAsync(req.StudentId, req.CourseId);
            var dto = mapper.Map<CertificateDto>(certificate); 
            return new () { Data = dto, Success = true, Message= "Sucess" };
        }
    }
}
