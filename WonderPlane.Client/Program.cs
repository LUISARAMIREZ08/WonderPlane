using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WonderPlane.Client;
using MudBlazor.Services;

using WonderPlane.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;
using CurrieTechnologies.Razor.SweetAlert2;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// HttpClient
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7056") });

// HttpClient para recursos locales (en el cliente)
builder.Services.AddHttpClient("LocalClient", client =>
{
    client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress);
});
// Authentication and Authorization
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
builder.Services.AddAuthorizationCore();

// Third - party services(MudBlazor)
builder.Services.AddMudServices(config =>
{
    config.SnackbarConfiguration.PositionClass = Defaults.Classes.Position.TopCenter;

    config.SnackbarConfiguration.PreventDuplicates = false;
    config.SnackbarConfiguration.NewestOnTop = false;
    config.SnackbarConfiguration.ShowCloseIcon = true;
    config.SnackbarConfiguration.VisibleStateDuration = 10000;
    config.SnackbarConfiguration.HideTransitionDuration = 500;
    config.SnackbarConfiguration.ShowTransitionDuration = 500;
    config.SnackbarConfiguration.SnackbarVariant = Variant.Filled;
});

builder.Services.AddSweetAlert2();


// Application-specific services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ImageUploadService>();

builder.Services.AddScoped<IFlightService, FlightService>();
builder.Services.AddScoped<CountryService>();

await builder.Build().RunAsync();
