using AutoMapper;
using LecX.Domain.Entities;

namespace LecX.Application.Features.Certificates.Common
{
    public class CertificateMappinProfile : Profile
    {
        public CertificateMappinProfile()
        {
            CreateMap<Certificate, CertificateDto>();
        }
    }
}
