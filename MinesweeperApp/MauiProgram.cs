using Microsoft.Extensions.Logging;
using MinesweeperApp.Views;
using MinesweeperApp.ViewModels;
using MinesweeperApp.Services;
using Syncfusion.Maui.Core.Hosting;

namespace MinesweeperApp
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
            builder.ConfigureSyncfusionCore();
            builder.Services.AddSingleton<GameView>();
            builder.Services.AddSingleton<GameViewModel>();
            builder.Services.AddSingleton<Service>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
