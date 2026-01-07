namespace WebApp_EMail.Services
{
    // IEmailService.cs
    public interface IEmailService
    {
        Task<bool> SendEmailAsync(string to, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        public async Task<bool> SendEmailAsync(string to, string subject, string body)
        {
            // Logic for sending an email (using SMTP, SendGrid, etc.)
            // For now, let's assume it sends successfully.

            // Simulate sending email
            await Task.Delay(1000);  // Simulate delay
            return true;  // Simulate success
        }
    }
}
