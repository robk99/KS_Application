using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using KS.Domain.Abstractions;
using KS.Domain.OfferArticles;
using KS.Domain.Offers;

namespace KS.Domain.Articles
{
    public class Article : AuditDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Price { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
