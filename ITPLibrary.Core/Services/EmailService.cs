using System.Net;
using System.Net.Mail;
using ITPLibrary.Core.Services.IServices;

namespace ITPLibrary.Core.Services
{
    public class EmailService : IEmailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _fromEmail;
        private readonly string _fromPassword;

        public EmailService(string smtpServer, int smtpPort, string fromEmail, string fromPassword)
        {
            _fromPassword = fromPassword;
            _fromEmail = fromEmail;
            _smtpPort = smtpPort;
            _smtpServer = smtpServer;
        }

        public async Task SendEmailAsync(string email, string subject, string body)
        {
            using var client = new SmtpClient(_smtpServer, _smtpPort);
            client.Credentials = new NetworkCredential(_fromEmail, _fromPassword);
            client.EnableSsl = true;

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_fromEmail),
                Subject = subject,
                Body = body,
                IsBodyHtml = true,
            };
            mailMessage.To.Add(email);

            await client.SendMailAsync(mailMessage);
        }
    }
}
