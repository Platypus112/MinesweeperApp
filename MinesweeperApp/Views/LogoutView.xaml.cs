using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class LogoutView : ContentPage
{
	public LogoutView(LoginViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}