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
		CreateGrid();
	}

	private async void CreateGrid()
	{
        dataGrid.Columns.Insert(0, new DataGridTextColumn()
        {
            MappingName = "Date",
            HeaderText = "Date",
        });
        dataGrid.Columns.Insert(0, new DataGridTextColumn()
        {
            MappingName = "Time",
            HeaderText = "Time",
        });
        dataGrid.Columns.Insert(0, new DataGridTextColumn()
        {
            MappingName = "Name",
            HeaderText = "Username",
            ColumnWidthMode = ColumnWidthMode.FitByCell,
            Width = 200,
        });
        dataGrid.Columns.Insert(0, new DataGridTextColumn()
        {
            MappingName = "difficulty.Name",
            HeaderText = "Difficulty",
        });
    }
}