using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.PuestosViewModel;

namespace PracticaMvcTi.Clients
{
    public class PuestosApiClient
    {

        private HttpClient _httpClient;

        public PuestosApiClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/puestos/");
        }

        public async Task<PaginationDto<Puesto>> Get(int page = 1, string search = "")
        {
            var url = $"?page={page}&search={search}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<PaginationDto<Puesto>>();
        }

        public async Task<Puesto> GetPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Puesto>();
        }

        public async Task<Puesto> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Puesto>();
        }



        public async Task<Puesto> Create(PuestosDto puesto)
        {
            var response = await _httpClient.PostAsJsonAsync("", puesto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Puesto>();
        }

        public async Task<Puesto> Edit(int id, Puesto puesto)
        {
            var response = await _httpClient.PutAsJsonAsync($"{id}", puesto);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Puesto>();
        }

    }
}
