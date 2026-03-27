using PracticaMvcTi.Models;
using PracticaMvcTi.ViewModels.EmpleadoViewModel;

namespace PracticaMvcTi.Clients
{
    public class EmpleadoApiClient
    {
        private HttpClient _httpClient;

        public EmpleadoApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://localhost:7279/api/empleados/");
        }


        public async Task<PaginationDto<EmpleadosDto>> Get(
              int page,
               string search,
              DateTime? fechaInicio,
              DateTime? fechaFin)
        {
            var url = $"?page={page}&pageSize=5&search={search}";

            if (fechaInicio.HasValue)
                url += $"&fechaInicio={fechaInicio.Value:yyyy-MM-dd}";

            if (fechaFin.HasValue)
                url += $"&fechaFin={fechaFin.Value:yyyy-MM-dd}";

            var response = await _httpClient
                .GetFromJsonAsync<PaginationDto<EmpleadosDto>>(url);

            return response;
        }

        public async Task<EmpleadosDto> GetPorId(int id)
        {
            var response = await _httpClient.GetAsync($"{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<EmpleadosDto>();
        }

        public async Task<bool> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{id}");
            return response.IsSuccessStatusCode;
        }

        public async Task<Empleado> Edit(int id, EmpleadosDto empleadoDto)
        {

            var response = await _httpClient.PutAsJsonAsync($"{id}", empleadoDto);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Empleado>();
        }



        public async Task<Empleado> CreateEmpleado(EmpleadosDto empleado)
        {

            var response = await _httpClient.PostAsJsonAsync("", empleado);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<Empleado>();

        }


    }
}
