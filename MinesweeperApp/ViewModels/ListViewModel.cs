using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using MinesweeperApp.Models;
using System.Threading.Tasks;
using Syncfusion.Maui.DataGrid;
using System.Windows.Input;

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

        private bool friends;
        public bool Friends { get { return friends; } set { friends = value; OnPropertyChanged(); } }

        private ObservableCollection<Object> items;
        public ObservableCollection<Object> Items { get { return items; } set { items = value;OnPropertyChanged(); } }
        
        private ColumnCollection columns;
        public ColumnCollection Columns { get { return columns; } set { columns = value;OnPropertyChanged(); } }
        
        public ICommand DeleteUserCommand { get; private set; }

        public ListViewModel(Service service_) : base(service_)
        {
            
        }

        private async void ReportUser(object user)
        {
            
        }

        private async void DeleteUser(object user)
        {
            UserData u =(UserData)user;

        }
        
        private async void CreateDataGrid()
        {
            InServerCall = true;
            if (Type.Contains("admin"))
            {
                Admin = true;
            }
            if (Type.Contains("reports"))
            {
                Reports = true;
            }
            if (!(type.Contains("users") || type.Contains("games")) && type.Contains("social"))
            {
                Friends = true;
            }
            ServerResponse<List<Object>> response=await service.GetCollectionByType(type);
            if (response != null)
            {
                if (response.Response)
                {
                    List<Object> list = response.Content;
                    Items = new ObservableCollection<object>();
                    foreach(object o in list)
                    {
                        Items.Add(o);
                    }
                }
                else
                {

                }
            }
            InServerCall = false;
        }
       
    }
}
