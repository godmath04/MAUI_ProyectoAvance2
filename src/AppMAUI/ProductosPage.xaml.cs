using MAUI_ProyectoAvance2.Models;
using MAUI_ProyectoAvance2.Services;

namespace MAUI_ProyectoAvance2;

public partial class ProductosPage : ContentPage
{
    private readonly DatabaseService _db;

    public ProductosPage(DatabaseService db)
    {
        InitializeComponent();
        _db = db;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await CargarProductos();
    }

    private async Task CargarProductos()
    {
        var productos = await _db.GetProductosAsync();
        productosListView.ItemsSource = productos;
    }
}
