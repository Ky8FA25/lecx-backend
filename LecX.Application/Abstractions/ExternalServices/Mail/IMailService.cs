namespace LecX.Application.Abstractions.ExternalServices.Mail
{
    public interface IMailService
    {
        Task SendMailAsync(MailContent mailContent);
    }
}
