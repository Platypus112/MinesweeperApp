using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    public class AdminViewModel:ViewModel
    {

        private ObservableCollection<UserData> items;
        public ObservableCollection<UserData> Items { get { return items; } set { items = value; OnPropertyChanged(); } }


        public ICommand ViewUserReportsCommand { get; private set; }
        public ICommand ReportUserCommand { get; private set; }
        public ICommand RemoveUserCommand { get; private set; }
        public AdminViewModel(Service service_) : base(service_,4)
        {
            FillCollection();
            ViewUserReportsCommand = new Command((Object o) => ViewUserReports(o));
            RemoveUserCommand = new Command((Object o) => RemoveUser(o));
            ReportUserCommand = new Command((Object o)=>ReportUser(o));
        }

        public override void RefreshPage()
        {
            base.RefreshPage();
            InServerCall = true;
            FillCollection();
            InServerCall = false;
        }
        private async void ReportUser(Object o)
        {
            try
            {
                string description = await AppShell.Current.DisplayPromptAsync("File Report", "\n" + "Describe the problem with the user");
                if (!string.IsNullOrEmpty(description))
                {
                    InServerCall = true;
                    ServerResponse<UserReport> serverResponse = await service.ReportUser(new((UserData)o), description);
                    if (serverResponse != null)
                    {
                        if (serverResponse.Response)
                        {
                            await AppShell.Current.DisplayAlert("Report filed successfuly", "", "ok");
                            FillCollection();
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured", "\n" + "Error occurred while filing report.\n\n" + serverResponse.ResponseMessage, "ok");
                        }
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Error occured", "\n" + "Error occurred while filing report.", "ok");
                    }
                    InServerCall = false;
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "\n" + "Error occurred while filing report.\n\n" + ex.Message, "ok");
            }
        }
        private async void ViewUserReports(Object o)
        {
            InServerCall = true;
            try
            {
                Dictionary<string, object> data = new();
                data.Add("user", o);
                await AppShell.Current.GoToAsync("userReportsPage", data);

            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", ex.Message, "ok");
            }
            InServerCall = false;
        }
        private async void RemoveUser(Object o)
        {
            InServerCall = true;
            try
            {
                UserData user = (UserData)o;
                if (user != null)
                {
                    ServerResponse<UserData> response = await service.RemoveUser(user);

                    if (response != null && response.Response)
                    {
                        await AppShell.Current.DisplayAlert("User removed", user.Name + " removed successfuly", "ok");
                        FillCollection();
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Error occured", "error occurred while trying to remove user\n" + response.ResponseMessage, "ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "error occurred while trying to remove user\n" + ex.Message, "ok");
            }
            InServerCall = false;
        }
        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                ServerResponse<List<Object>> listResponse = await service.GetCollectionByType("users");
                if (listResponse != null && listResponse.Response)
                {
                    Items = new();
                    foreach (Object item in listResponse.Content)
                    {
                        UserData u = (UserData)item;
                        u.FullPicPath = service.GetImagesBaseAddress() + u.PicPath;
                        Items.Add(u);
                    }
                }
            }
            catch (Exception ex)
            {
                InServerCall = false;
            }
            InServerCall = false;
        }
    }
}
