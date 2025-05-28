using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

public partial class BlockedListView : ContentPage
{
	public BlockedListView(BlockedListViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((BlockedListViewModel)this.BindingContext).RefreshPage();
    }
}