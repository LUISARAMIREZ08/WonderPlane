using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WonderPlane.Client;
using MudBlazor.Services;

using WonderPlane.Client.Servicios;
using Microsoft.AspNetCore.Components.Authorization;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7056") });

// Authentication and Authorization
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

// Third-party services (MudBlazor)
builder.Services.AddMudServices();

// Application-specific services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ImageUploadService>();
builder.Services.AddScoped<CountryService>();

await builder.Build().RunAsync();
