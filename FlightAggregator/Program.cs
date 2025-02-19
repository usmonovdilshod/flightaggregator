using FlightAggregator.Abstracts;
using FlightAggregator.Components;
using FlightAggregator.Data;
using FlightAggregator.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


#region DATABASE
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion


builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie() 
.AddGoogle(options =>
{
    options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
    options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    options.Events.OnRedirectToAuthorizationEndpoint = context =>
    {
        // Append the prompt=select_account parameter to the redirect URI
        var redirectUri = QueryHelpers.AddQueryString(context.RedirectUri, "prompt", "select_account");
        context.Response.Redirect(redirectUri);
        return Task.CompletedTask;
    };
});



builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ISearchService, SearchService>();
builder.Services.AddLogging();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>();
using var dbContext = dbContextFactory.CreateDbContext();
await dbContext.Database.MigrateAsync();

app.Run();
