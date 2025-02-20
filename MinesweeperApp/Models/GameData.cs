using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class GameData
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public double TimeInSeconds { get; set; }
        public Difficulty Difficulty { get; set; }
        public UserDTO User { get; set; }
        public List<GameReport> Reports { get; set; }

        public GameData() { }

        public GameData(int id_,DateTime date_, double timeInSeconds_, Difficulty difficulty_, UserDTO user_, List<GameReport> reports_)
        {
            Id= id_;
            Date = date_;
            TimeInSeconds = timeInSeconds_;
            Difficulty = difficulty_;
            User = user_;
            Reports = reports_;
        }
    }
}
