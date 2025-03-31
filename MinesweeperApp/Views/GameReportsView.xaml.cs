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

}