
using MAUI_ProyectoAvance2.Models;
using MAUI_ProyectoAvance2.Services;
using System.Collections.ObjectModel;

namespace MAUI_ProyectoAvance2;

public partial class ProductosPage : ContentPage
{
    public ObservableCollection<Producto> Productos { get; set; } = new();

    public ProductosPage()
    {
        InitializeComponent();
        BindingContext = this;
        _ = CargarProductos();
    }

    private async Task CargarProductos()
    {
        var api = new ApiService();
        var lista = await api.GetProductosAsync();
        foreach (var producto in lista)
            Productos.Add(producto);
    }
}
