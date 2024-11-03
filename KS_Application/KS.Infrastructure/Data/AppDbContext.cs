using KS.Application.Abstractions.Data;
using KS.Domain.Articles;
using KS.Domain.OfferArticles;
using KS.Domain.Offers;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KS.Infrastructure.Data
{
    public class AppDbContext : DbContext, IApplicationDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<OfferArticle> OfferArticles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<OfferArticle>()
                .HasKey(oa => oa.Id);

            modelBuilder.Entity<OfferArticle>()
              .HasOne(oa => oa.Offer)
              .WithMany(o => o.OfferArticles)
              .HasForeignKey(oa => oa.OfferId);

            modelBuilder.Entity<OfferArticle>()
              .HasOne(oa => oa.Article)
              .WithMany()
              .HasForeignKey(oa => oa.ArticleId)
              .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.HasSequence<int>("OfferNumberSequence", "dbo")
             .StartsAt(OfferConfiguration.InitialOfferNumber)
             .IncrementsBy(OfferConfiguration.OfferNumberIncrement);

            modelBuilder.Entity<Offer>(entity =>
            {
                entity.Property(o => o.OfferNumber)
                    .HasDefaultValueSql("NEXT VALUE FOR dbo.OfferNumberSequence");

                entity.HasIndex(o => o.OfferNumber).IsUnique();
            });

            modelBuilder.Entity<Offer>()
                .Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.Entity<Article>()
                .Property(e => e.IsDeleted)
                .HasDefaultValue(false);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            var rnd = new Random();

            // Articles
            for (int i = 1; i < 151; i++)
            {
                modelBuilder.Entity<Article>().HasData(new Article()
                {
                    Id = i,
                    Name = $"Article-{i}",
                    Price = rnd.Next(1, 10001),
                });
            }

            var offers = new List<Offer>();
            var offerArticleId = 1;
            for (int i = 1; i < 51; i++)
            {
                // Offers
                modelBuilder.Entity<Offer>().HasData(new Offer()
                {
                    Id = i,
                });

                // OfferArticles
                var articleIds = Enumerable.Range(1, 150)
                    .OrderBy(r => rnd.Next())
                    .Take(rnd.Next(1, 25))
                    .ToArray();

                for (int j = 0; j < articleIds.Length; j++)
                {
                    var articleId = articleIds[j];
                    modelBuilder.Entity<OfferArticle>().HasData(new OfferArticle()
                    {
                        Id = offerArticleId,
                        OfferId = i,
                        ArticleId = articleId
                    });
                    offerArticleId++;
                }
            }
        }
    }
}
