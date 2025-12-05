using HostelBooking.Api.Configurations;
using HostelBooking.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddApplicationServices();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    var railwayUrl = builder.Configuration["RailwayUrl"];
    if (string.IsNullOrEmpty(railwayUrl))
    {
        throw new InvalidOperationException("No database connection string found. Please configure 'ConnectionStrings:DefaultConnection' or 'RailwayUrl'.");
    }
    connectionString = ConnectionStringHelper.ParseRailwayUrl(railwayUrl);
}

builder.Services.AddDbContext<HostelDbContext>(options =>
{
    options.UseNpgsql(connectionString);
});

var app = builder.Build();

// Apply database migrations
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<HostelDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// For Railway deployment, use the PORT environment variable
var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Run($"http://0.0.0.0:{port}");
