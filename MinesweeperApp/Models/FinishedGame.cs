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
        public DateTime Date {  get; set; }
        public double TimeInSeconds { get;set; }
        public string Time
        {
            get
            {
                return TimeSpan.FromSeconds(TimeInSeconds).ToString();
            }
        }
        public Difficulty difficulty { get; set; }

        public UserDTO User { get; set; }
    }
}
