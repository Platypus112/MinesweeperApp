using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.ViewModels
{
    public class GameViewModel:ViewModel
    {
        public Tile[,] Board { get { return board; } set {board=value; OnPropertyChanged();} }
        private Tile[,] board;
        public int Height { get { return Height; } set { Height = value; OnPropertyChanged(); } }
        public int Width { get { return Width; } set { Width = value; OnPropertyChanged(); } }
        private readonly Service service;
        private readonly Game game;
        public GameViewModel()
        {
            game = new Game(5, 5, 4);
        }

    }
}
