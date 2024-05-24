using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SozonovBackend.ConfigurationManager;
using SozonovBackend.Repository;
using SozonovBackend.Services.MailSendService;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(config)
    .CreateLogger();

Configurations.SetProperties(config);

// Services 
#region ����� ������� ��� ������ �������
builder.Services.AddControllers();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(optionsJWT =>
{
    optionsJWT.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configurations.JwtSettings.Key)),
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true
    };
});

builder.Services.AddLogging(loggingBuilder => 
    {
        loggingBuilder.AddSerilog(logger, dispose: true);
    });
var connectionString = Configurations.DataBaseSettings.ConnectionString;

builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseNpgsql(connectionString)
);

builder.Services.AddScoped<IRepositoryProposal, RepositoryProposal>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

#endregion

var app = builder.Build();

logger.Information("������ �������.�� ����� ���� ������");

app.MapGet("/", () => "Web-Api ������ ��� �������.��");

app.UseAuthentication();
app.UseAuthorization(); 

app.MapControllers();

app.Run();
