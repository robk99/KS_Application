using KS.Application.DTOs.Offer;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace KS.Application.Clients
{
    public class OfferClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _fullOfferApiURL;

        public OfferClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var baseURL = configuration["ApiSettings:BaseUrl"];
            var offerApiURL = configuration["ApiSettings:OffersUrl"];
            _fullOfferApiURL = $"{baseURL}/{offerApiURL}";
        }

        public async Task<IEnumerable<OfferDetailsDTO>?> GetAllListAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<OfferDetailsDTO>>($"{_fullOfferApiURL}/getAllList");
        }

        public async Task<OfferDetailsDTO?> GetByIdAsync(long id)
        {
            return await _httpClient.GetFromJsonAsync<OfferDetailsDTO>($"{_fullOfferApiURL}/getById/{id}");
        }

        public async Task<bool> CreateAsync(OfferCreateDTO offerDTO)
        {
            var response = await _httpClient.PostAsJsonAsync($"{_fullOfferApiURL}/create", offerDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateAsync(OfferUpdateDTO offerDTO)
        {
            var response = await _httpClient.PutAsJsonAsync($"{_fullOfferApiURL}/update", offerDTO);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var response = await _httpClient.DeleteAsync($"{_fullOfferApiURL}/delete/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
