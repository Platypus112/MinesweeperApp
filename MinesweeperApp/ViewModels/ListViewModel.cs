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

        private ObservableCollection<Object> items;
        public ObservableCollection<Object> Items { get { return items; } set { items = value;OnPropertyChanged(); } }
        
        private ColumnCollection columns;
        public ColumnCollection Columns { get { return columns; } set { columns = value;OnPropertyChanged(); } }

        private DataList dataList;

        public ListViewModel(Service service_) : base(service_)
        {
            
        }
        private async void CreateDataGrid()
        {
            ServerResponse<DataList> responseResult = await service.GetDataList(Type);
            if(responseResult != null)
            {
                if (responseResult.Response)
                {
                    dataList = responseResult.Content;

                }
                else
                {
                    return;
                }
            }
        }
        private async void GenerateColumns()
        {
            if (dataList.User)
            {
                Columns.Add(new DataGridTextColumn()
                {
                    MappingName = "Name",
                    HeaderText = "Username",
                });
                Columns.Add(new DataGridTextColumn()
                {
                    MappingName = "Description",
                    HeaderText = "Description",
                });
            }
            if (dataList.Games)
            {
                Columns.Add(new DataGridTextColumn()
                {
                    MappingName = "difficulty.Name",
                    HeaderText = "Difficulty",
                });
                Columns.Add(new DataGridTextColumn()
                {
                    MappingName = "Time",
                    HeaderText = "Time",
                });
                Columns.Add(new DataGridTextColumn()
                {
                    MappingName = "",
                    HeaderText = "",
                });
                Columns.Add(new DataGridTextColumn()
                {
                    MappingName = "",
                    HeaderText = "",
                });
                Columns.Add(new DataGridTextColumn()
                {
                    MappingName = "Name",
                    HeaderText = "Username",
                });
            }
        }
    }
}
