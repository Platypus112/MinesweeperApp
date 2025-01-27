using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class FriendRequest
    {
        public AppUser UserSending { get; set; }

        public AppUser UserRecieving { get; set; }

        public Status Status { get; set; }

        public bool Recieving { get; set; }

        public FriendRequest() { }
        public FriendRequest(AppUser userSending_, AppUser userRecieving_, Status status_,bool recieving_)
        {
            UserSending = userSending_;
            UserRecieving = userRecieving_;
            Status = status_;
            Recieving = recieving_;
        }
    }
}
