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
            return await _httpClient.GetFromJsonAsync<List<Cliente>>("ClientesBD");
        }

        public async Task<Cliente> GetClienteAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Cliente>($"ClientesBD/{id}");
        }

        public async Task<bool> CreateClienteAsync(Cliente cliente)
        {
            var response = await _httpClient.PostAsJsonAsync("ClientesBD", cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateClienteAsync(Cliente cliente)
        {
            var response = await _httpClient.PutAsJsonAsync($"ClientesBD/{cliente.Id}", cliente);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteClienteAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"ClientesBD/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
