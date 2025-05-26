using MAUI_ProyectoAvance2.Models;
using MAUI_ProyectoAvance2.Services;
using System.Collections.ObjectModel;
namespace MAUI_ProyectoAvance2;

public partial class ClientesPage : ContentPage
{
    public ObservableCollection<Cliente> Clientes { get; set; } = new();
    private readonly ClienteService _clienteService;

    public ClientesPage()
    {
        InitializeComponent();

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

        //Suscribirse a evento para actualizar lista cuando se cree o edite un cliente
        MessagingCenter.Subscribe<ClienteFormPage>(this, "ActualizarClientes", (sender) =>
        {
            CargarClientes();
        });

        CargarClientes();
    }

    private async void CargarClientes()
    {
        try
        {
            Console.WriteLine("Llamando al servicio...");
            var lista = await _clienteService.GetClientesAsync();
            Console.WriteLine("Respuesta recibida.");

            // ? Limpia y actualiza la colección observable
            Clientes.Clear();
            foreach (var cliente in lista)
            {
                Clientes.Add(cliente);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo conectar a la API.\n\n{ex.GetType().Name}: {ex.Message}", "OK");
        }
    }

    private async void OnAgregarClienteClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new ClienteFormPage());
    }

    private async void OnEditarCliente(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var cliente = swipeItem?.BindingContext as Cliente;

        if (cliente != null)
        {
            await Navigation.PushAsync(new ClienteFormPage(cliente));
        }
    }

    private async void OnEliminarCliente(object sender, EventArgs e)
    {
        var swipeItem = sender as SwipeItem;
        var cliente = swipeItem?.BindingContext as Cliente;

        if (cliente != null)
        {
            bool confirmar = await DisplayAlert("Confirmar", $"¿Eliminar a {cliente.Nombre}?", "Sí", "No");
            if (confirmar)
            {
                var exito = await _clienteService.DeleteClienteAsync(cliente.Id);
                if (exito)
                {
                    await DisplayAlert("Éxito", "Cliente eliminado", "OK");
                    CargarClientes();
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el cliente", "OK");
                }
            }
        }
    }
}
