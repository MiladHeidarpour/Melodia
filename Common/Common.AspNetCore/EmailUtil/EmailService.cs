using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace Common.AspNetCore.EmailUtil;

public class EmailService:IEmailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _senderEmail;
    private readonly string _senderPassword;

    public EmailService(IConfiguration configuration)
    {
        _smtpServer = configuration["EmailSettings:SmtpServer"];
        _smtpPort = int.Parse(configuration["EmailSettings:SmtpPort"]);
        _senderEmail = configuration["EmailSettings:SenderEmail"];
        _senderPassword = configuration["EmailSettings:SenderPassword"];
    }

    public async Task SendEmail(string recipientEmail, string subject, string message)
    {
        var emailMessage = new MimeMessage();
        emailMessage.From.Add(new MailboxAddress("ملودیا", _senderEmail));
        emailMessage.To.Add(new MailboxAddress("سیشسیشسیشسیشسی", recipientEmail));
        emailMessage.Subject = subject;

        var body = new TextPart("plain")
        {
            Text = message
        };

        emailMessage.Body = body;

        try
        {
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync(_smtpServer, _smtpPort, false);
                await client.AuthenticateAsync(_senderEmail, _senderPassword);
                await client.SendAsync(emailMessage);
                await client.DisconnectAsync(true);
            }
        }
        catch (Exception ex)
        {
            //
        }
    }
}