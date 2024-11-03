using KS.API.Extensions;
using KS.Infrastructure;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructureServices(builder.Configuration);

builder.Services.AddControllers();

var app = builder.Build();

await app.ApplyMigrationsAndSeed();

app.UseAuthorization();

app.MapControllers();

app.Run();
