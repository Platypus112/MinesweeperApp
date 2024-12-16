
namespace MinesweeperServer.DTO
{
    public class AppUser
    {
        public int? Id { get; set; }
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string? PicPath { get; set; }

        public bool IsAdmin { get; set; }

        public AppUser(string name_,string email_,string password_,string picPath_,bool isAdmin_)
        {
            Name = name_;
            Email = email_;
            Password = password_;
            PicPath = picPath_;
            IsAdmin = isAdmin_;
        }


    }
}
