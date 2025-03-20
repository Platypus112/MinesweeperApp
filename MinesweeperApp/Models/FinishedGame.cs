using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class FinishedGame
    {
        public int Id { get; set; }
        public DateTime Date {  get; set; }
        public double TimeInSeconds { get;set; }
        public string Time
        {
            get
            {
                return TimeSpan.FromSeconds(TimeInSeconds).ToString();
            }
        }
        public Difficulty Difficulty { get; set; }

        public UserDTO User { get; set; }

        public FinishedGame()
        {

        }
        public FinishedGame(int id_, DateTime date_, double timeInSeconds_, Difficulty difficulty_,UserDTO user_)
        {
            Id = id_;
            Date = date_;
            TimeInSeconds = timeInSeconds_;
            Difficulty = difficulty_;
            User = user_;
        }
    }
}
