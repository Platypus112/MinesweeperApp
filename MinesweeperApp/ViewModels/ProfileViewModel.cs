using MinesweeperApp.Models;
using MinesweeperApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MinesweeperApp.ViewModels
{
    [QueryProperty(nameof(User),"user")]
    public class ProfileViewModel:ViewModel
    {
        public AppUser User { get { return user; }
            set
            {
                user = new(value);
                OnPropertyChanged();
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Email));
                OnPropertyChanged(nameof(Password));
                OnPropertyChanged(nameof(Description));
                OnPropertyChanged(nameof(IsLoggedUser));
            } 
        }   
        private AppUser user;
        public bool IsLoggedUser { get { return user.Email == service.LoggedUser.Email; } }

        private string photoURL;
        public string PhotoURL { get { return photoURL; } set { photoURL = value; OnPropertyChanged(); } }

        private string localPhotoPath;
        public string LocalPhotoPath { get { return localPhotoPath; } set { localPhotoPath = value; OnPropertyChanged(); } }

        public string Email { get { return User.Email; } set { User.Email = value; OnPropertyChanged(); } }
        public string Name { get { return User.Name; } set { User.Name = value;OnPropertyChanged(); } }
        public string Password { get { return User.Password; } set { User.Password=value;OnPropertyChanged(); } }
        public string Description { get { return User.Description; } set { User.Description=value;OnPropertyChanged(); } }

        private string errorMsg;
        public string ErrorMsg { get { return errorMsg; } set {  errorMsg = value; OnPropertyChanged(); } }

        private bool isViewingPassword;
        public bool IsViewingPassword { get { return isViewingPassword; } set { isViewingPassword = value; OnPropertyChanged(); OnPropertyChanged(nameof(IsNotViewingPassword)); } }
        public bool IsNotViewingPassword { get { return !isViewingPassword; } set { isViewingPassword = !value; OnPropertyChanged(); OnPropertyChanged(nameof(IsViewingPassword)); } }

        public ICommand ViewPasswordCommand { get; private set; }
        public ICommand EditProfileCommand { get; private set; }
        public ICommand UploadPhotoCommand { get; private set; }
        public ProfileViewModel(Service service_) : base(service_)
        {
            User = service.LoggedUser;
            ViewPasswordCommand = new Command(ViewPassword);
            EditProfileCommand = new Command(EditProfile);
            UploadPhotoCommand = new Command(UploadPhoto);
        }
        private async void ViewPassword()
        {
            if (IsViewingPassword) IsViewingPassword = false;
            else IsViewingPassword = true;
        }
        private async void EditProfile()
        {
            InServerCall = true;
            ErrorMsg=string.Empty;
            try
            {
                ErrorMsg += await service.ValidateUsername(Name);
                ErrorMsg += await service.ValidatePassword(Password);
                if (!string.IsNullOrEmpty(ErrorMsg)) return;

                ServerResponse<AppUser> response = await service.EditUser(User);
                if (response != null)
                {
                    if (response.Response)
                    {
                        if (!string.IsNullOrEmpty(LocalPhotoPath))
                        {
                            ServerResponse<AppUser?> updatedUserResponse = await service.UploadProfileImage(LocalPhotoPath);
                            if (updatedUserResponse == null|| !updatedUserResponse.Response)
                            {
                                await Shell.Current.DisplayAlert("User Data Was Saved BUT Profile image upload failed", "", "ok");
                            }
                            else
                            {
                                User.PicPath = updatedUserResponse.Content.PicPath;
                                UpdatePhotoURL(User.PicPath);
                            }
                            User = response.Content;
                            await AppShell.Current.DisplayAlert("Profile was saved successfully", "", "ok");

                        }
                        else
                        {
                            ErrorMsg = response.ResponseMessage;
                        }
                    }
                    else
                    {
                        ErrorMsg = "Failed to actualize changes. please try again";
                    }
                    InServerCall = false;

                }
            }
            catch (Exception ex)
            {
                InServerCall = false;
                ErrorMsg = ex.Message;
            }
        }
        private async void UploadPhoto()
        {
            try
            {
                var result = await MediaPicker.Default.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please select a photo",
                });

                if (result != null)
                {
                    this.LocalPhotoPath = result.FullPath;
                    this.PhotoURL = result.FullPath;
                }
            }
            catch (Exception ex)
            {
            }

        }
        private void UpdatePhotoURL(string virtualPath)
        {
            Random r = new Random();
            PhotoURL = service.GetImagesBaseAddress() + virtualPath + "?v=" + r.Next();
            LocalPhotoPath = "";
        }
    }
}
