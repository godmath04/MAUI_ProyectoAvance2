using MAUI_ProyectoAvance2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MAUI_ProyectoAvance2.Services
{
    public class ClienteService
    {
        private readonly HttpClient _httpClient;

        public ClienteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Cliente>> GetClientesAsync()
        {
            return await _httpClient.GetFromJsonAsync<List<Cliente>>("api/ClientesBD");
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Cliente>($"api/ClientesBD/{id}");
        }

        public async Task<bool> CreateClienteAsync(Cliente cliente)
        {
            var response = await _httpClient.PostAsJsonAsync("api/ClientesBD", cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClienteAsync(Cliente cliente)
        {
            var response = await _httpClient.PutAsJsonAsync($"api/ClientesBD/{cliente.Id}", cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/ClientesBD/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
