
namespace KS.Domain.Offers
{
    public interface IOfferRepository
    {
        public Task<List<Offer>> GetAll();

        public Task<Offer?> GetById(long id);

        public Task<bool> Create(Offer offer);

        public Task<bool> Update(Offer updatedOffer);

        public Task<bool> Delete(long id);
    }
}
