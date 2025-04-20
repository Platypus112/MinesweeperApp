using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinesweeperApp.Models;
using System.Collections.ObjectModel;
using MinesweeperApp.Services;

namespace MinesweeperApp.ViewModels
{
    public class BlockedListViewModel:ViewModel
    {
        public ObservableCollection<AppUser> Friends { get { return friends; } set { friends = value; OnPropertyChanged(); } }
        private ObservableCollection<AppUser> friends;
        public BlockedListViewModel(Service service_) : base(service_)
        {

        }

        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                ServerResponse<List<Object>> listResponse = await service.GetCollectionbyType("users.blocked");
                if (listResponse != null && listResponse.Response)
                {
                    Friends = new();
                    foreach (Object item in listResponse.Content)
                    {
                        UserData uf = (UserData)item;
                        Friends.Add(new(uf));
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
