namespace SozonovBackend.Services.MailSendService
{
    public class SenderMailSettings
    {
        public string RecipientMail { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
