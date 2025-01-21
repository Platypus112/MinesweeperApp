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

        private Difficulty selectedDifficulty;
        public Difficulty SelectedDifficulty { get { return selectedDifficulty; } set {  selectedDifficulty = value;OnPropertyChanged(); ((Command)StartGameCommand).ChangeCanExecute(); } }
        
        public ICommand StartGameCommand { get; private set;}
        public StartGameViewModel(Service service_) : base(service_)
        {
            StartGameCommand = new Command(StartGame, ()=>SelectedDifficulty!=null);
            FillDifficulties();
        }

        private async void FillDifficulties()
        {
            List<Difficulty>? difficultyList = (await service.GetDifficulties()).Content;
            Difficulties = new();
            if(difficultyList != null)
            {
                foreach(Difficulty d in difficultyList)
                {
                    Difficulties.Add(d);
                }
            }
        }

        private async void StartGame()
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("Difficulty", SelectedDifficulty);
            await AppShell.Current.GoToAsync("gamePage", data);
            SelectedDifficulty = null;
        }
    }
}
