﻿using MinesweeperApp.Models;
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
    public class FriendRequestsViewModel:ViewModel
    {
        public ObservableCollection<FriendRequest> Requests { get { return requests; } set { requests = value;OnPropertyChanged(); } }
        private ObservableCollection<FriendRequest> requests;
        public ICommand DeclineFriendRequestCommand { get; private set; }
        public ICommand AcceptFriendRequestCommand { get; private set; }
        public FriendRequestsViewModel(Service service_):base(service_)
        {

            FillCollection();
            AcceptFriendRequestCommand = new Command((Object o) => AcceptFriendRequest(o));
            DeclineFriendRequestCommand = new Command((Object o) => DeclineFriendRequest(o));
        }
        public override void RefreshPage()
        {
            base.RefreshPage();
            InServerCall = true;
            FillCollection();
            InServerCall = false;
        }
        private async void DeclineFriendRequest(Object o)
        {
            InServerCall = true;
            FriendRequest f = (FriendRequest)o;
            string result = await AppShell.Current.DisplayActionSheet("Decline " + f.UserSending.Name + "'s friend request", "Cancel", null, "Decline");
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "Decline")
                {
                    try
                    {
                        ServerResponse<AppUser> response = await service.DeclineFriendRequest(f);
                        if (response.Response)
                        {
                            await AppShell.Current.DisplayAlert("Friend request declined successfuly", "\n" + f.UserSending.Name + "'s request has been declined", "Ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured while declining friend request", "\n" + response.ResponseMessage, "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await AppShell.Current.DisplayAlert("Error occured while declining friend request", "\n" + ex.Message, "Ok");
                    }
                }
            }
            FillCollection();
        }
        private async void AcceptFriendRequest(Object o)
        {
            InServerCall = true;
            FriendRequest f = (FriendRequest)o;
            string result = await AppShell.Current.DisplayActionSheet("Accept " + f.UserSending.Name + "'s friend request", "Cancel", null, "Add Friend");
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "Add Friend")
                {
                    try
                    {
                        ServerResponse<AppUser> response = await service.AcceptFriendRequest(f);
                        if (response.Response)
                        {
                            await AppShell.Current.DisplayAlert("Friend added successfuly", "\n" + f.UserSending.Name + " has been added to your friends", "Ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured while adding friend", "\n" + response.ResponseMessage, "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await AppShell.Current.DisplayAlert("Error occured while adding friend", "\n" + ex.Message, "Ok");
                    }
                }
            }
            FillCollection();
            InServerCall = false;
        }
        private async void FillCollection()
        {
            //add option to view users profile before accepting
            InServerCall = true;
            try
            {
                ServerResponse<List<Object>> listResponse = await service.GetCollectionByType("social");
                if (listResponse != null && listResponse.Response)
                {
                    Requests = new();
                    foreach (Object o in listResponse.Content)
                    {
                        FriendRequest f=(FriendRequest)o;
                        if(f.Status.Name=="pending")Requests.Add(f);
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
