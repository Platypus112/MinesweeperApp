using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class SocialPageView : ContentPage
{
	public SocialPageView(SocialPageViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((SocialPageViewModel)this.BindingContext).RefreshPage();
    }
}