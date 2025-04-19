using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class FriendRequest
    {
        public LoginInfo UserSending { get; set; }

        public LoginInfo UserRecieving { get; set; }

        public Status Status { get; set; }

        public bool Recieving { get; set; }

        public FriendRequest() { }
        public FriendRequest(LoginInfo userSending_, LoginInfo userRecieving_, Status status_,bool recieving_)
        {
            UserSending = userSending_;
            UserRecieving = userRecieving_;
            Status = status_;
            Recieving = recieving_;
        }
    }
}
