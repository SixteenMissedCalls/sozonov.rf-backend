
namespace SozonovBackend.ConfigurationManager
{
    public static class Configurations
    {
        public static DataBaseSettings DataBaseSettings { get; private set; } = new DataBaseSettings();

        public static void SetProperties(IConfiguration configuration)
        {
            DataBaseSettings = GetProperties<DataBaseSettings>(configuration, "DataBaseSettings");
        }

        private static T GetProperties<T>(IConfiguration configuration, string section)
        {
            return configuration.GetSection(section).Get<T>() ??
                throw new InvalidOperationException($"Секция: {nameof(T)} не была найдена в конфигеЫ");
        }
    }
}
