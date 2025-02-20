using MinesweeperApp.Models;
using MinesweeperApp.Services;
using Syncfusion.Maui.DataGrid;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    public class LeaderboardViewModel:ViewModel
    {
        private string type;
        public string Type { get { return type; } set { type = value; FillCollection(); } }

        private bool admin;
        public bool Admin { get { return admin; } set { admin = value; OnPropertyChanged(); } }

        private ObservableCollection<GameData> items;
        public ObservableCollection<GameData> Items { get { return items; } set { items = value; OnPropertyChanged(); } }

        private ColumnCollection columns;
        public ColumnCollection Columns { get { return columns; } set { columns = value; OnPropertyChanged(); } }

        public ICommand ReportGameCommand { get; private set; }
        public ICommand ViewGameReportsCommand { get; private set; }
        public ICommand RemoveGameCommand { get; private set; }
        public LeaderboardViewModel(Service service_) : base(service_)
        {
            Admin = service.LoggedUser!=null&&service.LoggedUser.IsAdmin;
            Type = "games";
        }
        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                ServerResponse<List<Object>> listResponse=await service.GetCollectionbyType(Type);
                if (listResponse != null&&listResponse.Response)
                {
                    Items = new();
                    foreach(Object item in listResponse.Content)
                    {
                        Items.Add((GameData)item);
                    }
                }
            }
            catch(Exception ex) 
            {

            }
            InServerCall= false;
        }
    }
}
