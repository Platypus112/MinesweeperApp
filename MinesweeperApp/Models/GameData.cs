using Microsoft.Maui.Platform;
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
        public AppUser User { get; set; }
        //public List<GameReport> Reports { get; set; }
        public string Time { get { return TimeSpan.FromSeconds(TimeInSeconds).ToString(); } private set { this.Time = value; } }

        public DateOnly DateOnly { get { return DateOnly.FromDateTime(Date.Value); } private set { this.DateOnly = value; } }
        public bool IsDeleted { get; set; }


        public GameData() { }

        public GameData(int id_, DateTime date_, double timeInSeconds_, Difficulty difficulty_, AppUser user_,bool isDeleted_/*, List<GameReport> reports_*/)
        {
            Id = id_;
            Date = date_;
            TimeInSeconds = timeInSeconds_;
            Difficulty = difficulty_;
            User = user_;
            IsDeleted = isDeleted_;
            //Reports = reports_;
        }
    }

    

}
