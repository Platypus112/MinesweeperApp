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
    public class SocialPageViewModel:ViewModel
    {

        public ObservableCollection<AppUser> Friends { get { return friends; } set { friends = value;OnPropertyChanged(); } }
        private ObservableCollection<AppUser> friends;

        public ICommand RemoveFriendCommand { get; private set; }
        public ICommand BlockUserCommand { get; private set; }
        public ICommand SendFriendRequestCommand { get; private set; }
        public ICommand ViewFriendRequestsCommand { get; private set; }
        public ICommand ViewBlockedUsersCommand { get; private set; }

        public SocialPageViewModel(Service service_):base(service_,2) 
        {
            FillCollection();
            RemoveFriendCommand=new Command((Object o)=>RemoveFriend(o));
            BlockUserCommand=new Command(BlockUser);
            SendFriendRequestCommand = new Command(SendFriendRequest);
            ViewBlockedUsersCommand = new Command(ViewBlockedUsers);
            ViewFriendRequestsCommand=new Command(ViewFriendRequests);
        }
        public override void RefreshPage()
        {
            base.RefreshPage();
            InServerCall = true;
            FillCollection();
            InServerCall = false;
        }
        private async void ViewFriendRequests()
        {
            InServerCall = true;
            try
            {
                await AppShell.Current.GoToAsync("friendRequestsPage", true);
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "\n" + ex.Message, "ok");
            }
            InServerCall = false;
        }
        private async void ViewBlockedUsers()
        {
            InServerCall = true;
            try
            {
                await AppShell.Current.GoToAsync("blockedListPage", true);
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "\n" + ex.Message, "ok");
            }
            InServerCall = false;
        }
        private async void BlockUser()
        {
            InServerCall = true;
            string result = await AppShell.Current.DisplayPromptAsync("Block Users", "\n" + "Type the username of the user you want to block");
            if (!string.IsNullOrEmpty(result))
            {
                try
                {
                    ServerResponse<AppUser> response = await service.BlockUser(result);
                    if (response.Response)
                    {
                        await AppShell.Current.DisplayAlert("User blocked successfuly", "\n" + result + " is now blocked, and will not be able to send you friend requests", "Ok");
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Error occured while blocking user", "\n" + response.ResponseMessage, "Ok");
                    }
                }
                catch (Exception ex)
                {
                    await AppShell.Current.DisplayAlert("Error occured while blocking user", "\n" + ex.Message, "Ok");
                }

            }
            FillCollection();
            InServerCall = false;
        }
        private async void RemoveFriend(Object o)
        {
            InServerCall = true;
            AppUser uf=(AppUser)o;
            string result = await AppShell.Current.DisplayActionSheet("Remove " +uf.Name + " from your friends?", "Cancel", null, "Remove Friend");
            if (!string.IsNullOrEmpty(result))
            {
                if (result == "Remove Friend")
                {
                    try
                    {
                        ServerResponse<AppUser> response = await service.RemoveFriend(uf);
                        if (response.Response)
                        {
                            await AppShell.Current.DisplayAlert("Friend removed successfuly", "\n" + uf.Name + " is no longer your friend", "Ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured while removing friend", "\n" + response.ResponseMessage, "Ok");
                        }
                    }
                    catch (Exception ex)
                    {
                        await AppShell.Current.DisplayAlert("Error occured while removing friend", "\n" + ex.Message, "Ok");
                    }
                }
            }
            FillCollection();
            InServerCall = false;
        }
        private async void SendFriendRequest()
        {
            ServerResponse<FriendRequest> serverResponse;
            try
            {
                string name = await AppShell.Current.DisplayPromptAsync("Friend Request", "\n" + "Type the username of the user you want to add as a friend");
                if (!string.IsNullOrEmpty(name))
                {
                    InServerCall = true;
                    serverResponse = await service.SendFriendRequest(name);
                    if (serverResponse != null)
                    {
                        if (serverResponse.Response)
                        {
                            await AppShell.Current.DisplayAlert("Friend request sent successfuly", "", "ok");
                        }
                        else
                        {
                            await AppShell.Current.DisplayAlert("Error occured", "\n" + "Error occurred while trying to add friend.\n\n" + serverResponse.ResponseMessage, "ok");
                        }
                    }
                    else
                    {
                        await AppShell.Current.DisplayAlert("Error occured", "\n" + "Error occurred while trying to add friend.", "ok");
                    }
                    InServerCall = false;
                }
            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "\n" + "Error occurred while trying to add friend.\n\n" + ex.Message, "ok");
            }
        }
        private async void FillCollection()
        {
            InServerCall = true;
            try
            {
                ServerResponse<List<Object>> listResponse = await service.GetCollectionByType("users.social");
                if (listResponse != null && listResponse.Response)
                {
                    Friends = new();
                    foreach (Object item in listResponse.Content)
                    {
                        UserData uf = (UserData)item;
                        uf.FullPicPath = service.GetImagesBaseAddress() + uf.PicPath;
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
