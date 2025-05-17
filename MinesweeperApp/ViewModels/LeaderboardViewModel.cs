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

        private bool isAdmin;
        public bool IsAdmin { get { return isAdmin; } set { isAdmin = value; OnPropertyChanged(); } }

        private List<GameData> allGames;
        private ObservableCollection<GameData> items;
        public ObservableCollection<GameData> Items { get { return items; } set { items = value; OnPropertyChanged(); } }

        private int diffIndex;
        public int DiffIndex { get { return diffIndex; } set { diffIndex = value; OnPropertyChanged(); Filter(); } }

        private List<Difficulty> difficultyList;
        public List<Difficulty> DifficultyList { get { return difficultyList; } set { difficultyList = value; OnPropertyChanged(); } }

        private int timesIndex;
        public int TimesIndex { get { return timesIndex; } set { timesIndex = value; OnPropertyChanged(); Filter(); } }

        private List<string> timesList;
        public List<string> TimesList { get { return timesList; } set { timesList = value; OnPropertyChanged(); } }

        public ICommand ReportGameCommand { get; private set; }
        public ICommand ViewGameReportsCommand { get; private set; }
        public ICommand RemoveGameCommand { get; private set; }
        public ICommand ViewProfileCommand { get;private set; } 
        public LeaderboardViewModel(Service service_) : base(service_)
        {
            AppShell.Current.FlyoutBehavior = FlyoutBehavior.Flyout;
            IsAdmin = service.LoggedUser!=null&&service.LoggedUser.IsAdmin;
            FillCollection();
            TimesIndex = 0;
            DiffIndex = 0;
            ViewGameReportsCommand = new Command((Object o) => ViewGameReports(o));
            ViewProfileCommand=new Command((Object o) => ViewProfile(o));
            ReportGameCommand = new Command((Object o) => ReportGame(o));
            RemoveGameCommand = new Command((Object o) => RemoveGame(o),(Object o)=>IsAdmin);
        }
        private async void ViewProfile(Object o)
        {
            InServerCall = true;
            try
            {
                Dictionary<string, object> data = new();
                data.Add("user", o);
                await AppShell.Current.GoToAsync("profilePage", data);

            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", ex.Message, "ok");
            }
            InServerCall = false;
        }
        private async void ReportGame(Object o)
        {
            try
            {
                string description = await AppShell.Current.DisplayPromptAsync("File Report","Describe the problem with the game");
                if (!string.IsNullOrEmpty(description))
                {
                    InServerCall = true;
                    ServerResponse<GameReport> serverResponse = await service.ReportGame((GameData)o, description);
                    if(serverResponse != null)
                    {
                        if (serverResponse.Response)
                        {
                            await AppShell.Current.DisplayAlert("Report filed successfuly", "", "ok");
                            FillCollection();
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured", "Error occurred while filing report.\n" + serverResponse.ResponseMessage, "ok");
                        }
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Error occured", "Error occurred while filing report.", "ok");
                    }
                    InServerCall=false;
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "Error occurred while filing report.\n"+ex.Message, "ok");
            }
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
                await AppShell.Current.DisplayAlert("Error occured", ex.Message, "ok");
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
                        await AppShell.Current.DisplayAlert("Game removed", "game removed successfuly", "ok");
                        FillCollection();
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Error occured", "error occurred while trying to remove game\n" + response.ResponseMessage, "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "error occurred while trying to remove game\n" + ex.Message, "ok");
            }
            InServerCall=false;
        }
        private async void Filter()
        {
            InServerCall = true;
            try
            {
                if (timesIndex >= 0)
                {
                    Items = new();
                    foreach (GameData g in allGames)
                    {
                        if (DifficultyList[DiffIndex].Name == g.Difficulty.Name && (IsAdmin || !g.IsDeleted))
                        {
                            if (TimesList[TimesIndex] == "Daily" && g.Date.Value.AddDays(1) > DateTime.Now) Items.Add(g);
                            else if (TimesList[TimesIndex] == "Weekly" && g.Date.Value.AddDays(7) > DateTime.Now) Items.Add(g);
                            else if (TimesList[TimesIndex] == "Monthly" && g.Date.Value.AddMonths(1) > DateTime.Now) Items.Add(g);
                            else if (TimesList[TimesIndex] == "Yearly" && g.Date.Value.AddYears(1) > DateTime.Now) Items.Add(g);
                            else if (TimesList[TimesIndex]=="All time")Items.Add(g);
                        }
                    }
                }
                InServerCall = false;
            }
            catch (Exception ex)
            {
                InServerCall = false;
            }
        }
        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                ServerResponse<List<Difficulty>> difficultiesResponse = await service.GetDifficulties();
                if (difficultiesResponse != null && difficultiesResponse.Response)
                {
                    DifficultyList = difficultiesResponse.Content;
                }
                OnPropertyChanged(nameof(DifficultyList));
                TimesList = new();
                TimesList.Add("All time");
                TimesList.Add("Daily");
                TimesList.Add("Weekly");
                TimesList.Add("Monthly");
                TimesList.Add("Yearly");
                OnPropertyChanged(nameof(TimesList));
                ServerResponse<List<Object>> listResponse = await service.GetCollectionbyType("games");
                if (listResponse != null && listResponse.Response)
                {
                    Items = new();
                    allGames = new();
                    foreach (Object item in listResponse.Content)
                    {
                        GameData g = (GameData)item;
                        allGames.Add(g);
                        if (IsAdmin || !g.IsDeleted)
                        {
                            Items.Add(g);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                InServerCall = false;
            }
            InServerCall = false;
        }
    }
}
