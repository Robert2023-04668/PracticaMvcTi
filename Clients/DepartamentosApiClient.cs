using PracticaMvcTi.Models;

namespace PracticaMvcTi.Clients
{
    public class DepartamentosApiClient
    {
        private  HttpClient _httpClient;

        public DepartamentosApiClient(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/departamentos/");
        }

        public async Task<PaginationDto<Departamento>> Get(int page = 1, string search = "")
        {
            var url = $"?page={page}&search={search}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content
                .ReadFromJsonAsync<PaginationDto<Departamento>>();
        }

        public async Task<Departamento> GetDepartamentoPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Departamento>();
        }

        public async Task<Departamento> DeleteDepartamentoPorId(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Departamento>();
        }



        public async Task<Departamento> CreateDepartament(Departamento departamento)
        {
            var response = await _httpClient.PostAsJsonAsync("", departamento);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Departamento>();
        }

        public async Task<Departamento> EditDepartment(int id, Departamento departamento)
        {
            var response = await _httpClient.PutAsJsonAsync($"{id}", departamento);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadFromJsonAsync<Departamento>();
        }

    }
}
