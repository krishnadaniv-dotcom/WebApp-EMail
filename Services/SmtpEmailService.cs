using System.Net.Mail;
using System.Net;

namespace WebApp_EMail.Services
{
    public class SmtpEmailService : EmailService
    {
        private readonly string _smtpServer = "smtp.example.com";
        private readonly string _smtpUser = "your-email@example.com";
        private readonly string _smtpPass = "your-password";
        private readonly int _smtpPort = 587;

        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            try
            {
                using (var client = new SmtpClient(_smtpServer, _smtpPort))
                {
                    client.Credentials = new NetworkCredential(_smtpUser, _smtpPass);
                    client.EnableSsl = true;

                    var mailMessage = new MailMessage
                    {
                        From = new MailAddress(_smtpUser),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true
                    };
                    mailMessage.To.Add(to);

                    await client.SendMailAsync(mailMessage);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
