using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class BlockedListView : ContentPage
{
	public BlockedListView(BlockedListViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
}