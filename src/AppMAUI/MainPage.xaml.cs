namespace MAUI_ProyectoAvance2 {
public partial class MainPage : ContentPage
{
    public List<string> PromoImages { get; set; }

    public MainPage()
    {
        InitializeComponent();

        PromoImages = new List<string>
        {
            "promo1.jpg",
            "promo2.jpg",
            "promo3.jpg"
        };

        BindingContext = this;
    }


        private async void OnClientesClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ClientesPage());
            }


        private async void OnFacturasClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FacturasPage());
        }

        private async void OnPedidosClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PedidosPage());
        }

        private async void OnProductoClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProductosPage());
        }

        private async void OnCategoriaClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CategoriaPage());
        }






    }
}