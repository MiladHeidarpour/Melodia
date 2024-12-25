namespace Common.AspNetCore.EmailUtil;

public interface IEmailService
{
    Task SendEmail(string recipientEmail, string subject, string message);
}