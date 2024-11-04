using AutoMapper;
using KS.Application.DTOs.Article;
using KS.Application.DTOs.Offer;
using KS.Domain.Articles;
using KS.Domain.OfferArticles;
using KS.Domain.Offers;

namespace KS.Application.Mappings
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateOfferMappings();

            CreateMap<Article, ArticleDTO>();
        }

        private void CreateOfferMappings()
        {
            CreateMap<Offer, OfferReadDTO>()
                .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src.OfferArticles.Select(oa => oa.Article)))
                .ForMember(dest => dest.TotalPrice, opt => opt.MapFrom(src => src.TotalPrice));

            CreateMap<OfferCreateDTO, Offer>()
                .ForMember(dest => dest.OfferArticles, opt => opt.MapFrom(src =>
                    src.ArticleIds.Select(id => new OfferArticle { ArticleId = id })));

            CreateMap<OfferUpdateDTO, Offer>()
                .ForMember(dest => dest.OfferArticles, opt => opt.MapFrom(src =>
                    src.ArticleIds.Select(id => new OfferArticle { ArticleId = id })))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<OfferReadDTO, OfferUpdateDTO>()
                .ForMember(dest => dest.ArticleIds, opt => opt.MapFrom(src => src.Articles.Select(a => a.Id)));
        }
    }
}
