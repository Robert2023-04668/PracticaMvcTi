using PracticaMvcTi.Models;

namespace PracticaMvcTi.Clients
{
    public class EstadosApiClient
    {

        private HttpClient _httpClient;

        public EstadosApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/estados/");
        }

        public async Task<PaginationDto<Estado>> Get(int page = 1, string search = "")
        {
            var url = $"?page={page}&search={search}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<PaginationDto<Estado>>();
        }

        public async Task<Estado> GetoPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Estado>();
        }

        public async Task<Estado> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Estado>();
        }



        public async Task<Estado> Create(Estado estado)
        {
            var response = await _httpClient.PostAsJsonAsync("", estado);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Estado>();
        }

        public async Task<Estado> Edit(int id, Estado estado)
        {
            var response = await _httpClient.PutAsJsonAsync($"{id}", estado);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Estado>();
        }
    }
}
