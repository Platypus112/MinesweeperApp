using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    public class HomeViewModel:ViewModel
    {
        public ICommand NavigateToLoginCommand { get; private set; }
        public ICommand NavigateToSignupCommand { get; private set; } 

        public HomeViewModel(Service service_) : base(service_)
        {
            NavigateToLoginCommand= new Command(NavigateToLogin);
            NavigateToSignupCommand= new Command(NavigateToSignup);
        }

        public async void NavigateToLogin()
        {

        }

        public async void NavigateToSignup()
        {

        }

    }
}
