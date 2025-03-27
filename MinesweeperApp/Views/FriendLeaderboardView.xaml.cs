using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class FriendLeaderboardView : ContentPage
{
	public FriendLeaderboardView(FriendLeaderboardViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}