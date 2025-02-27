using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.ViewModels
{
    [QueryProperty(nameof(Game),"game")]
    public class GameReportsViewModel:ViewModel
    {
        private GameData game;
        public GameData Game { get { return game; } set { game = value; OnPropertyChanged(); FillCollection(); } }

        private ObservableCollection<GameReport> gameReports;
        public ObservableCollection<GameReport> GameReports { get { return gameReports; } set { gameReports = value; OnPropertyChanged();} } 

        public GameReportsViewModel(Service service_):base (service_)
        {
            AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;


        }
        private async void FillCollection()
        {
            InServerCall = true;
            GameReports = new();
            foreach (GameReport report in game.Reports)
            {
                GameReports.Add(report);
            }
            InServerCall = false;
        }

    }
}
