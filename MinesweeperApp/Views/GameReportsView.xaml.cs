using MinesweeperApp.ViewModels;
using Syncfusion.Maui.DataGrid;

namespace MinesweeperApp.Views;

public partial class GameReportsView : ContentPage
{
    public GameReportsView(GameReportsViewModel vm)
    {
        this.BindingContext = vm;
        InitializeComponent();
        CreateGrid();
    }

    private async void CreateGrid()
    {
        dataGrid.Columns.Insert(0, new DataGridTextColumn()
        {
            MappingName = "Status.Name",
            HeaderText = "Status",
        });
        dataGrid.Columns.Insert(0, new DataGridTextColumn()
        {
            MappingName = "Description",
            HeaderText = "Description",
        });
        
    }

}