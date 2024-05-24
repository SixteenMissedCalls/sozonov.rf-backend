using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using Org.BouncyCastle.Asn1.Mozilla;
using SozonovBackend.ConfigurationManager;
using System.Net.Sockets;

namespace SozonovBackend.Services.MailSendService
{
    public class MailService : IMailService
    {
        private readonly ILogger _logger;

        public MailService(ILogger<MailService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SendMailSettings(SenderMailSettings senderSettings, MailSettings mailSettings)
        {
            try
            {
                using var emailMessage = new MimeMessage();

                emailMessage.From.Add(new MailboxAddress(mailSettings.SenderName, mailSettings.SenderMail));
                emailMessage.To.Add(new MailboxAddress("", senderSettings.RecipientMail));
                emailMessage.Subject = senderSettings.Subject;

                var body = new BodyBuilder();
                body.HtmlBody = senderSettings.Body;
                emailMessage.Body = body.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    try
                    {
                        await client.ConnectAsync(mailSettings.Smtp, mailSettings.Port, SecureSocketOptions.SslOnConnect);
                        await client.AuthenticateAsync(mailSettings.SenderMail, mailSettings.Password);
                        await client.SendAsync(emailMessage);
                    }
                    catch (AuthenticationException ex)
                    {
                        _logger.LogError($"Ошибка аутентификации: {ex.Message}");
                        return false;
                    }
                    catch (SmtpCommandException ex)
                    {
                        _logger.LogError($"Ошибка SMTP команды: {ex.Message}");
                        return false;
                    }
                    catch (SmtpProtocolException ex)
                    {
                        _logger.LogError($"Ошибка протокола SMTP: {ex.Message}");
                        return false;
                    }
                    catch (SocketException ex)
                    {
                        _logger.LogError($"Ошибка сокета: {ex.Message}");
                        return false;
                    }
                    catch (InvalidOperationException ex)
                    {
                        _logger.LogError($"Неверное состояние: {ex.Message}");
                        return false;
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Произошла ошибка: {ex.Message}");
                        return false;
                    }
                    finally
                    {
                        await client.DisconnectAsync(true);
                        _logger.LogInformation("Подключение к почтовому сервису закрыто");
                    }
                }
                return true;
            }
            catch (IOException ex)
            {
                _logger.LogError($"Ошибка ввода-вывода: {ex.Message}");
                return false;
            }
            catch (ObjectDisposedException ex)
            {
                _logger.LogError($"Объект был удален: {ex.Message}");
                return false;
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogError($"Неверное состояние: {ex.Message}");
                return false;
            }
            catch (ArgumentException ex)
            {
                _logger.LogError($"Недопустимый аргумент: {ex.Message}");
                return false;
            }
            catch (FormatException ex)
            {
                _logger.LogError($"Ошибка формата: {ex.Message}");
                return false;
            }
            catch (AggregateException ex)
            {
                _logger.LogError($"Агрегированная ошибка: {ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Произошла ошибка: {ex.Message}");
                return false;
            }
        }
    }
}
