using ct.backend.Features.Auth.Common;
using FastEndpoints;
using LecX.Application.Abstractions.ExternalServices.Mail;
using LecX.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.WebUtilities;
using System.Text;

namespace LecX.WebApi.Endpoints.Auth.ResetPassword
{
    public sealed class ForgotPasswordEndpoint(
        UserManager<User> userManager,
        IEmailTemplateService emailTpl,
        IConfiguration config,
        IMailService mail)
      : Endpoint<ForgotPasswordRequest>
    {
        public override void Configure()
        {
            Post("/api/auth/forgot-password");
            AllowAnonymous();
            Description(d => d.WithTags("Auth"));
        }

        public override async Task HandleAsync(ForgotPasswordRequest req, CancellationToken ct)
        {
            var user = await userManager.FindByEmailAsync(req.Email);
            if (user == null || !(await userManager.IsEmailConfirmedAsync(user)))
            {
                await SendOkAsync(new { message = "If your email is registered, you will receive a password reset email." }, ct);
                return;
            }

            //var returnUrl = HttpContext.Request.Query["returnUrl"].FirstOrDefault() ?? "/";
            var rawToken = await userManager.GeneratePasswordResetTokenAsync(user);
            var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(rawToken));

            var feBaseUrl = config.GetValue<string>("Frontend:BaseUrl") ?? "";
            var feResetPath = config.GetValue<string>("Frontend:ResetPath") ?? "/reset-password";

            // chuẩn hoá
            feBaseUrl = feBaseUrl.TrimEnd('/');
            feResetPath = feResetPath.StartsWith("/") ? feResetPath : "/" + feResetPath;

            var resetUrl = $"{feBaseUrl}{feResetPath}"
                           + $"?userId={user.Id}"
                           + $"&token={encodedToken}";
            //+ (string.IsNullOrEmpty(returnUrl) ? "" : $"&returnUrl={Uri.EscapeDataString(returnUrl)}");

            var emailBody = await emailTpl.BuildEmailBodyAsync(
                          templateFileName: "ResetPasswordTemplate.html",
                          confirmationUrl: resetUrl!,
                          email: user.Email
                      );

            // 4. Gửi mail
            await mail.SendMailAsync(new MailContent
            {
                To = user.Email,
                Subject = "Reset your LecX password",
                Body = emailBody
            });

            await SendOkAsync(new { message = "If your email is registered, you will receive a password reset email." }, ct);
        }
    }
}
