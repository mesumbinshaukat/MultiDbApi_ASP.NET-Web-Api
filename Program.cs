using Microsoft.EntityFrameworkCore;
using MultiDbApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Register DbContexts
var elasticConn = builder.Configuration.GetConnectionString("ElasticPoolDb");
var sqlConn = builder.Configuration.GetConnectionString("SqlServerDb");

if (string.IsNullOrEmpty(elasticConn) || string.IsNullOrEmpty(sqlConn))
{
    throw new InvalidOperationException("One or more connection strings are missing in appsettings.json.");
}

builder.Services.AddDbContext<ElasticPoolDbContext>(options =>
    options.UseSqlServer(elasticConn));

builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(sqlConn));

// 2. Enable CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("NextJsPolicy", policy =>
    {
        policy.WithOrigins("http://localhost:3000", "https://your-nextjs-domain.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // 3. Map Scalar API Reference
    app.MapScalarApiReference();
}

// 4. Handle HTTPS Redirection gracefully
// In development, we allow HTTP to avoid redirection issues when running the 'http' profile locally.
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

// 5. Use CORS
app.UseCors("NextJsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
