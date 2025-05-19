using MinesweeperApp.Services;
using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class LogoutView : ContentPage
{
	public LogoutView(LogoutViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
		((LogoutViewModel)this.BindingContext).Logout();
    }

}