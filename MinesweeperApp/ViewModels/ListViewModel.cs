using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MinesweeperApp.Models;
using System.Threading.Tasks;
using Syncfusion.Maui.DataGrid;

namespace MinesweeperApp.ViewModels
{
    [QueryProperty(nameof(Type),"type")]
    public class ListViewModel:ViewModel
    {
        private string type;
        public string Type { get { return type; } set { type = value; CreateDataGrid(); } }

        private bool admin;
        public bool Admin { get { return admin; } set {  admin = value; OnPropertyChanged(); } }

        private bool reports;
        public bool Reports { get { return reports; } set { reports = value; OnPropertyChanged(); } }

        private ObservableCollection<Object> items;
        public ObservableCollection<Object> Items { get { return items; } set { items = value;OnPropertyChanged(); } }
        
        private ColumnCollection columns;
        public ColumnCollection Columns { get { return columns; } set { columns = value;OnPropertyChanged(); } }


        public ListViewModel(Service service_) : base(service_)
        {
            
        }
        private async void CreateDataGrid()
        {
            GenerateColumns();
            if (Type.Contains("admin"))
            {
                Admin = true;
            }
            if (Type.Contains("reports"))
            {
                Reports = true;
            }
        }
        private async void GenerateColumns()
        {
            if (Type.Contains("users"))
            {
                Columns.Insert(0,new DataGridTextColumn()
                {
                    MappingName = "Name",
                    HeaderText = "Username",
                });
                Columns.Insert(0, new DataGridTextColumn()
                {
                    MappingName = "Description",
                    HeaderText = "Description",
                });
            }
            if (Type.Contains("games"))
            {
                Columns.Insert(0, new DataGridTextColumn()
                {
                    MappingName = "difficulty.Name",
                    HeaderText = "Difficulty",
                });
                Columns.Insert(0, new DataGridTextColumn()
                {
                    MappingName = "Name",
                    HeaderText = "Username",
                    ColumnWidthMode=ColumnWidthMode.FitByCell,
                    Width=200,
                });
                Columns.Insert(0, new DataGridTextColumn()
                {
                    MappingName = "Time",
                    HeaderText = "Time",
                });
                Columns.Insert(0, new DataGridTextColumn()
                {
                    MappingName = "Date",
                    HeaderText = "Date",
                });
                Columns.Insert(0, new DataGridTextColumn()
                {
                    MappingName = "",
                    HeaderText = "",
                });
            }
        }
    }
}
