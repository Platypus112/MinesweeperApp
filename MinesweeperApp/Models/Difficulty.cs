using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class Difficulty
    {
        public string Name { get; set; }
        public int height {  get; set; }
        public int width { get; set; }
        public int bombs { get; set; }
        public Difficulty()
        {
            Name = "";
        }
        
    }
}
