using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class DataList
    {

        public string Name { get; set; }

        public bool Report { get; set; }

        public bool Games { get; set; }

        public bool User { get; set; }

        public bool AdminAccess { get; set; }

        public bool Personal { get; set; }

        public DataList() { }

        public DataList(string name_, bool report_, bool games_, bool user_, bool adminAccess_,bool personal_)
        {
            Name = name_;
            Report = report_;
            Games = games_;
            User = user_;
            AdminAccess = adminAccess_;
            Personal = personal_;
        }
    }
}
