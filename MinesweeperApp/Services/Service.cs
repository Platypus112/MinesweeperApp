using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinesweeperApp.Services
{
    public class Service
    {
        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "v6mq8s7g-5110.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://92b8rvh4-5074.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://92b8rvh4-5074.euw.devtunnels.ms/";
        #endregion

        public Service()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }


        public async Task<bool> ValidateEmail(string Email)
        {

            bool result = string.IsNullOrEmpty(Email);
            if (!result)
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            return result;
           
        }
    }
}
