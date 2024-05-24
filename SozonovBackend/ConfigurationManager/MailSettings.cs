namespace SozonovBackend.ConfigurationManager
{
    public class MailSettings
    {
    public string Smtp {  get; set; } = string.Empty;
    public int Port { get; set; }
    public string SenderMail { get; set; } = string.Empty;
    public string Password {  get; set; } = string.Empty;
    public string SenderName {  get; set; } = string.Empty;
    public bool UseTls {  get; set; }
    }
}
