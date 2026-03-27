using Microsoft.EntityFrameworkCore;
using MultiDbApi;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// 1. Register DbContexts
builder.Services.AddDbContext<ElasticPoolDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ElasticPoolDb")));

builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDb")));

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
