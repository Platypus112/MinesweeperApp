using Syncfusion.Maui.DataGrid;
using MinesweeperApp.ViewModels;

namespace MinesweeperApp.Views;

[QueryProperty(nameof(Type), "type")]
public partial class ListView : ContentPage
{
    private string type;
    public string Type { get { return type; } set { type = value; CreateDataGrid(); } }
    public ListView()
	{
		InitializeComponent();
	}

    private async void CreateDataGrid()
    {
        if (Type.Contains("users"))
        {
            dataGrid.Columns.Insert(0, new DataGridTextColumn()
            {
                MappingName = "Description",
                HeaderText = "Description",
            });
            dataGrid.Columns.Insert(0, new DataGridTextColumn()
            {
                MappingName = "Name",
                HeaderText = "Username",
            });
        }
        if (Type.Contains("games"))
        {
            dataGrid.Columns.Insert(0, new DataGridTextColumn()
            {
                MappingName = "",
                HeaderText = "",
            });
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
        if (!(type.Contains("users") || type.Contains("games")) && type.Contains("social"))
        {
            dataGrid.Columns.Insert(0, new DataGridTextColumn()
            {
                MappingName = "",
                HeaderText = "",
            });
            dataGrid.Columns.Insert(0, new DataGridTextColumn()
            {
                MappingName = "Description",
                HeaderText = "Description",
            });
            dataGrid.Columns.Insert(0, new DataGridTextColumn()
            {
                MappingName = "Name",
                HeaderText = "Username",
            });
        }
    }
}
