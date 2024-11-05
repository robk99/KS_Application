using KS.Application.Offers.Create;
using KS.Application.Offers.Update;
using KS.Application.Response;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace KS.Application.Offers
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

        public async Task<PagedResultDTO<OfferListDTO>> GetAllListAsyncPaginated(int page, int pageSize)
        {
            return await _httpClient.GetFromJsonAsync<PagedResultDTO<OfferListDTO>>($"{_fullOfferApiURL}/getAllList?page={page}&pageSize={pageSize}");
        }

        public async Task<OfferDetailsDTO?> GetByIdAsync(long id)
        {
            var offerDetails = new OfferDetailsDTO();

            try
            {
                offerDetails = await _httpClient.GetFromJsonAsync<OfferDetailsDTO>($"{_fullOfferApiURL}/getById/{id}");
            }
            catch (HttpRequestException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound) return null;
                
            }
            catch (System.Exception ex)
            {
                throw ex;
            }

            return offerDetails;
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
