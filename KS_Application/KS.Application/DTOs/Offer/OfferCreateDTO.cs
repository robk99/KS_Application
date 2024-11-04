namespace KS.Application.DTOs.Offer
{
    public class OfferCreateDTO
    {
        public IList<long> ArticleIds { get; set; } = new List<long>();
    }
}
