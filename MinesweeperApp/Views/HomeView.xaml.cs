using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class HomeView : ContentPage
{
	public HomeView(HomeViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}