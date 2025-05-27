using Microsoft.Extensions.DependencyInjection;
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

        }
        public async void Logout()
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
                        AppShell.Current.FlyoutBehavior = FlyoutBehavior.Disabled;
                        await AppShell.Current.GoToAsync("///homePage");

                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Logout failed", serverResponse.ResponseMessage, "ok");
                        await AppShell.Current.GoToAsync("///gamePage");
                    }
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Logout failed", "", "ok");
                    await AppShell.Current.GoToAsync("///gamePage");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Logout failed",ex.Message,"ok");
                await AppShell.Current.GoToAsync("///gamePage");
                InServerCall = true;
            }
        }
    }
}
