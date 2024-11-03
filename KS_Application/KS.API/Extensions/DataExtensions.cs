using KS.Infrastructure.Data;
using KS.Infrastructure.Data.Utils;

namespace KS.API.Extensions
{
    public static class DataExtensions
    {
        public async static Task ApplyMigrationsAndSeed(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await DataUtil.ApplyMigrationsAndSeed(dbContext);
        }
    }
}
