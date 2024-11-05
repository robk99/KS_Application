using FluentValidation.AspNetCore;
using KS.API.Extensions;
using KS.Application;
using KS.Infrastructure;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "KS_Application_API", Version = "v1" });
});

builder.Services.AddHealthChecks();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "KS_Application_API v1");
    });
}

app.MapHealthChecks("/api/health");

await app.ApplyMigrationsAndSeed();

app.UseAuthorization();

app.MapControllers();

app.Run();
