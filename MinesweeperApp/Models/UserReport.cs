using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class UserReport
    {
        public Status Status { get; set; }

        public string? Description { get; set; }

        public AppUser User { get; set; }

        public UserReport() { }
        public UserReport(Status status_, string description_, AppUser user_)
        {
            Status = status_;
            Description = description_;
            User = user_;
        }
    }
}
