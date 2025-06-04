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
            ResolveReportCommand = new Command((Object obj)=> ResolveReport(obj));
        }


        private async void ResolveReport(Object obj)
        {
            InServerCall = true;
            string result = await AppShell.Current.DisplayActionSheet("Accept report and remove game from leaderboard?", "cancel",null, "Accept report","Absolve report");
            if(result != null)
            {
                if (result == "Accept report")
                {
                    try
                    {
                        ServerResponse<GameReport> response = await service.AcceptGameReport((GameReport)obj);
                        if (response.Response)
                        {
                            await AppShell.Current.DisplayAlert("Report accepted successfuly", "\n" + "Game will not be shown on leaderboards", "Ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured while accepting report", "\n" + response.ResponseMessage, "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await AppShell.Current.DisplayAlert("Error occured while accepting report", "\n" + ex.Message, "Ok");
                    }
                }
                else if (result == "Absolve report")
                {
                    try
                    {
                        ServerResponse<GameReport> response = await service.AbsolveGameReport((GameReport)obj);
                        if (response.Response)
                        {
                            await AppShell.Current.DisplayAlert("Report absolved successfuly", "\n" + "Game will still be shown on leaderboards", "Ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured while absolving report", "\n" + response.ResponseMessage, "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await AppShell.Current.DisplayAlert("Error occured while absolving report", "\n" + ex.Message, "Ok");
                    }
                }
            }
            FillCollection();
            InServerCall = false;
        }
        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                GameReports = new();
                ServerResponse<List<object>> serverResponse = await service.GetCollectionByType("games.reports.admin");
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
                    await AppShell.Current.DisplayAlert("Error occured while loading reports", "\n" + serverResponse.ResponseMessage, "Ok");
                    InServerCall = false;
                    await AppShell.Current.GoToAsync("//leaderboardPage");
                }
            }
            catch(Exception ex) 
            {
                await AppShell.Current.DisplayAlert("Error occured while loading reports", "\n" + ex.Message, "Ok");
                InServerCall = false;
                await AppShell.Current.GoToAsync("//leaderboardPage");
            }
            InServerCall = false;
        }

    }
}
