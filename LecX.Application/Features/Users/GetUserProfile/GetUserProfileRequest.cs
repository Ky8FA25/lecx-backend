using System.Security.Claims;
using MediatR;

namespace LecX.Application.Features.Users.GetUserProfile
{
    public class GetUserProfileRequest : IRequest<GetUserProfileResponse>
    {
        public ClaimsPrincipal ClaimsPrincipal { get; set; }
    }
}