using FastEndpoints;
using LecX.Application.Features.Categories.GetAllCategories;
using LecX.Application.Features.Users.UserChangePassword;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;

namespace LecX.WebApi.Endpoints.Users.UserChangePassword
{
    public class UserChangePasswordEndPoint(ISender sender, IHttpContextAccessor httpContextAccessor) : Endpoint<UserChangePasswordRequest, UserChangePasswordResponse>
    {
        public override void Configure()
        {
            Post("/api/user/change-password");
            Summary(s => s.Summary = "User Change password");
            Description(d => d.WithTags("User"));
        }

        public override async Task HandleAsync(UserChangePasswordRequest req, CancellationToken ct) => await SendAsync(await sender.Send(req, ct), cancellation: ct);
    }
}
