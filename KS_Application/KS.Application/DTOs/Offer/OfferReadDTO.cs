using KS.Application.DTOs.Article;

namespace KS.Application.DTOs.Offer
{
    public class OfferReadDTO
    {
        public long Id { get; set; }
        public long OfferNumber { get; set; }
        public List<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();
        public int? TotalPrice { get; set; }
    }
}
