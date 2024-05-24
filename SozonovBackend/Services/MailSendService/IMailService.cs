using SozonovBackend.ConfigurationManager;

namespace SozonovBackend.Services.MailSendService
{
    public interface IMailService
    {
        Task<bool> SendMailSettings(SenderMailSettings _settings, MailSettings mailSettings);

    }
}
