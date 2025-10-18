using ct.backend.Features.Auth.Common;
using FastEndpoints;
using LecX.Application.Abstractions.ExternalServices.Mail;
using LecX.Application.Features.Auth.Register;
using LecX.WebApi.Endpoints.Auth.ConfirmEmail;
using MediatR;

namespace LecX.WebApi.Endpoints.Auth.Register
{
    public sealed class RegisterEndpoint(
        ISender sender,
        IMailService mail,
        IEmailTemplateService emailTpl,
        ILogger<RegisterEndpoint> logger,
        AutoMapper.IMapper mapper
        )
        : Endpoint<RegisterRequest, RegisterResponse>
    {
        public override void Configure()
        {
            Post("/api/auth/register");
            AllowAnonymous();
            Description(d => d.WithTags("Auth"));
        }

        public override async Task HandleAsync(RegisterRequest req, CancellationToken ct)
        {
            var cmd = mapper.Map<RegisterCommand>(req);
            var result = await sender.Send(cmd, ct);

            var reqHttp = HttpContext.Request;
            var scheme = reqHttp.Scheme;
            var host = reqHttp.Host.Value;
            var basePath = reqHttp.PathBase.Value;

            var confirmUrl = $"{reqHttp.Scheme}://{host}{basePath}{ConfirmEmailEndpoint.Route}"
                           + $"?userId={result.UserId}"
                           + $"&token={result.EncodedEmailConfirmToken}"
                           + (string.IsNullOrEmpty(req.ReturnUrl) ? "" : $"&returnUrl={Uri.EscapeDataString(req.ReturnUrl)}");

            // render template + gửi mail
            var body = await emailTpl.BuildEmailBodyAsync(
                templateFileName: "ConfirmEmailTemplate.html",
                confirmationUrl: confirmUrl!,
                email: result.Email);

            await mail.SendMailAsync(new MailContent
            {
                To = result.Email,
                Subject = "Confirm Email LecX",
                Body = body
            });

            logger.LogInformation("User registered, confirmation email sent.");

            await SendOkAsync(new RegisterResponse
            {

                Message = "Registration successful. Please check your email to confirm your account.",
                ConfirmUrl = confirmUrl
            },
            ct);
        }
    }
}