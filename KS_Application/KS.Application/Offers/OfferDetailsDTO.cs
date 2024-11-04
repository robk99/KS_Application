using KS.Application.Articles;

namespace KS.Application.Offers
{
    public class OfferDetailsDTO : OfferListDTO
    {
        public List<ArticleDTO> Articles { get; set; } = new List<ArticleDTO>();
        public int? TotalPrice { get; set; }
    }
}
