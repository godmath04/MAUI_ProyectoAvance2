using Microsoft.Extensions.Logging;
using System.Net.Http;
using MAUI_ProyectoAvance2.Services;

namespace MAUI_ProyectoAvance2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            //Registra HttpClient con la URL base del API
            builder.Services.AddSingleton(new HttpClient
            {
                BaseAddress = new Uri("https://localhost:7196/api/")
            });

            //Registra servicio cliente
            builder.Services.AddSingleton<ClienteService>();

            return builder.Build();
        }
    }
}
