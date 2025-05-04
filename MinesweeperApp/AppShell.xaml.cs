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
            Routing.RegisterRoute("socialPage", typeof(SocialPageView));
            Routing.RegisterRoute("friendRequestsPage", typeof(FriendRequestsView));
            Routing.RegisterRoute("blockedListPage", typeof(BlockedListView));
            Routing.RegisterRoute("profilePage", typeof(ProfileView));
            Routing.RegisterRoute("editProfilePage", typeof(EditProfileView));
        }
    }
}
