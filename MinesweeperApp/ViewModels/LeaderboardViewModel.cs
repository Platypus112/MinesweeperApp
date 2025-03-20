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

        public ICommand ReportGameCommand { get; private set; }
        public ICommand ViewGameReportsCommand { get; private set; }
        public ICommand RemoveGameCommand { get; private set; }
        public LeaderboardViewModel(Service service_) : base(service_)
        {
            AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            Admin = service.LoggedUser!=null&&service.LoggedUser.IsAdmin;
            Type = "games";
            ViewGameReportsCommand = new Command((Object o) => ViewGameReports(o));
        }
        private async void ViewGameReports(Object o)
        {
            InServerCall = true;
            try
            {
                Dictionary<string, object> data = new();
                data.Add("game", o);

                await AppShell.Current.GoToAsync("gameReportsPage", data);

            }
            catch (Exception ex)
            {

            }
            InServerCall=false;
        }
        private async void RemoveGame(Object o)
        {
            InServerCall = true;
            try
            {
                GameData game= (GameData)o;
                if (game!=null)
                {
                    ServerResponse<GameData> response=await service.RemoveGame(game);

                    if (response != null && response.Response)
                    {
                        //great! it happend and worked
                        FillCollection();
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            InServerCall=false;
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
