﻿using KS.Domain.OfferArticles;
using KS.Domain.Offers;
using Microsoft.EntityFrameworkCore;

namespace KS.Infrastructure.Data.Repositories
{
    public class OfferRepository : IOfferRepository
    {
        private readonly AppDbContext _context;

        public OfferRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Offer>> GetAll()
        {
            return await _context.Offers
                .Where(a => !a.IsDeleted)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Offer?> GetById(long id)
        {
            return await _context.Offers
                .FirstOrDefaultAsync(a => a.Id == id && !a.IsDeleted);
        }

        public async Task<bool> Create(Offer offer)
        {
            await _context.Offers.AddAsync(offer);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> Update(Offer updatedOffer)
        {
            var existingOffer = await _context.Offers
              .Include(o => o.OfferArticles)
              .FirstOrDefaultAsync(o => o.Id == updatedOffer.Id);

            if (existingOffer == null || existingOffer.IsDeleted)
                return false;

            _context.OfferArticles.RemoveRange(existingOffer.OfferArticles);

            foreach (var articleId in updatedOffer.OfferArticles.Select(oa => oa.ArticleId))
            {
                existingOffer.OfferArticles.Add(new OfferArticle
                {
                    OfferId = existingOffer.Id,
                    ArticleId = articleId
                });
            }

            _context.Offers.Update(existingOffer);
            await _context.SaveChangesAsync();

            return true;
        }

        /// <summary>
        /// Soft Delete
        /// </summary>
        /// <param name="id"></param>
        public async Task<bool> Delete(long id)
        {
            var offer = await _context.Offers.FindAsync(id);

            if (offer == null || offer.IsDeleted)
                return false;

            offer.IsDeleted = true;

            _context.Offers.Update(offer);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}