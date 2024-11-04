using Microsoft.Extensions.DependencyInjection;
using KS.Application.Clients;
using KS.Application.Mappings;

namespace KS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));

            services.AddHttpClient<OfferClient>();
            services.AddHttpClient<ArticleClient>();

            return services;
        }

    }
}
