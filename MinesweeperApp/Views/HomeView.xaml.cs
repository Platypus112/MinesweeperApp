using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel vm)
	{
        Application.Current.UserAppTheme = AppTheme.Dark;

        this.BindingContext = vm;
		InitializeComponent();
	}
}