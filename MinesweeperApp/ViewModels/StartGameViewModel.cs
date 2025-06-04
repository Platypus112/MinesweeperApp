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
    public class StartGameViewModel:ViewModel
    {
        private ObservableCollection<Difficulty> difficulties;
        public ObservableCollection<Difficulty> Difficulties {  get { return difficulties; } set { difficulties = value;OnPropertyChanged(); } }

        private Difficulty? selectedDifficulty;
        public Difficulty? SelectedDifficulty { get { return selectedDifficulty; } set {  selectedDifficulty = value;OnPropertyChanged(); ((Command)StartGameCommand).ChangeCanExecute(); } }
        
        public ICommand StartGameCommand { get; private set;}
        public StartGameViewModel(Service service_) : base(service_)
        {
            StartGameCommand = new Command(StartGame, ()=>SelectedDifficulty!=null);
            FillDifficulties();
        }

        public override void RefreshPage()
        {
            base.RefreshPage();
            InServerCall = true;
            FillDifficulties() ;
            InServerCall=false;
        }

        private async void FillDifficulties()
        {
            ServerResponse<List<Object>> difficultiesResponse = await service.GetCollectionByType("difficulties");

            if (difficultiesResponse != null && difficultiesResponse.Response)
            {
                Difficulties = new();
                foreach (Object item in difficultiesResponse.Content)
                {
                    Difficulties.Add((Difficulty)item);
                }
            }
        }

        private async void StartGame()
        {
            if (SelectedDifficulty != null)
            {
                Dictionary<string, object> data = new()
                {
                    {"Difficulty", SelectedDifficulty }
                };
                await AppShell.Current.GoToAsync("gamePage", data);
                SelectedDifficulty = null;
            }
        }
    }
}
