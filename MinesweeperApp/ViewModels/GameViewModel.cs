using Microsoft.Maui.Graphics.Text;
using MinesweeperApp.Models;
using MinesweeperApp.Services;
using Syncfusion.Maui.Core.Internals;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    [QueryProperty(nameof(Diff), "Difficulty")]
    public class GameViewModel:ViewModel
    {
        private ObservableCollection<Difficulty> difficulties;
        public ObservableCollection<Difficulty> Difficulties { get { return difficulties; } set { difficulties = value; OnPropertyChanged(); } }

       
        private Difficulty diff;
        public Difficulty Diff { get => diff; set { diff = value; CreateGameBoard(); OnPropertyChanged(); OnPropertyChanged(nameof(Board)); } }
        public ObservableCollection<Tile> Board { get { return board; } set {board=value; OnPropertyChanged();} }
        private ObservableCollection<Tile> board;
        public int Height { get { return height; } set { height = value; OnPropertyChanged(); } }
        private int height;
        public int Width { get { return width; } set { width = value; OnPropertyChanged(); } }
        private int width;
        public int Bombs {get { return bombs; }set { bombs=value; OnPropertyChanged(); } }
        private int bombs;
        public double GridHeight { get { return gridHeight; }set { gridHeight = value; SquareHeight = value / Height; OnPropertyChanged(); } }
        private double gridHeight;
        public double TileHeight { get { return squareHeight - 1; } set {  squareHeight = value+1; OnPropertyChanged(); } }
        public double SquareHeight { get { return squareHeight; } set { squareHeight = value; OnPropertyChanged(); } }
        private double squareHeight;
        public RowDefinitionCollection Rows { get { return rows; } set { rows = value; OnPropertyChanged(); } }
        private RowDefinitionCollection rows;
        public RowDefinitionCollection NavBarRows { get { return navBarRows; } set { navBarRows = value; OnPropertyChanged(); } }
        private RowDefinitionCollection navBarRows;
        public ColumnDefinitionCollection Columns { get { return columns; } set { columns = value; OnPropertyChanged(); } }
        private ColumnDefinitionCollection columns;
        public string Timer { get { return timer; } set {  timer = value; OnPropertyChanged(); } }
        private string timer;
        private IDispatcherTimer t;
        private Game game;
        private bool isFlagging;
        private bool notStarted;
        private bool gameFinished;

        public ICommand ClickTileCommand { get; private set; }
        public ICommand ToggleFlagCommand { get; private set; }
        public ICommand ToggleMineCommand { get; private set; }
        public GameViewModel(Service service_) : base(service_,0)
        {
            ClickTileCommand = new Command(async (Object obj) => await ClickTile(obj)/*, (object obj) => !gameFinished&!clickingRunning*/);
            ToggleFlagCommand = new Command(async () => await ToggleFlagging(), () => !isFlagging);
            ToggleMineCommand = new Command(async () => await ToggleFlagging(), () => isFlagging);
            InServerCall = true;
            t = App.Current.Dispatcher.CreateTimer();
            t.Stop();
            t.Interval = new TimeSpan(0, 0, 0, 0, 200);
            t.Tick += async (object sender, EventArgs e) => Timer = (DateTime.Now - game.StartTime).ToString().Substring(3, 5);
            notStarted = true;
            isFlagging = false;
            gameFinished = false;
            InServerCall = false;
        }

        public override void RefreshPage()
        {
            base.RefreshPage();
        }
        private async void CreateGameBoard()
        {
            Width = Diff.width;
            Height = Diff.height;
            Bombs = Diff.bombs;
            game = new Game(Width, Height, 0, null, null);
            ArranageGrid();
            UpdateCollection();
        }
        private async void ArranageGrid()
        {
            Columns = new();
            Rows = new();
            GridHeight = DeviceDisplay.Current.MainDisplayInfo.Height * 0.25;
            for (int i = 0; i < Width; i++)
            {
                Columns.Add(new ColumnDefinition());
            }
            for (int i = 0; i < Height; i++)
            {
                Rows.Add(new RowDefinition());
            }

        }
        private async Task ToggleFlagging()
        {
            if (isFlagging)
            {
                isFlagging = false;
            }
            else
            {
                isFlagging = true;
            }
            ((Command)ToggleFlagCommand).ChangeCanExecute();
            ((Command)ToggleMineCommand).ChangeCanExecute();
        }
        private async Task UpdateCollection()
        {
            Board=new ObservableCollection<Tile>(game.GetBoardStateList());

        }
        private async Task GameOver()
        {
            InServerCall = true;
            t.Stop();
            Timer = (DateTime.Now - game.StartTime).ToString().Substring(3, 5);
            gameFinished = true;
            ((Command)ClickTileCommand).ChangeCanExecute();
            await Task.Delay(5000);//let them have a bit of time to see
            if (game.HasWon)
            {
                try
                {
                    ServerResponse<FinishedGame> response = await service.SendFinishedGame(game);
                    if (response != null)
                    {
                        if (response.Response) await AppShell.Current.DisplayAlert("Game won", "\n" + "Game won in " + Timer + "\n\n Game recorded successfuly", "Ok");

                        else await AppShell.Current.DisplayAlert("Game won", "\n" + "Game won in " + Timer + "\n\nError occured while recording game:\n " + response.ResponseMessage, "Ok");
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Game won", "\n" + "Game won in " + Timer + "\n\nError ocurred while recording game", "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await AppShell.Current.DisplayAlert("Error occured", "\n" + "Game has not been uploaded", "Ok");
                }
            }
            else
            {
                await AppShell.Current.DisplayAlert("Game over", "\n" + "maybe next time!", "Ok");
            }
            InServerCall = false;
        }
        private async Task ClickTile(Object obj)
        {
            if (gameFinished) return;
            try
            {
                if(notStarted)
                {

                    game = new Game(Diff, ((Tile)obj).DisplayDetails.x, ((Tile)obj).DisplayDetails.y);
                    notStarted = false;
                    await UpdateCollection();
                    t.Start();
                }
                else
                {
                    if (await game.EasyDig(((Tile)obj).DisplayDetails.x, ((Tile)obj).DisplayDetails.y)) await GameOver();
                }
                if (!isFlagging)
                {
                    
                    if(await game.UnveilTile(((Tile)obj).DisplayDetails.x, ((Tile)obj).DisplayDetails.y)) await GameOver();
                    //AnimateDig(((Tile)obj), 2);
                }
                else game.FlagTile(((Tile)obj).DisplayDetails.x, ((Tile)obj).DisplayDetails.y);
                Bombs = game.Bombs;
            }
            catch
            {
                return;
            }
            
        }
    
    }
}
