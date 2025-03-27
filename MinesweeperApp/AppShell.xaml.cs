using MinesweeperApp.ViewModels;
using MinesweeperApp.Views;
using Syncfusion.Maui.Core.Converters;

namespace MinesweeperApp
{
    public partial class AppShell : Shell
    {
        
        public AppShell(AppShellViewModel vm)
        {
            this.BindingContext = vm;
            InitializeComponent();
            Routing.RegisterRoute("gamePage", typeof(GameView));
            Routing.RegisterRoute("homePage", typeof(HomeView));
            Routing.RegisterRoute("loginPage", typeof(LoginView));
            Routing.RegisterRoute("registerPage", typeof(RegisterView));
            Routing.RegisterRoute("startGamePage", typeof(StartGameView));
            Routing.RegisterRoute("leaderboardPage", typeof(LeaderboardView));
            Routing.RegisterRoute("friendLeaderboardPage", typeof(FriendLeaderboardView));
            Routing.RegisterRoute("gameReportsPage", typeof(GameReportsView));
        }
    }
}
