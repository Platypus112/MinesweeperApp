using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.ViewModels
{
    public class AppShellViewModel:ViewModel
    {
        private bool logged;
        public bool Logged { get { return logged; } set { logged = value; OnPropertyChanged(); OnPropertyChanged(nameof(NotLogged)); } }
        public bool NotLogged { get { return !logged; } set { logged = !value; OnPropertyChanged(); OnPropertyChanged(nameof(Logged)); } }
        
        public AppShellViewModel(Service service_) : base(service_)
        {
            Logged = false;
        }

    }
}
