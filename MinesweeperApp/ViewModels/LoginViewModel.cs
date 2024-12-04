using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    public class LoginViewModel:ViewModel
    {
        private string name;
        public string Name { get { return name; } set { name = value;OnPropertyChanged(); ((Command)LoginCommand).ChangeCanExecute(); } }

        private string password;
        public string Password { get { return password; } set { password = value; OnPropertyChanged();((Command)LoginCommand).ChangeCanExecute(); } }

        public ICommand LoginCommand { get; private set; }

        public LoginViewModel(Service service_):base(service_) 
        {
            Name = "";
            Password = "";
            LoginCommand = new Command(Login,()=>!string.IsNullOrEmpty(Name)&&!string.IsNullOrEmpty(Password));
        }

        private async void Login()
        {
            bool isEmail=await service.ValidateEmail(Name);
            if (isEmail)
            {

            }
            else
            {

            }

        } 
    }
}
