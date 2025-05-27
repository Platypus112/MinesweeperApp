
namespace MinesweeperApp.Models
{
    public class AppUser
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? PicPath { get; set; }

        public string? FullPicPath {  get; set; }

        public bool IsAdmin { get; set; }

        public string? Description { get; set; }

        public AppUser() { }
        public AppUser(string name_, string email_, string password_, string picPath_,string fullPicPath_, bool isAdmin_, string description_)
        {
            Name = name_;
            Email = email_;
            Password = password_;
            PicPath = picPath_;
            FullPicPath = fullPicPath_;
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
            FullPicPath= appUser_.FullPicPath;
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
}
