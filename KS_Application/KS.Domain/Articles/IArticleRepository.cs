namespace KS.Domain.Articles
{
    public interface IArticleRepository
    {
        public Task<List<Article>> GetAll();
    }
}
