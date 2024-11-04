namespace KS.Application.DTOs.Article
{
    public class ArticleDetailsDTO
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
