using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace KS.Application.Articles
{
    public class ArticleClient
    {
        private readonly HttpClient _httpClient;
        private readonly string _fullArticleApiURL;

        public ArticleClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            var baseURL = configuration["ApiSettings:BaseUrl"];
            var articleApiURL = configuration["ApiSettings:ArticlesUrl"];
            _fullArticleApiURL = $"{baseURL}/{articleApiURL}";
        }

        public async Task<IEnumerable<ArticleDTO>?> GetAllAsync()
        {
            return await _httpClient.GetFromJsonAsync<IEnumerable<ArticleDTO>>($"{_fullArticleApiURL}/getAll");
        }
    }
}
