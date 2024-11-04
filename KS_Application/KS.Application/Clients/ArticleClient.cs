using KS.Domain.Articles;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace KS.Application.Clients
{
    public class ArticleClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _articleApiURL;

        public ArticleClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _articleApiURL = configuration["ApiSettings:ArticlesUrl"];
        }

        public async Task<List<Article>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Article>>($"{_articleApiURL}/getAll");
        }
    }
}
