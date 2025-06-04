using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class StartGameView : ContentPage
{
	public StartGameView(StartGameViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((StartGameViewModel)this.BindingContext).RefreshPage();
    }
}