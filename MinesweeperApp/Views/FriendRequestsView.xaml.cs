using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class FriendRequestsView : ContentPage
{
	public FriendRequestsView(FriendRequestsViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}