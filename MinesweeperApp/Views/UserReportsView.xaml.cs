using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class UserReportsView : ContentPage
{
	public UserReportsView(UserReportsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}