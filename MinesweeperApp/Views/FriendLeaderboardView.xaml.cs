using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class FriendLeaderboardView : ContentPage
{
	public FriendLeaderboardView(FriendLeaderboardViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((FriendLeaderboardViewModel)this.BindingContext).RefreshPage();
    }
}