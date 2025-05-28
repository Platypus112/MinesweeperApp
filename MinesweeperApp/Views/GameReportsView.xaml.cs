using MinesweeperApp.ViewModels;
using Syncfusion.Maui.DataGrid;

namespace MinesweeperApp.Views;

public partial class GameReportsView : ContentPage
{
    public GameReportsView(GameReportsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ((GameReportsViewModel)this.BindingContext).RefreshPage();
    }

}