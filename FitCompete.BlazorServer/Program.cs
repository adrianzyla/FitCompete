using FitCompete.Api.Controllers; // Potrzebne do BaseApiController
using FitCompete.Application.Common.Mappings;
using FitCompete.Application.Services;
using FitCompete.BlazorServer.Services;
using FitCompete.BlazorServer.Components;
using FitCompete.Domain.Interfaces;
using FitCompete.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

// 1. Konfiguracja Serilog dla Blazor Server
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("logs/blazor-server-log-.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Information)
    .WriteTo.File("logs/blazor-server-error-log-.txt",
        rollingInterval: RollingInterval.Day,
        restrictedToMinimumLevel: LogEventLevel.Error)
    .CreateLogger();

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddMudServices();

builder.Services.AddHttpClient<IChallengeHttpService, ChallengeHttpService>(client =>
{
    // Tutaj podajemy adres naszego API.
    // WeŸ go z w³aœciwoœci projektu FitCompete.Api -> Debug -> Ogólne -> Adres URL aplikacji
    client.BaseAddress = new Uri("https://localhost:7142"); // <-- ZMIEÑ PORT, JEŒLI JEST INNY!
});


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"),
    b => b.MigrationsAssembly("FitCompete.Infrastructure")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(FitCompete.Infrastructure.Persistence.Repositories.GenericRepository<>));

// Rejestracja serwisów aplikacyjnych
builder.Services.AddScoped<IChallengeService, ChallengeService>();
builder.Services.AddScoped<IChallengeAttemptService, ChallengeAttemptService>();

// Rejestracja AutoMappera
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.UseSerilogRequestLogging();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();