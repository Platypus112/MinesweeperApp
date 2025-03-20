using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    [QueryProperty(nameof(Game),"game")]
    public class GameReportsViewModel:ViewModel
    {
        private GameData game;
        public GameData Game { get { return game; } set { game = value; OnPropertyChanged(); FillCollection(); } }

        private ObservableCollection<GameReport> gameReports;
        public ObservableCollection<GameReport> GameReports { get { return gameReports; } set { gameReports = value; OnPropertyChanged();} } 

        public ICommand ResolveReportCommand { get; private set; }

        public GameReportsViewModel(Service service_):base (service_)
        {
            AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;


        }
        private async void ResolveReport(Object obj)
        {
            string result = await AppShell.Current.DisplayActionSheet("Remove game from existence?", "cancel", null, "Remove game","Remove report");
            if(result != null)
            {
                if(result=="Remove Game")
                {
                    await service.RemoveGame(Game);
                    await AppShell.Current.DisplayAlert("Game removed successfuly", "", "Ok");
                    await AppShell.Current.GoToAsync("//leaderboardPage");
                }
                else if(result== "Remove Report")
                {

                    await AppShell.Current.DisplayAlert("Report removed successfuly", "", "Ok");
                    GameReports.Remove((GameReport)obj);
                }
            }
        }
        private async void FillCollection()
        {
            InServerCall = true;
            GameReports = new();
            ServerResponse<List<object>> serverResponse = await service.GetCollectionbyType("games.reports.admin");
            if(serverResponse != null&&serverResponse.Response)
            {
                List<GameReport> list= new List<GameReport>();
                foreach(Object report in serverResponse.Content)
                {
                    list.Add(((GameReport)report));
                }
                foreach(GameReport report in list)
                {
                    if (report.Game.Id == Game.Id) GameReports.Add(report);
                }
            }
            else
            {

            }
            InServerCall = false;
        }

    }
}
