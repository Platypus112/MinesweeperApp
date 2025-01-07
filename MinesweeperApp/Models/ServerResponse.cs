using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Models
{
    public class ServerResponse<T>
    {
        public bool Response { get; private set; }
        public string ResponseMessage { get; private set; }
        public T? Content { get; private set; }
        public ServerResponse() 
        {
            Response = false;
            ResponseMessage = string.Empty;
            Content = default(T);
        }
        public ServerResponse(string errorMessage_)
        {
            Response = false;
            ResponseMessage = errorMessage_;
            Content = default(T);
        }
        public ServerResponse(bool response_, string responseMessage_, T content_)
        {
            Response = response_;
            ResponseMessage = responseMessage_;
            Content = content_;
        }
    }
}
