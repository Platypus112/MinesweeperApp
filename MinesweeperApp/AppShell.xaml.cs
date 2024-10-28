using MinesweeperApp.Views;

namespace MinesweeperApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("gamePage", typeof(GameView));
        }
    }
}
