using FitCompete.BlazorWasm;
using FitCompete.BlazorWasm.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7142") 
});

// Rejestracja naszego serwisu HTTP
builder.Services.AddScoped<IChallengeHttpService, ChallengeHttpService>();

await builder.Build().RunAsync();