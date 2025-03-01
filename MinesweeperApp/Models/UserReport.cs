﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class UserReport
    {
        public int Id { get; set; }
        public Status Status { get; set; }

        public string? Description { get; set; }

        public AppUser User { get; set; }

        public UserReport() { }
        public UserReport(int id_, Status status_, string description_, AppUser user_)
        {
            Id = id_;
            Status = status_;
            Description = description_;
            User = user_;
        }
    }
}
