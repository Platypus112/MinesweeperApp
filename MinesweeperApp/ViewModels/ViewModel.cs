using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Maui.Graphics.Text;
using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System.Data;
using System.Linq.Expressions;
using System.Windows.Input;
using Syncfusion.Maui.Core.Converters;
using System.Collections.ObjectModel;

namespace MinesweeperApp.ViewModels
{
    public class ViewModel:INotifyPropertyChanged
    {
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        protected readonly Service service;

        private bool inServerCall;
        public bool InServerCall { get { return inServerCall; } set { inServerCall = value; OnPropertyChanged();} }

        private static bool isAdmin;
        public bool IsAdmin { get { return isAdmin; } set { isAdmin = value; OnPropertyChanged(); } }

        private ObservableCollection<TabItem> tabs;
        public ObservableCollection<TabItem> Tabs { get { return tabs; } set { tabs = value; OnPropertyChanged(); } }

        public ICommand NavigateCommand { get; private set; }

        public ViewModel(Service service_)
        {
            NavigateCommand = new Command((Object o) => Navigate(o));
            this.service= service_;
            InServerCall= false;
            FillTabs();
        }
        private async void Navigate(Object o)
        {
            try
            {
                TabItem page=(TabItem)o;
                await AppShell.Current.GoToAsync("///"+page.Route);
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Navigation failed","try again","ok");
            }
        }
        protected async void FillTabs()
        {
            Tabs = new ObservableCollection<TabItem>();
            Tabs.Add(new TabItem("Game", "gamePage"));
            Tabs.Add(new TabItem("Leaderboard", "leaderboardPage"));
            Tabs.Add(new TabItem("Social", "socialPage"));
            Tabs.Add(new TabItem("Profile", "profilePage"));
            if(isAdmin) Tabs.Add(new TabItem("Admin", "adminPage"));
            Tabs.Add(new TabItem("Logout", "logoutPage"));
        }
    }
}
