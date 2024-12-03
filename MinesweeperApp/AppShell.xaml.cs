using MinesweeperApp.ViewModels;
using MinesweeperApp.Views;

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
        }
    }
}
