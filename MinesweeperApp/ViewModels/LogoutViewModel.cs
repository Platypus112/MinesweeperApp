using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.ViewModels
{
    public class LogoutViewModel:ViewModel
    {
        public LogoutViewModel(Service service_):base(service_)
        {
            Logout();
        }
        private async void Logout()
        {
            InServerCall = true;
            try
            {
                InServerCall = true;
                ServerResponse<AppUser> serverResponse = await service.Logout();
                if (serverResponse!=null)
                {
                    if (serverResponse.Response)
                    {
                        await AppShell.Current.DisplayAlert("Logout succeded", "Have a good day " +serverResponse.Content.Name, "ok");
                        await AppShell.Current.GoToAsync("///homePage");
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Logout failed", serverResponse.ResponseMessage, "ok");
                        await AppShell.Current.GoToAsync("///startGamePage");
                    }
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Logout failed", "", "ok");
                    await AppShell.Current.GoToAsync("///startGamePage");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Logout failed",ex.Message,"ok");
                await AppShell.Current.GoToAsync("///startGamePage");
                InServerCall = true;
            }
        }
    }
}
