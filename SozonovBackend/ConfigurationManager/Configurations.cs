
namespace SozonovBackend.ConfigurationManager
{
    public static class Configurations
    {
        public static DataBaseSettings DataBaseSettings { get; private set; } = new DataBaseSettings();
        public static MailSettings MailSettings { get; private set; } = new MailSettings();
        public static AuthenticationService AuthenticationService { get; private set; } = new AuthenticationService();
        public static JwtSettings JwtSettings { get; private set; } = new JwtSettings();

        public static void SetProperties(IConfiguration configuration)
        {
            DataBaseSettings = GetProperties<DataBaseSettings>(configuration, "DataBaseSettings");
            MailSettings = GetProperties<MailSettings>(configuration, "MailSettings");
            AuthenticationService = GetProperties<AuthenticationService>(configuration, "AuthenticationService");
            JwtSettings = GetProperties<JwtSettings>(configuration, "JwtSettings");
        }

        private static T GetProperties<T>(IConfiguration configuration, string section)
        {
            return configuration.GetSection(section).Get<T>() ??
                throw new InvalidOperationException($"Секция: {nameof(T)} не была найдена в конфигеЫ");
        }
    }
}
