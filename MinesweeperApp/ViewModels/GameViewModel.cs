using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.ViewModels
{
    public class GameViewModel:ViewModel
    {
        public List<Tile> Board { get { return board; } set {board=value; OnPropertyChanged();} }
        private List<Tile> board;
        public int Height { get { return height; } set { height = value; OnPropertyChanged(); } }
        private int height;
        public int Width { get { return width; } set { width = value; OnPropertyChanged(); } }
        private int width;
        private readonly Service service;
        private readonly Game game;
        public GameViewModel()
        {
            game = new Game(5, 5, 4);
            Width = 5;
            Height = 5;
            Board = game.GetBoardStateList();
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
