using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinesweeperApp.Models;
using System.Collections.ObjectModel;
using MinesweeperApp.Services;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    public class BlockedListViewModel:ViewModel
    {
        public ObservableCollection<AppUser> BlockedUsers { get { return blockedUsers; } set { blockedUsers = value; OnPropertyChanged(); } }
        private ObservableCollection<AppUser> blockedUsers;
        public ICommand UnblockUserCommand { get; private set; }
        public BlockedListViewModel(Service service_) : base(service_)
        {

            FillCollection();
            UnblockUserCommand=new Command((Object o)=>UnblockUser(o));
        }
        public override void RefreshPage()
        {
            base.RefreshPage();
            InServerCall = true;
            FillCollection();
            InServerCall = false;
        }
        private async void UnblockUser(Object o)
        {
            InServerCall = true;

            try
            {
                AppUser toUnblock=(AppUser)o;
                ServerResponse<AppUser> response = await service.UnblockUser(toUnblock);
                if (response.Response)
                {
                    await AppShell.Current.DisplayAlert("User unblocked successfuly", toUnblock.Name + " is now unblocked, and will be able to send you friend requests or view your profile", "Ok");
                }
                else
                {
                    await AppShell.Current.DisplayAlert("Error occured while unblocking user", response.ResponseMessage, "Ok");
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured while unblocking user", ex.Message, "Ok");
            }

            FillCollection();
            InServerCall = false;
        }
        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                ServerResponse<List<Object>> listResponse = await service.GetCollectionByType("users.blocked");
                if (listResponse != null && listResponse.Response)
                {
                    BlockedUsers = new();
                    foreach (Object item in listResponse.Content)
                    {
                        UserData uf = (UserData)item;
                        BlockedUsers.Add(new(uf));
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
