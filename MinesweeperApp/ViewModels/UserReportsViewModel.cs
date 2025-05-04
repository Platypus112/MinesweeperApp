using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    [QueryProperty(nameof(User), "user")]

    public class UserReportsViewModel:ViewModel
    {
        private UserData user;
        public UserData User { get { return user; } set { user = value; OnPropertyChanged(); FillCollection(); } }

        private ObservableCollection<UserReport> userReports;
        public ObservableCollection<UserReport> UserReports { get { return userReports; } set { userReports = value; OnPropertyChanged(); } }

        public ICommand ResolveReportCommand { get; private set; }

        public UserReportsViewModel(Service service_) : base(service_)
        {
            AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
            ResolveReportCommand = new Command((Object obj) => ResolveReport(obj));
        }
        private async void ResolveReport(Object obj)
        {
            InServerCall = true;
            string result = await AppShell.Current.DisplayActionSheet("Accept report and remove user from the leaderboards?", "cancel", null, "Accept report", "Absolve report");
            if (result != null)
            {
                if (result == "Accept report")
                {
                    try
                    {
                        ServerResponse<UserReport> response = await service.AcceptUserReport((UserReport)obj);
                        if (response.Response)
                        {
                            await AppShell.Current.DisplayAlert("Report accepted successfuly", "User will not be shown on leaderboards", "Ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured while accepting report", response.ResponseMessage, "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await AppShell.Current.DisplayAlert("Error occured while accepting report", ex.Message, "Ok");
                    }
                }
                else if (result == "Absolve report")
                {
                    try
                    {
                        ServerResponse<UserReport> response = await service.AbsolveUserReport((UserReport)obj);
                        if (response.Response)
                        {
                            await AppShell.Current.DisplayAlert("Report absolved successfuly", "", "Ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured while absolving report", response.ResponseMessage, "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await AppShell.Current.DisplayAlert("Error occured while absolving report", ex.Message, "Ok");
                    }
                }
            }
            FillCollection();
            InServerCall = false;
        }
        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                UserReports = new();
                ServerResponse<List<object>> serverResponse = await service.GetCollectionbyType("users.reports.admin");
                if (serverResponse != null && serverResponse.Response)
                {
                    List<UserReport> list = new List<UserReport>();
                    foreach (Object report in serverResponse.Content)
                    {
                        list.Add(((UserReport)report));
                    }
                    foreach (UserReport report in list)
                    {
                        if (report.User.Id == User.Id) UserReports.Add(report);
                    }
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Error occured while loading reports", serverResponse.ResponseMessage, "Ok");
                    InServerCall = false;
                    await AppShell.Current.GoToAsync("//adimnPage");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured while loading reports", ex.Message, "Ok");
                InServerCall = false;
                await AppShell.Current.GoToAsync("//adminPage");
            }
            InServerCall = false;
        }
    }
}
