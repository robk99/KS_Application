using Microsoft.Extensions.DependencyInjection;
using KS.Application.Mappings;
using KS.Application.Articles;
using KS.Application.Offers;
using FluentValidation;
using KS.Application.Offers.Create;
using KS.Application.Offers.Update;

namespace KS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile));
            //services.AddValidators();
            //services.AddValidatorsFromAssemblyContaining<OfferCreateDTOValidator>();
            //services.AddValidatorsFromAssemblyContaining<OfferUpdateDTOValidator>();
            services.AddScoped<IValidator<OfferCreateDTO>, OfferCreateDTOValidator>();
            services.AddScoped<IValidator<OfferUpdateDTO>, OfferUpdateDTOValidator>();

            services.AddHttpClient<OfferClient>();
            services.AddHttpClient<ArticleClient>();

            return services;
        }


        private static void AddValidators(this IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<OfferCreateDTOValidator>();
            services.AddValidatorsFromAssemblyContaining<OfferUpdateDTOValidator>();
        }
    }
}
