using KS.API.Extensions;
using KS.Application;
using KS.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddHealthChecks();

var app = builder.Build();

app.MapHealthChecks("/api/health");

await app.ApplyMigrationsAndSeed();

app.UseAuthorization();

app.MapControllers();

app.Run();
