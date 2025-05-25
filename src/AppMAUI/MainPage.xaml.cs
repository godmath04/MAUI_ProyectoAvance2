namespace MAUI_ProyectoAvance2 {
public partial class MainPage : ContentPage
{
    public List<string> PromoImages { get; set; }

    int count = 0;

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

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        CounterBtn.Text = count == 1
            ? "Haz clic 1 vez"
            : $"Haz clic {count} veces";

        SemanticScreenReader.Announce(CounterBtn.Text);
    }
}
}