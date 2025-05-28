using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class AdminView : ContentPage
{
	public AdminView(AdminViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
		((AdminViewModel)this.BindingContext).RefreshPage();
    }
}