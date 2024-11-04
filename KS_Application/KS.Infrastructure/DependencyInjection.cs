using KS.Infrastructure.Data;
using KS.Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using KS.Infrastructure.Data.Repositories;
using KS.Domain.Offers;
using KS.Domain.Articles;

namespace KS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");

            services.AddScoped<ISaveChangesInterceptor, UpdateAuditDetailsInterceptor>();
            services.AddDbContext<AppDbContext>((sp, options) =>
            {
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);

            });

            services.AddScoped<IArticleRepository, ArticleRepository>();
            services.AddScoped<IOfferRepository, OfferRepository>();

            return services;
        }

    }
}
