﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class UserData
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? PicPath { get; set; }

        public string? FullPicPath { get; set; }

        public bool IsAdmin { get; set; }

        public string? Description { get; set; }

        public bool IsReported { get; set; }

        //public List<UserReport> Reports { get; set; }

        //public List<GameDataDTO> Games { get; set; }

        public UserData() { }
        public UserData(string name_, string email_, string password_, string picPath_,string fullPicPath_, bool isAdmin_, string description_,bool isReported_/*, List<UserReport> reports_*//*, List<GameDataDTO> games_*/)
        {
            Name = name_;
            Email = email_;
            Password = password_;
            PicPath = picPath_;
            FullPicPath=fullPicPath_;
            IsAdmin = isAdmin_;
            Description = description_;
            IsReported = isReported_;
            //Reports = reports_;
            //Games = games_;
        }
    }
}
