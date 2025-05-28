using MinesweeperApp.ViewModels;
using Syncfusion.Maui.DataGrid;
using System.Runtime.CompilerServices;

namespace MinesweeperApp.Views;

public partial class LeaderboardView : ContentPage
{
	public LeaderboardView(LeaderboardViewModel vm)
	{
		this.BindingContext = vm;
		InitializeComponent();
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((LeaderboardViewModel)this.BindingContext).RefreshPage();
    }

}