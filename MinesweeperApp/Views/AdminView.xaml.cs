using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class AdminView : ContentPage
{
	public AdminView(AdminViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}