using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class GameReport
    {
        public int Id { get; set; }
        public Status Status { get; set; }

        public string? Description { get; set; }

        public FinishedGame Game { get; set; }

        public GameReport() { }
        public GameReport(int id_,Status status_, string description_, FinishedGame game_)
        {
            Id = id_;
            Status = status_;
            Description = description_;
            Game = game_;
        }
    }
}
