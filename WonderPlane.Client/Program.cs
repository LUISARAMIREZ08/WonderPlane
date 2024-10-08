using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using WonderPlane.Client;
using MudBlazor.Services;

using WonderPlane.Client.Servicios;
//using WonderPlane.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddMudServices();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7056") });

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ImageUploadService>();
builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddMudServices();
builder.Services.AddScoped<CountryService>();

await builder.Build().RunAsync();
