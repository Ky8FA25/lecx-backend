using AutoMapper;
using LecX.Application.Features.Auth.Login;
using LecX.Application.Features.Auth.Refresh;
using LecX.Application.Features.Auth.Register;
using LecX.Application.Features.Auth.ResetPassword;
using LecX.WebApi.Endpoints.Auth.Login;
using LecX.WebApi.Endpoints.Auth.Refresh;
using LecX.WebApi.Endpoints.Auth.Register;
using LecX.WebApi.Endpoints.Auth.ResetPassword;

namespace LecX.WebApi.Endpoints.Auth.Common
{
    public class AuthMappingProfile : Profile
    {
        public AuthMappingProfile()
        {
            //Login
            CreateMap<LoginRequest, LoginCommand>().ReverseMap();
            CreateMap<LoginResult, LoginResponse>();

            //Refresh token
            CreateMap<RefreshResult, RefreshResponse>();

            //Register 
            CreateMap<RegisterRequest, RegisterCommand>().ReverseMap();
            CreateMap<RegisterResult, RegisterResponse>();

            //Register 
            CreateMap<RegisterRequest, RegisterCommand>().ReverseMap();
            CreateMap<RegisterResult, RegisterResponse>();

            //Password reset
            CreateMap<ResetPasswordRequest, ResetPasswordCommand>().ReverseMap();
        }
    }
}
