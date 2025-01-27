using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class Status
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Status() { }
        public Status(int id_, string name_)
        {
            Id = id_;
            Name = name_;
        }
    }
}
