using AutoMapper;
using LecX.Application.Features.Auth.Common;
using LecX.Application.Features.Auth.Login;
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
            CreateMap<LoginRequest, LoginCommand>();
            CreateMap<AuthResult, LoginResponse>();

            //Refresh token
            CreateMap<AuthResult, RefreshResponse>();

            //Register 
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<RegisterResult, RegisterResponse>();

            //Register 
            CreateMap<RegisterRequest, RegisterCommand>();
            CreateMap<RegisterResult, RegisterResponse>();

            //Password reset
            CreateMap<ResetPasswordRequest, ResetPasswordCommand>();
        }
    }
}
