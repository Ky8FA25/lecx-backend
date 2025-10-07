namespace ct.backend.Features.Auth.Common;

public interface IEmailTemplateService
{
    Task<string> BuildEmailBodyAsync(string templateFileName, string confirmationUrl, string email);
}

public class EmailTemplateService : IEmailTemplateService
{
    private readonly IWebHostEnvironment _env;
    public EmailTemplateService(IWebHostEnvironment env) => _env = env;

    public async Task<string> BuildEmailBodyAsync(string templateFileName, string confirmationUrl, string email)
    {
        var path = Path.Combine(_env.ContentRootPath, "Common", "Templates", templateFileName);
        var html = await File.ReadAllTextAsync(path);
        return html
            .Replace("{{ .ConfirmationURL }}", confirmationUrl)
            .Replace("{{ .Email }}", email);
    }
}
