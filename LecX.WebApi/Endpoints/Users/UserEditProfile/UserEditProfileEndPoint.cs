using FastEndpoints;
using LecX.Application.Features.Users.GetUserProfile;
using LecX.Application.Features.Users.UserEditProfile;
using MediatR;

namespace LecX.WebApi.Endpoints.Users.UserEditProfile
{
    public class UserEditProfileEndPoint(ISender sender) : Endpoint<UserEditProfileRequest, UserEditProfileResponse>
    {
        public override void Configure()
        {
            Put("/api/user/profile/edit"); 
            Summary(s =>
            {
                s.Summary = "Cập nhật thông tin hồ sơ người dùng";
                s.Description = "Cho phép người dùng đã đăng nhập cập nhật thông tin cá nhân của họ như tên, địa chỉ, ngày sinh, giới tính và ảnh đại diện.";
            });
        }

        public override async Task HandleAsync(UserEditProfileRequest req, CancellationToken ct)
        {
            var result = await sender.Send(req, ct);

            if (!result.Success)
            {
                await SendAsync(result, StatusCodes.Status400BadRequest, ct);
                return;
            }

            await SendAsync(result, StatusCodes.Status200OK, ct);
        }
    }
}
