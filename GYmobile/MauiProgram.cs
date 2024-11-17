using GYmobile.Services;
using Microsoft.Extensions.Logging;

namespace GYmobile
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
                });

            builder.Services.AddMauiBlazorWebView();

            builder.Services.AddHttpClient<AuthService>(client =>
            {
                client.BaseAddress = new Uri("http://rentagym.runasp.net");
            });

            builder.Services.AddHttpClient<CommonService>(client =>
            {
                client.BaseAddress = new Uri("http://rentagym.runasp.net");
            });

            //builder.Services.AddSingleton<CommonService>();

            builder.Services.AddHttpClient<LandlordService>(client =>
            {
                client.BaseAddress = new Uri("http://rentagym.runasp.net");
            });

            builder.Services.AddHttpClient<TenantService>(client =>
            {
                client.BaseAddress = new Uri("http://rentagym.runasp.net");
            });


#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
