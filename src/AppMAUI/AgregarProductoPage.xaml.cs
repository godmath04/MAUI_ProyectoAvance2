using MAUI_ProyectoAvance2.Models;
using MAUI_ProyectoAvance2.Services;

namespace MAUI_ProyectoAvance2;

public partial class AgregarProductoPage : ContentPage
{
    private List<CategoriaViewModel> categorias;

    public AgregarProductoPage()
    {
        InitializeComponent();
        CargarCategorias();
    }

    private void CargarCategorias()
    {
        categorias = new List<CategoriaViewModel>
        {
            new() { Id = 1, Nombre = "Bebidas" },
            new() { Id = 2, Nombre = "Postres" },
            new() { Id = 3, Nombre = "Picadas" },
            new() { Id = 4, Nombre = "Cervezas" }
        };

        categoriaPicker.ItemsSource = categorias;
    }

    private async void OnGuardarClicked(object sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(nombreEntry.Text) || categoriaPicker.SelectedItem == null)
        {
            await DisplayAlert("Error", "Completa todos los campos", "OK");
            return;
        }

        var producto = new Producto
        {
            Nombre = nombreEntry.Text,
            Precio = decimal.Parse(precioEntry.Text),
            Stock = int.Parse(stockEntry.Text),
            CategoriaId = ((CategoriaViewModel)categoriaPicker.SelectedItem).Id
        };

        var api = new ApiService();
        var resultado = await api.AgregarProductoAsync(producto);

        if (resultado)
            await DisplayAlert("Éxito", "Producto guardado", "OK");
        else
            await DisplayAlert("Error", "No se pudo guardar", "OK");
    }
}
