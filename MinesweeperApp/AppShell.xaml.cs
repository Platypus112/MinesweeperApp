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
        }
    }
}
