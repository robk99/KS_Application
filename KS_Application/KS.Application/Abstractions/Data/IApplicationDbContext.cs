using KS.Domain.Articles;
using KS.Domain.OfferArticles;
using KS.Domain.Offers;
using Microsoft.EntityFrameworkCore;

namespace KS.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        DbSet<Article> Articles { get; }
        DbSet<Offer> Offers { get; }
        DbSet<OfferArticle> OfferArticles { get; set; }
    }
}
