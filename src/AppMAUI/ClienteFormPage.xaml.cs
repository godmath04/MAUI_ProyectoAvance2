using MAUI_ProyectoAvance2.Models;
using MAUI_ProyectoAvance2.Services;

namespace MAUI_ProyectoAvance2;

public partial class ClienteFormPage : ContentPage
{
    private readonly ClienteService _clienteService;
    private Cliente _clienteEditando;

    public ClienteFormPage(Cliente cliente = null)
    {
        InitializeComponent();

        _clienteService = ServicioHttpClienteFactory.Crear();

        if (cliente != null)
        {
            Title = "Editar Cliente";
            _clienteEditando = cliente;

            NombreEntry.Text = cliente.Nombre;
            ApellidoEntry.Text = cliente.Apellido;
            TelefonoEntry.Text = cliente.Telefono;
            CorreoEntry.Text = cliente.Correo;
        }
        else
        {
            Title = "Nuevo Cliente";
        }
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        var cliente = new Cliente
        {
            Nombre = NombreEntry.Text,
            Apellido = ApellidoEntry.Text,
            Telefono = TelefonoEntry.Text,
            Correo = CorreoEntry.Text,
            FechaCreacion = DateTime.Now
        };

        bool exito;

        if (_clienteEditando == null)
        {
            exito = await _clienteService.CreateClienteAsync(cliente);
        }
        else
        {
            cliente.Id = _clienteEditando.Id;
            exito = await _clienteService.UpdateClienteAsync(cliente);
        }

        if (exito)
        {
            await DisplayAlert("Éxito", "Cliente guardado correctamente", "OK");
            await Navigation.PopAsync();
        }
        else
        {
            await DisplayAlert("Error", "Hubo un problema al guardar", "OK");
        }
    }
}
