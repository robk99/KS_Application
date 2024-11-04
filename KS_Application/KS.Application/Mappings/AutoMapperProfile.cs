using AutoMapper;
using KS.Application.Articles;
using KS.Application.Offers;
using KS.Application.Offers.Create;
using KS.Application.Offers.Update;
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

            CreateMap<Offer, OfferListDTO>();

            CreateMap<Offer, OfferDetailsDTO>()
                .ForMember(dest => dest.Articles, opt => opt.MapFrom(src => src.OfferArticles.Select(oa => oa.Article)));


            CreateMap<OfferCreateDTO, Offer>()
                .ForMember(dest => dest.OfferArticles, opt => opt.MapFrom(src =>
                    src.ArticleIds.Select(id => new OfferArticle { ArticleId = id })));

            CreateMap<OfferUpdateDTO, Offer>()
                .ForMember(dest => dest.OfferArticles, opt => opt.MapFrom(src =>
                    src.ArticleIds.Select(id => new OfferArticle { ArticleId = id })))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));

            CreateMap<OfferDetailsDTO, OfferUpdateDTO>()
                .ForMember(dest => dest.ArticleIds, opt => opt.MapFrom(src => src.Articles.Select(a => a.Id)));
        }
    }
}
