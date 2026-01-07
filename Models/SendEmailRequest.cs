namespace WebApp_EMail.Models
{
    public class SendEmailRequest
    {
        public string Token { get; set; }
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
