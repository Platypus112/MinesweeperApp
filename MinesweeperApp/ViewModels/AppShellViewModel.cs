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
        public AppShellViewModel(Service service_) : base(service_) { }
    }
}
