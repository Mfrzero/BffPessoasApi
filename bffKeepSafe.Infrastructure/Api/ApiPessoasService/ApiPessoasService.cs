using bffKeepSafe.Domain.Interfaces.Api;
using bffKeepSafe.Domain.Models.Pessoas;
using bffKeepSafe.Domain.Models.Usuarios;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace bffKeepSafe.Infrastructure.Api.ApiPessoasService
{
    public class ApiPessoasService : IApiPessoasService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://localhost:5001/api/";

        public ApiPessoasService(HttpClient httpClient, IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(BaseUrl);
            _httpClient.Timeout = TimeSpan.FromSeconds(9999);
        }

        public async Task<IEnumerable<PessoasResponse>> GetApiResponseAsync()
        {
            var authEndpoint = "https://localhost:5001/api/usuario/geraToken";
            var credentials = new Usuario
            {
                Nome = "Matheus",
                Senha = "12345",
                Role = "admin"
            };
            var responses = await _httpClient.PostAsJsonAsync(authEndpoint, credentials);
            var token = await responses.Content.ReadAsStringAsync();


            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); 
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var response = await _httpClient.GetAsync($"pessoas"); 
            response.EnsureSuccessStatusCode();
            IEnumerable<PessoasResponse> responseData = await response.Content.ReadFromJsonAsync<IEnumerable<PessoasResponse>>();
                
            return responseData;
        }

    }
}
