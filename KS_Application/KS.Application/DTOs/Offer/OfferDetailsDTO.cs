using KS.Application.DTOs.Article;

namespace KS.Application.DTOs.Offer
{
    public class OfferDetailsDTO : OfferListDTO
    {
        public List<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();
        public int? TotalPrice { get; set; }
    }
}
