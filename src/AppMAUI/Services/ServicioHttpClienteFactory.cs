namespace MAUI_ProyectoAvance2.Services;

public static class ServicioHttpClienteFactory
{
    public static ClienteService Crear()
    {
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

        return new ClienteService(httpClient);
    }
}
