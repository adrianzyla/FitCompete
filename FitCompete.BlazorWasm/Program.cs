using FitCompete.BlazorWasm;
using FitCompete.BlazorWasm.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http; // Potrzebne dla HttpClient

// W Blazor WebAssembly u�ywamy WebAssemblyHostBuilder zamiast WebApplicationBuilder.
var builder = WebAssemblyHostBuilder.CreateDefault(args);

// 1. Rejestracja us�ug z biblioteki MudBlazor.
builder.Services.AddMudServices();

// 2. Rejestracja naszego typowanego serwisu HTTP.
//    Dzi�ki temu b�dziemy mogli wstrzykiwa� IChallengeHttpService w komponentach.
builder.Services.AddScoped<IChallengeHttpService, ChallengeHttpService>();

builder.Services.AddScoped(sp =>
{
    var loggerFactory = sp.GetRequiredService<ILoggerFactory>();

    var httpClient = new HttpClient
    {
        // UPEWNIJ SI�, �E TEN PORT JEST POPRAWNY!
        // Powinien to by� port HTTPS z pliku launchSettings.json projektu FitCompete.Api
        BaseAddress = new Uri("https://localhost:7142")
    };

    return httpClient;
});


await builder.Build().RunAsync();