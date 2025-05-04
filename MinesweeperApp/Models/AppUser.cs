
namespace MinesweeperApp.Models
{
    public class AppUser
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? PicPath { get; set; }

        public bool IsAdmin { get; set; }

        public string? Description { get; set; }

        public AppUser() { }
        public AppUser(string name_, string email_, string password_, string picPath_, bool isAdmin_, string description_)
        {
            Name = name_;
            Email = email_;
            Password = password_;
            PicPath = picPath_;
            IsAdmin = isAdmin_;
            Description = description_;
        }
        public AppUser(AppUser appUser_)
        {
            Id = appUser_.Id;
            Name = appUser_.Name;
            Email = appUser_.Email;
            Password = appUser_.Password;
            PicPath = appUser_.PicPath;
            IsAdmin = appUser_.IsAdmin;
            Description = appUser_.Description;
        }
        public AppUser(UserData userData_)
        {
            Id = userData_.Id;
            Name = userData_.Name;
            Email = userData_.Email;
            Password = userData_.Password;
            PicPath = userData_.PicPath;
            IsAdmin = userData_.IsAdmin;
            Description = userData_.Description;
        }

    }
    public class UserDTO
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string PicPath { get; set; } 

        public UserDTO() { }

        public UserDTO(string name_,string email_,string password_,string picPath_)
        {
            Name = name_;
            Email = email_; 
            Password = password_;
            PicPath = picPath_;
        }

        public UserDTO(AppUser user_)
        {
            Name = user_.Name;
            Email = user_.Email;
            Password = user_.Password;
            PicPath = user_.PicPath;
        }
    }

}
