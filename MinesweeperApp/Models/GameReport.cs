using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class GameReport
    {
        public Status Status { get; set; }

        public string? Description { get; set; }

        public FinishedGame Game { get; set; }

        public GameReport() { }
        public GameReport(Status status_, string description_, FinishedGame game_)
        {
            Status = status_;
            Description = description_;
            Game = game_;
        }
    }
}
