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
            builder.Services.AddTransient<AdminViewModel>();
            builder.Services.AddTransient<AppShellViewModel>();
            builder.Services.AddTransient<BlockedListViewModel>();
            builder.Services.AddTransient<EditProfileViewModel>();
            builder.Services.AddTransient<FriendLeaderboardViewModel>();
            builder.Services.AddTransient<FriendRequestsViewModel>();
            builder.Services.AddTransient<GameReportsViewModel>();
            builder.Services.AddTransient<GameViewModel>();
            builder.Services.AddTransient<HomeViewModel>();
            builder.Services.AddTransient<LeaderboardViewModel>();
            builder.Services.AddTransient<LoginViewModel>();
            builder.Services.AddTransient<LogoutViewModel>();
            builder.Services.AddTransient<ProfileViewModel>();
            builder.Services.AddTransient<RegisterViewModel>();
            builder.Services.AddTransient<StartGameViewModel>();
            builder.Services.AddTransient<SocialPageViewModel>();
            builder.Services.AddTransient<UserReportsViewModel>();
            #endregion

            #region add Service
            builder.Services.AddSingleton<Service>();
            #endregion

            #region add Views
            builder.Services.AddTransient<AdminView>();
            builder.Services.AddTransient<AppShell>();
            builder.Services.AddTransient<BlockedListView>();
            builder.Services.AddTransient<EditProfileView>();
            builder.Services.AddTransient<FriendLeaderboardView>();
            builder.Services.AddTransient<FriendRequestsView>();
            builder.Services.AddTransient<GameReportsView>();
            builder.Services.AddTransient<GameView>();
            builder.Services.AddTransient<HomeView>();
            builder.Services.AddTransient<LeaderboardView>();
            builder.Services.AddTransient<LoginView>();
            builder.Services.AddTransient<LogoutView>();
            builder.Services.AddTransient<ProfileView>();
            builder.Services.AddTransient<RegisterView>();
            builder.Services.AddTransient<StartGameView>();
            builder.Services.AddTransient<SocialPageView>();
            builder.Services.AddTransient<UserReportsView>();
            #endregion

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
