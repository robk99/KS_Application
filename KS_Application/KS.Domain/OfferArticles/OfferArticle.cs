using KS.Domain.Articles;
using KS.Domain.Offers;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace KS.Domain.OfferArticles
{
    /// <summary>
    /// Bridge table that represents many-many relationship between Offers and Articles
    /// </summary>
    public class OfferArticle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long OfferId { get; set; }
        public Offer Offer { get; set; }

        public long ArticleId { get; set; }
        public Article Article { get; set; }
    }
}
