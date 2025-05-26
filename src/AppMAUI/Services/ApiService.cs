using MAUI_ProyectoAvance2.Models;
using System.Net.Http.Json;

namespace MAUI_ProyectoAvance2.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000/api/") // Cambia si tu backend usa otro puerto
            };
        }

        public async Task<List<Producto>> GetProductosAsync()
        {
            try
            {
                var productos = await _httpClient.GetFromJsonAsync<List<Producto>>("productos");
                return productos ?? new List<Producto>();
            }
            catch
            {
                return new List<Producto>();
            }
        }

        public async Task<bool> AgregarProductoAsync(Producto producto)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("productos", producto);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }
    }
}
