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

        // ? Inicializar el servicio correctamente con HttpClient
        var httpClient = new HttpClient
        {
#if ANDROID
            BaseAddress = new Uri("https://10.0.2.2:7196/")
#else
            BaseAddress = new Uri("https://localhost:7196/")
#endif
        };

        _clienteService = new ClienteService(httpClient);
        BindingContext = this;

        CargarClientes();
    }

    private async void CargarClientes()
    {
        Clientes = await _clienteService.GetClientesAsync(); // ? método correcto
        OnPropertyChanged(nameof(Clientes));
    }
}
