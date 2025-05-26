using MAUI_ProyectoAvance2.Services;

namespace MAUI_ProyectoAvance2;

public partial class ClientesPage : ContentPage
{
    private readonly ClienteService _clienteService;

    public ClientesPage(ClienteService clienteService)
    {
        InitializeComponent();
        _clienteService = clienteService;
        // Llama a los métodos del servicio según lo necesites
    }
}