using FastEndpoints;
using LecX.Application.Features.Users.GetUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
namespace LecX.WebApi.Endpoints.Users.GetUserProfile
{
    [Authorize]
    public class GetUserProfileEndPoint(ISender sender, IHttpContextAccessor httpContextAccessor) : EndpointWithoutRequest<GetUserProfileResponse>
    {
        public override void Configure()
        {
            Get("/api/profile/me");
            Summary(s => s.Summary = "Get current user profile");
            Description(d => d.WithTags("Profile"));

        }
        public override async Task HandleAsync(CancellationToken ct)
        {
            // ✅ Lấy ClaimsPrincipal từ HttpContext
            var userPrincipal = httpContextAccessor.HttpContext?.User;

            if (userPrincipal == null || !userPrincipal.Identity?.IsAuthenticated == true)
            {
                await SendForbiddenAsync(ct);
                return;
            }

            

            // ✅ Gửi request đến MediatR handler
            var result = await sender.Send(new GetUserProfileRequest
            {
                ClaimsPrincipal = userPrincipal
            }, ct);

            await SendAsync(result);
        } 
    }
}
