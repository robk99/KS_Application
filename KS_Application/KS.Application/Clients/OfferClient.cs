using KS.Domain.Offers;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace KS.Application.Clients
{
    public class OfferClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _offerApiURL;

        public OfferClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _offerApiURL = configuration["ApiSettings:OffersUrl"];
        }

        public async Task<List<Offer>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Offer>>($"{_offerApiURL}/getAll");
        }

        public async Task<Offer?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<Offer>($"{_offerApiURL}/getById/{id}");
        }

        public async Task<bool> CreateAsync(Offer offer)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_offerApiURL}/create", offer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(Offer updatedOffer)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_offerApiURL}/update", updatedOffer);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"{_offerApiURL}/delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
