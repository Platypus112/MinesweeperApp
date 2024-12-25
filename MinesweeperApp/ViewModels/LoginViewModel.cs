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

        private string errorMSG;
        public string ErrorMSG { get { return errorMSG; } set { errorMSG = value; OnPropertyChanged();} }

        private bool isViewingPassword;
        public bool IsViewingPassword { get { return isViewingPassword; } set { isViewingPassword = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsNotViewingPassword)); } }
        public bool IsNotViewingPassword { get { return !isViewingPassword; } set { isViewingPassword = !value; OnPropertyChanged(); OnPropertyChanged(nameof(IsViewingPassword)); } }

        public ICommand LoginCommand { get; private set; }
        public ICommand ViewPasswordCommand { get; private set; }

        public LoginViewModel(Service service_):base(service_) 
        {
            LoginCommand = new Command(Login,()=>!string.IsNullOrEmpty(Name)&&!string.IsNullOrEmpty(Password));
            ViewPasswordCommand = new Command(ViewPassword);
            IsViewingPassword = false;
            Name = "";
            Password = "";
        }
        private async void ViewPassword()
        {
            if (IsViewingPassword) IsViewingPassword = false;
            else IsViewingPassword = true;
        }

        private async void Login()
        {

            ErrorMSG = "";
            InServerCall = true;      
            bool succeeded = await service.Login(Name, Password)&&service.LoggedUser!=null;
            if (succeeded)
            {
                Logged = true;
                await AppShell.Current.GoToAsync("\\gamePage");
                InServerCall = false;
            }
            else
            {
                errorMSG = "Log in Failed";
                InServerCall = false;
            }
        } 
    }
}
