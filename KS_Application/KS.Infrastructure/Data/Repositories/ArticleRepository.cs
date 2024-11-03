using KS.Domain.Articles;
using Microsoft.EntityFrameworkCore;

namespace KS.Infrastructure.Data.Repositories
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly AppDbContext _context;

        public ArticleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Article>> GetAll()
        {
            return await _context.Articles
                .Where(a => !a.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
