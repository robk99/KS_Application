using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KS.Domain.Abstractions;
using KS.Domain.OfferArticles;

namespace KS.Domain.Offers
{
    public class Offer : AuditDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        /// <summary>
        /// Check OfferConfiguration for more info
        /// </summary>
        public long OfferNumber { get; set; }

        [Required]
        public ICollection<OfferArticle> OfferArticles { get; set; } = new List<OfferArticle>();
        public bool IsDeleted { get; set; } = false;

        [NotMapped]
        public int? TotalPrice => OfferArticles?.Sum(a => a.Article.Price) ?? 0;
    }
}
