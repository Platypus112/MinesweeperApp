using System.ComponentModel.DataAnnotations;

namespace MinesweeperApp.Models
{
    public class LoginInfo
    {
        public string Name { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }


        public LoginInfo():this(string.Empty, string.Empty, string.Empty)
        {

        }

        public LoginInfo(string name_, string email_, string password_)
        {
            Name = name_;
            Email = email_;
            Password = password_;
        }
        public LoginInfo(AppUser user)
        {
            Name=user.Name;
            Email = user.Email;
            Password = user.Password;
        }
    }
}
