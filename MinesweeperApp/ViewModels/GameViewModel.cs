using Microsoft.Maui.Graphics.Text;
using MinesweeperApp.Models;
using MinesweeperApp.Services;
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
        private Difficulty diff;
        public Difficulty Diff { get => diff; set { diff = value; OnPropertyChanged();CreateGameBoard(); } }
        public ObservableCollection<Tile> Board { get { return board; } set {board=value; OnPropertyChanged();} }
        private ObservableCollection<Tile> board;
        public int Height { get { return height; } set { height = value; OnPropertyChanged(); } }
        private int height;
        public int Width { get { return width; } set { width = value; OnPropertyChanged(); } }
        private int width;
        public int Bombs {get { return bombs; }set { bombs=value; OnPropertyChanged(); } }
        private int bombs;
        public RowDefinitionCollection Rows { get { return rows; } set { rows = value; OnPropertyChanged(); } }
        private RowDefinitionCollection rows;
        public ColumnDefinitionCollection Columns { get { return columns; } set { columns = value; OnPropertyChanged(); } }
        private ColumnDefinitionCollection columns;
        public string Timer { get { return timer; } set {  timer = value; OnPropertyChanged(); } }
        private string timer;
        private readonly IDispatcherTimer t;
        private Game game;
        private bool isFlagging;
        private bool notStarted;
        private bool gameFinished;
        private bool clickingRunning;

        public ICommand ClickTileCommand { get; private set; }
        public ICommand ToggleFlagCommand { get; private set; }
        public ICommand ToggleMineCommand { get; private set; }
        public GameViewModel(Service service_) : base(service_)
        {
            t = App.Current.Dispatcher.CreateTimer();
            t.Stop();
            t.Interval = new TimeSpan(0, 0, 0, 0, 200);
            t.Tick += async (object sender, EventArgs e) => Timer =  (DateTime.Now - game.StartTime).ToString().Substring(3,5);
            notStarted = true;
            Board = new ObservableCollection<Tile>();
            gameFinished = false;
            clickingRunning = false;
       
            ClickTileCommand = new Command(async (Object obj) => await ClickTile(obj)/*, (object obj) => !gameFinished&!clickingRunning*/) ;
            ToggleFlagCommand = new Command(async () => await ToggleFlagging(), () => !isFlagging);
            ToggleMineCommand = new Command(async () => await ToggleFlagging(), () => isFlagging);
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
            if (game.HasWon)await service.SendFinishedGame(game);
            InServerCall = false;

        }
        private async Task ClickTile(Object obj)
        {
            clickingRunning = true;
            if (gameFinished) return;
            try
            {
                if(notStarted)
                {

                    //game = await new Task<Game>(() => new Game(Width, Height, Bombs, ((Tile)obj).DisplayDetails.x, ((Tile)obj).DisplayDetails.y));
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
                clickingRunning = false;
                Bombs = game.Bombs;
            }
            catch
            {
                return;
            }
            
        }
        //private async Task AnimateDig(Tile tile,double secs)
        //{
        //    IDispatcherTimer timer = App.Current.Dispatcher.CreateTimer();
        //    timer.Stop();
        //    int tick = ((int)(secs / 0.05));
        //    timer.Interval = new TimeSpan(0, 0, 0, 0, tick);
        //    timer.Tick += async (Object sender, EventArgs e) =>
        //    {
        //        if (tile.DisplayDetails.Scale != 0) tile.DisplayDetails.Scale -= 0.05;
        //        else
        //        {
        //            tile.DisplayDetails.Scale = 1;
        //            timer.Stop();
        //        }
        //    };
            
        //}
        //private DataTable ConvertBoardToDatatable()
        //{
        //    DataTable result = new DataTable();
        //    for(int i = 0; i < Width; i++)
        //    {
        //        DataColumn column = new DataColumn();
        //        column.DataType=typeof(Tile);
        //        column.Unique = false;
        //        column.ColumnName = i.ToString();
        //        result.Columns.Add(column);
        //    }
        //    for (int i = 0; i < Height; i++)
        //    {
        //        DataRow row = result.NewRow();
        //        for (int j = 0; j < Width; j++)
        //        {
        //            row[j.ToString()] = game.GetBoardState()[i,j];
        //        }
        //        result.Rows.Add(row);
        //    }
        //    return result;
        //}
    
    }
}
