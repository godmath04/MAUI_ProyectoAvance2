using MAUI_ProyectoAvance2.Models;
using MAUI_ProyectoAvance2.Services;

namespace MAUI_ProyectoAvance2;

public partial class ClientesPage : ContentPage
{
    public List<Cliente> Clientes { get; set; } = new();
    private readonly ClienteService _clienteService;

    public ClientesPage()
    {
        InitializeComponent();

        // ? Crear HttpClient con URL del host (adaptada para Android)
#if ANDROID
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://10.0.2.2:5184/")
        };
#else
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:7196/")
        };
#endif

        _clienteService = new ClienteService(httpClient);

        BindingContext = this;
        CargarClientes();
    }

    private async void CargarClientes()
    {
        try
        {
            Clientes = await _clienteService.GetClientesAsync();
            OnPropertyChanged(nameof(Clientes));
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo conectar a la API.\n{ex.Message}", "OK");
        }
    }
}
