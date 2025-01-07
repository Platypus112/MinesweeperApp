using MinesweeperApp.Models;
using MinesweeperServer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Java.Util.Jar.Attributes;

namespace MinesweeperApp.Services
{
    public class Service
    {
        #region with tunnel
        //Define the serevr IP address! (should be realIP address if you are using a device that is not running on the same machine as the server)
        private static string serverIP = "92b8rvh4-5074.euw.devtunnels.ms";
        private HttpClient client;
        private string baseUrl;
        public static string BaseAddress = "https://92b8rvh4-5074.euw.devtunnels.ms/api/";
        private static string ImageBaseAddress = "https://92b8rvh4-5074.euw.devtunnels.ms/";
        #endregion

        public AppUser? LoggedUser { get; private set; }

        public Service()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.CookieContainer = new System.Net.CookieContainer();

            this.client = new HttpClient(handler);
            this.baseUrl = BaseAddress;
        }
        public async Task<ServerResponse<AppUser>> Login(string name,string password)
        {
            string url = BaseAddress + "Login";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                bool IsEmail=await ValidateEmail(name);
                LoginInfo info = new()
                {
                    Password = password
                };
                
                if (IsEmail)info.Email = name;
                else info.Name = name;

                string json = JsonSerializer.Serialize(info);
                HttpContent content = new StringContent(json,Encoding.UTF8,"application/json");
                HttpResponseMessage response = await client.PostAsync(url,content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    LoggedUser = JsonSerializer.Deserialize<AppUser>(result,options);
                    responseResult = new(true,response.ReasonPhrase,LoggedUser);
                    return responseResult;
                }
                else
                {
                    responseResult = new(response.ReasonPhrase);
                    return responseResult;
                }
            }
            catch(Exception ex) 
            {
                responseResult = new(ex.Message);

                return responseResult;
            }
        }
        public async Task<ServerResponse<AppUser>> Register(string username,string email, string password)
        {
            string url = BaseAddress + "Register";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                if (await ValidateEmail(email))
                {
                    responseResult = new("Email isn't valid");
                    return responseResult;
                }
                if (await ValidateUsername(username))
                {
                    responseResult = new("Email isn't valid");
                    return responseResult;
                }
                if (await ValidatePassowrd(password))
                {
                    responseResult = new("Email isn't valid");
                    return responseResult;
                }
                LoginInfo info = new()
                {
                    Name=username,
                    Email=email,
                    Password = password
                };

                string json = JsonSerializer.Serialize(info);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    LoggedUser = JsonSerializer.Deserialize<AppUser>(result, options);
                    responseResult = new(true, response.ReasonPhrase, LoggedUser);
                    return responseResult;
                }
                else
                {
                    responseResult = new(response.ReasonPhrase);
                    return responseResult;
                }
            }
            catch(Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }

        public async Task<bool> ValidateEmail(string Email)
        {

            bool result = string.IsNullOrEmpty(Email);
            if (!result)
            {
                //check if email is in the correct format using regular expression
                if (System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
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
        public async Task<bool> ValidateUsername(string Username)
        {
            bool result = string.IsNullOrEmpty(Username);

            if (!result)
            {
                //check if email is in the correct format using regular expression
                if (Username.Length<=4&&Username.Length>20)
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
        public async Task<bool> ValidatePassword(string Password)
        {
            bool result = string.IsNullOrEmpty(Password);

            if (!result)
            {
                bool hasNumbers = false;
                foreach (char c in Password)
                {
                    if (c >= '0' && c <= '9')
                    {
                        hasNumbers = true; break;
                    }
                }
                bool hasCaps = false;
                foreach (char c in Password)
                {
                    if (c >= 'A' && c <= 'Z')
                    {
                        hasCaps = true; break;
                    }
                }
                //check if email is in the correct format using regular expression
                if (Password.Length <= 4 && Password.Length > 20&&hasCaps&&hasNumbers)
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
