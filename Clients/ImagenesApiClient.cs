using PracticaMvcTi.Models;

namespace PracticaMvcTi.Clients
{
    public class ImagenesApiClient
    {

        private HttpClient _httpClient;

        public ImagenesApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/imagenes/");
        }

        public async Task<PaginationDto<Imagene>> Get(int page = 1, string search = "")
        {
            var url = $"?page={page}&search={search}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<PaginationDto<Imagene>>();
        }

     
        public async Task<Imagene> GetPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Imagene>();
        }

        public async Task<Imagene> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Imagene>();
        }



        public async Task<Imagene> Create(Imagene imagen)
        {
            var response = await _httpClient.PostAsJsonAsync("", imagen);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Imagene>();
        }

        public async Task<Imagene> Edit(int id, Imagene imagene)
        {
            var response = await _httpClient.PutAsJsonAsync($"{id}", imagene);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Imagene>();
        }
    }
}
