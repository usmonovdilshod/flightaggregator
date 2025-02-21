using Microsoft.EntityFrameworkCore;
using SkyLinkApi.Abstracts;
using SkyLinkApi.Data;
using SkyLinkApi.Services;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;


#region DATABASE
builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
#endregion
// Add services to the container.
builder.Services.AddScoped<ISearchService, SearchService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//var dbContextFactory = app.Services.GetRequiredService<IDbContextFactory<AppDbContext>>();
//using var dbContext = dbContextFactory.CreateDbContext();
//await dbContext.Database.MigrateAsync();

app.Run();
