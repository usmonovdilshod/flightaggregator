using FlightAggregator.Abstracts;
using FlightAggregator.Components;
using FlightAggregator.Data;
using FlightAggregator.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


#region DATABASE
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();



builder.Services.AddMemoryCache();
builder.Services.AddSingleton<ISearchService, SearchService>();
builder.Services.AddLogging();
var app = builder.Build();

// Configure the HTTP request pipeline.
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

var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>();
using var dbContext = dbContextFactory.CreateDbContext();
await dbContext.Database.MigrateAsync();

app.Run();
