using AutoMapper;
using LecX.Application.Features.Payment.PaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LecX.Application.Features.Payment.Common
{
    public sealed class PaymentMappingProfile : Profile
    {
        public PaymentMappingProfile()
        {
            CreateMap<LecX.Domain.Entities.Payment, PaymentDto>()
                .ForMember(dest => dest.PaymentId, opt => opt.MapFrom(src => src.PaymentId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.CourseTitle, opt => opt.MapFrom(src => src.Course != null ? src.Course.Title : "(Unknown Course)"))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status.ToString()))
                .ForMember(dest => dest.PaymentDate, opt => opt.MapFrom(src => src.PaymentDate))
                .ForMember(dest => dest.OrderCode, opt => opt.MapFrom(src => src.OrderCode))
                .ForMember(dest => dest.CheckoutUrl, opt => opt.MapFrom(src => src.CheckoutUrl))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Description));
        }
    }
}
