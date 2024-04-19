using Serilog;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

#region Общие сервисы для работы системы
builder.Services.AddControllers();

builder.Services.AddLogging(loggingBuilder => 
    {
        loggingBuilder.AddSerilog(logger, dispose: true);
    });
#endregion

var app = builder.Build();

logger.Information("Сервис созонов.рф начал свою работу");

app.MapGet("/", () => "Web-Api сервис дял созонов.рф");

app.Run();
