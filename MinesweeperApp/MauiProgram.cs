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

            #region Syncfusion
            builder.ConfigureSyncfusionCore();
            #endregion

            #region add ViewModels
            builder.Services.AddTransient<GameViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<StartGameViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<LeaderboardViewModel>();
            builder.Services.AddTransient<GameReportsViewModel>();
            #endregion

            #region add Service
            builder.Services.AddSingleton<Service>();
            #endregion

            #region add Views
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<GameView>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<StartGameView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<LeaderboardView>();
            builder.Services.AddTransient<GameReportsView>();
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
