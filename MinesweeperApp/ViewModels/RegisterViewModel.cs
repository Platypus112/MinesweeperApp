﻿using MinesweeperApp.Models;
using MinesweeperApp.Services;
using MinesweeperServer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using static Java.Util.Jar.Attributes;

namespace MinesweeperApp.ViewModels
{
    public class RegisterViewModel:ViewModel
    {
        private string email;
        public string Email { get { return email; } set { email = value; OnPropertyChanged(); } }

        private string username;
        public string Username { get { return username; } set {  username = value; OnPropertyChanged(); } }

        private string password;
        public string Password { get { return password; } set { password = value;OnPropertyChanged(); } }

        private string errorMSG;
        public string ErrorMSG { get { return errorMSG; } set { errorMSG = value; OnPropertyChanged(); } }

        private bool isViewingPassword;
        public bool IsViewingPassword { get { return isViewingPassword; } set { isViewingPassword = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsNotViewingPassword)); } }
        public bool IsNotViewingPassword { get { return !isViewingPassword; } set { isViewingPassword = !value; OnPropertyChanged(); OnPropertyChanged(nameof(IsViewingPassword)); } }

        public ICommand RegisterCommand { get;private set; }
        public ICommand ViewPasswordCommand { get;private set; }
        public RegisterViewModel(Service service_):base(service_) 
        {
            RegisterCommand = new Command(Register, () => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password));
            ViewPasswordCommand = new Command(ViewPassword);
            IsViewingPassword = false;
        }
        private async void ViewPassword()
        {
            if (IsViewingPassword) IsViewingPassword = false;
            else IsViewingPassword = true;
        }
        private async void Register()
        {
            ErrorMSG = "";
            InServerCall = true;
            ServerResponse<AppUser> response=await service.
        }
    }
}