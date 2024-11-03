using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Polly;

namespace KS.Infrastructure.Data.Utils
{
    public static class DataUtil
    {
        public async static Task ApplyMigrationsAndSeed(AppDbContext dbContext)
        {
            var retryPolicy = Policy.Handle<SqlException>()
                .WaitAndRetryAsync(10, attempt => TimeSpan.FromSeconds(3), (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retry {retryCount} failed to connect to the DB. Retrying in {timeSpan.TotalSeconds} seconds.");
                });

            await retryPolicy.ExecuteAsync(async () =>
            {
                if (dbContext.Database.GetPendingMigrations().Any())
                {
                    Console.WriteLine("----------------- APPLYING MIGRATIONS -----------------");
                    dbContext.Database.Migrate();
                }
                else
                {
                    Console.WriteLine("----------------- NO MIGRATIONS -----------------");
                }
            });
        }
    }
}
