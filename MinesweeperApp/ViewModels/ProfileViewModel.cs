using MinesweeperApp.Models;
using MinesweeperApp.Services;
using MinesweeperApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    [QueryProperty(nameof(User2),"user")]
    public class ProfileViewModel:ViewModel
    {
        private List<GameData> allGames;
        private ObservableCollection<GameData> items;
        public ObservableCollection<GameData> Items { get { return items; } set { items = value; OnPropertyChanged(); } }

        private int diffIndex;
        public int DiffIndex { get { return diffIndex; } set { diffIndex = value; OnPropertyChanged(); Filter(); } }

        private ObservableCollection<Difficulty> difficultyList;
        public ObservableCollection<Difficulty> DifficultyList { get { return difficultyList; } set { difficultyList = value; OnPropertyChanged(); } }
        public AppUser User
        {
            get { return user; }
            set
            {
                user = new(value);
                user.FullPicPath = service.GetImagesBaseAddress() + user.PicPath;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(PhotoURL));
                OnPropertyChanged(nameof(IsLoggedUser));
                OnPropertyChanged(nameof(IsNotLoggedUser));
                FillCollection();
            }
        }
        private AppUser user;
        public AppUser User2 { private get; 
            set; }//used to check if logged usxer's profile or not

        public bool IsLoggedUser { get { return user.Email == service.LoggedUser.Email; } }
        public bool IsNotLoggedUser { get { return user.Email != service.LoggedUser.Email; } }

        public string PhotoURL { get { return User.FullPicPath; } set { User.FullPicPath = value; OnPropertyChanged(); } }
        public string Email { get { return User.Email; } set { User.Email = value; OnPropertyChanged(); } }
        public string Name { get { return User.Name; } set { User.Name = value; OnPropertyChanged(); } }
        public string Password { get { return User.Password; } set { User.Password = value; OnPropertyChanged(); } }
        public string Description { get { return User.Description; } set { User.Description = value; OnPropertyChanged(); } }

        private bool isRefreshing;
        public bool IsRefreshing { get { return isRefreshing; } set { isRefreshing = value; OnPropertyChanged(); ((Command)RefreshCommand).ChangeCanExecute(); } }


        public ICommand EditProfileCommand { get; private set; }
        public ICommand ReportUserCommand { get; private set; }
        public ICommand AddFriendCommand { get;private set; }
        public ICommand RefreshCommand { get; private set; }
        
        public ProfileViewModel(Service service_):base(service_,3) 
        {
            EditProfileCommand = new Command(EditProfile,()=>IsLoggedUser);
            ReportUserCommand=new Command(ReportUser,()=>!IsLoggedUser);
            AddFriendCommand=new Command(AddFriend,()=>!IsLoggedUser);
            RefreshCommand = new Command(FillCollection, () => !IsRefreshing);
            DiffIndex = 0;
            User = service.LoggedUser;
        }
        public override void RefreshPage()
        {
            base.RefreshPage();
            InServerCall = true;
            if (User2 == null) User = service.LoggedUser;
            else User = User2;
            InServerCall = false;
        }
        private async void AddFriend()
        {
            ServerResponse<FriendRequest> serverResponse;
            try
            {
                if (User!=null)
                {
                    InServerCall = true;
                    serverResponse = await service.SendFriendRequest(User.Name);
                    if (serverResponse != null)
                    {
                        if (serverResponse.Response)
                        {
                            await AppShell.Current.DisplayAlert("Friend request sent successfuly", "\n" + "Friend request to " + serverResponse.Content.UserRecieving.Name+" has been sent", "ok");
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
            IsRefreshing = true;
            InServerCall = true;
            try
            {
                ServerResponse<List<GameData>> listResponse;
                if (IsLoggedUser) listResponse = await service.GetUserGames(User.Email);
                else listResponse=await service.GetUserRecords(User.Email);

                if (listResponse != null && listResponse.Response)
                {
                    Items = new();
                    allGames = new();
                    foreach (GameData g in listResponse.Content)
                    {
                        Items.Add(g);
                        allGames.Add(g);
                    }
                }
                ServerResponse<List<Object>> difficultiesResponse = await service.GetCollectionByType("difficulties");

                if (difficultiesResponse != null && difficultiesResponse.Response)
                {
                    DifficultyList = new();
                    foreach (Object item in difficultiesResponse.Content)
                    {
                        DifficultyList.Add((Difficulty)item);
                    }
                }
            }
            catch (Exception ex)
            {
                InServerCall = false;
                IsRefreshing = false;

            }
            IsRefreshing = false;
            InServerCall = false;
        }
        private async void Filter()
        {
            InServerCall = true;
            try
            {
                if (diffIndex >= 0)
                {
                    Items = new();
                    foreach (GameData g in allGames)
                    {
                        if (DifficultyList[DiffIndex].Name == g.Difficulty.Name)
                        {
                            Items.Add(g);
                        }
                    }
                }
                InServerCall = false;
            }
            catch (Exception ex)
            {
                InServerCall = false;
            }
        }
        private async void EditProfile()
        {
            InServerCall = true;
            try
            {
                Dictionary<string, object> data = new();
                data.Add("user", User);
                await AppShell.Current.GoToAsync("editProfilePage", data);

            }
            catch (Exception ex)
            {
                await AppShell.Current.DisplayAlert("Error occured", "\n" + ex.Message, "ok");
            }
            InServerCall = false;
        }
        private async void ReportUser()
        {
            try
            {
                string description = await AppShell.Current.DisplayPromptAsync("File Report", "\n" + "Describe the problem with the user");
                if (!string.IsNullOrEmpty(description))
                {
                    InServerCall = true;
                    ServerResponse<UserReport> serverResponse = await service.ReportUser(User, description);
                    if (serverResponse != null)
                    {
                        if (serverResponse.Response)
                        {
                            await AppShell.Current.DisplayAlert("Report filed successfuly", "", "ok");
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
    }
}
