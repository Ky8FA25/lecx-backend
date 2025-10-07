using LecX.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace LecX.Application.Features.Auth.Register;

public sealed class RegisterHandler(
    UserManager<User> userManager
) : IRequestHandler<RegisterCommand, RegisterResult>
{
    public async Task<RegisterResult> Handle(RegisterCommand request, CancellationToken ct)
    {
        // 1) check tồn tại
        var existed = await userManager.FindByEmailAsync(request.Email);
        if (existed is not null)
            throw new InvalidOperationException("UserName or Email is existed");

        if (request.Password != request.ConfirmPassword)
            throw new InvalidOperationException("The password and the confirmation password are mismatched");

        // 2) tạo user
        var rnd = new Random();
        var user = new User
        {
            UserName = request.Email.Split('@')[0] + $"{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}{rnd.Next(0, 10)}",
            ProfileImagePath = "",
            FirstName = request.FirstName,
            LastName = request.LastName ?? "",
            Email = request.Email,
            EmailConfirmed = false
        };

        var create = await userManager.CreateAsync(user, request.Password);
        if (!create.Succeeded)
            throw new InvalidOperationException(create.Errors.First().Description);

        // 3) gán role mặc định
        await userManager.AddToRoleAsync(user, "Student");

        // 4) tạo token + encode
        var rawToken = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var encoded = WebEncoders.Base64UrlEncode(System.Text.Encoding.UTF8.GetBytes(rawToken));

        return new RegisterResult
        {
            UserId = user.Id,
            Email = user.Email!,
            EncodedEmailConfirmToken = encoded
        };
    }
}
