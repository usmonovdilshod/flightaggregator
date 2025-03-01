using FlightAggregatorShared.TokenHandler;
using FlightAggregatorUI;
using FlightAggregatorUI.State;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NetcodeHub.Packages.Extensions.LocalStorage;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddHttpClient(Constant.Client, client =>
{
    client.BaseAddress = new Uri(builder.Configuration["AggregatorUrl"]!);
});
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthState>();
builder.Services.AddAuthorizationCore();
builder.Services.AddNetcodeHubLocalStorageService();


await builder.Build().RunAsync();

