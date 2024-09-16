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
    public class GameViewModel:ViewModel
    {
        public ObservableCollection<Tile> Board { get { return board; } set {board=value; OnPropertyChanged();} }
        private ObservableCollection<Tile> board;
        public int Height { get { return height; } set { height = value; OnPropertyChanged(); } }
        private int height;
        public int Width { get { return width; } set { width = value; OnPropertyChanged(); } }
        private int width;
        private readonly Service service;
        private readonly Game game;
        public ICommand ClickTileCommand { get; private set; }
        public GameViewModel()
        {
            game = new Game(5, 5, 4);
            Width = 5;
            Height = 5;
            Board = new ObservableCollection<Tile>();
            UpdateCollection();
            
            ClickTileCommand = new Command(async (Object obj) => await ClickTile(obj));
            
        }
        public async Task UpdateCollection()
        {
            Board.Clear();
            List<Tile> tiles = game.GetBoardStateList();
            foreach (Tile tile in tiles)
            {
                Board.Add(tile);
            }

        }
        public async Task ClickTile(Object obj)
        {
            try
            {
                game.UnvailTile(((Tile)obj).DisplayDetails.x, ((Tile)obj).DisplayDetails.y);
                
            }
            catch
            {
                return;
            }
            
        }
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
