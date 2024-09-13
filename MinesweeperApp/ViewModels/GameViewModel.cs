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
        public Tile[,] Board { get { return board; }; set {board=value; OnPropertyChanged();} }
        private Tile[,] board;
        private readonly Service service;
        public GameViewModel()
        {

        }

    }
}
