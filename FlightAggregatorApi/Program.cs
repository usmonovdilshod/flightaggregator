using FlightAggregatorApi.Abstracts;
using FlightAggregatorApi.Data;
using FlightAggregatorApi.ServiceCollection;
using FlightAggregatorApi.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .MinimumLevel.Override("System", LogEventLevel.Warning)
    .MinimumLevel.Information()
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.Logger(apiLogger => apiLogger
        .Filter.ByIncludingOnly(e =>
        {
            if (e.Properties.TryGetValue("RequestPath", out var pathProperty))
            {
                var path = pathProperty.ToString().Trim('"');
                return path.StartsWith("/api");
            }
            return false;
        })
        .WriteTo.File(
            "logs/api-requests-.txt",
            rollingInterval: RollingInterval.Day,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}"
        )
    )
    .CreateLogger();



builder.Host.UseSerilog();

#region DATABASE
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddMemoryCache();
builder.Services.AddLogging();
builder.Services.AddHttpClient();
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IBookService, BookService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseSerilogRequestLogging();
app.UseAuthorization();

app.MapControllers();


//var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>();
//using var dbContext = dbContextFactory.CreateDbContext();
//await dbContext.Database.MigrateAsync();

app.Run();
