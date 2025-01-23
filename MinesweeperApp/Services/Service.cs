using MinesweeperApp.Models;
using MinesweeperServer.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

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

        public async Task<ServerResponse<DataList>> GetDataList(string name)
        {
            string url = BaseAddress + "GetDataList";
            ServerResponse<DataList> responseResult = new();
            try
            {
                string json = JsonSerializer.Serialize(name);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    DataList toReturn= JsonSerializer.Deserialize<DataList>(result);
                    responseResult = new(true, await response.Content.ReadAsStringAsync(), toReturn);
                    return responseResult;
                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                    return responseResult;
                }
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<FinishedGame>> SendFinishedGame(Game game)
        {
            string url = BaseAddress + "RecordGame";
            ServerResponse<FinishedGame> responseResult = new();
            try
            {
                if (LoggedUser==null)
                {
                    responseResult = new("No user is logged in");
                    return responseResult;
                }

                FinishedGame finishedGame = new()
                {
                    Date = game.StartTime,
                    TimeInSeconds = (game.EndTime - game.StartTime).TotalSeconds,
                    difficulty = game.Diff,
                    User = new(LoggedUser)
                };

                string json = JsonSerializer.Serialize(finishedGame);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();

                    responseResult = new(true, await response.Content.ReadAsStringAsync(), finishedGame);
                    return responseResult;
                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                    return responseResult;
                }

            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<List<Difficulty>>> GetDifficulties()
        {
            string url = BaseAddress + "GetDifficulties";
            ServerResponse<List<Difficulty>> responseResult = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result=await response.Content.ReadAsStringAsync();
                    List<Difficulty> difficulties = JsonSerializer.Deserialize<List<Difficulty>>(result, options);
                    responseResult = new(true, result, difficulties);
                    return responseResult;
                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                    return responseResult;
                }
            }
            catch (Exception ex) 
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<AppUser>> Login(string name,string password)
        {
            string url = BaseAddress + "Login";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                string EmailValidation = await ValidateEmail(name);
                LoginInfo info = new()
                {
                    Password = password
                };
                
                if (string.IsNullOrEmpty(EmailValidation)) info.Email = name;
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
                    responseResult = new(true,await response.Content.ReadAsStringAsync(),LoggedUser);
                    return responseResult;
                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
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
                string usernameValidation = await ValidateUsername(username);
                if (!string.IsNullOrEmpty(usernameValidation))
                {
                    responseResult = new(usernameValidation);
                    return responseResult;
                }
                string emailValidation = await ValidateEmail(email);
                if (!string.IsNullOrEmpty(emailValidation))
                {
                    responseResult = new(emailValidation);
                    return responseResult;
                }
                string passwordValidation = await ValidatePassword(password);
                if (!string.IsNullOrEmpty(passwordValidation))
                {
                    responseResult = new(passwordValidation);
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
                    responseResult = new(true, await response.Content.ReadAsStringAsync(), LoggedUser);
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

        public async Task<string> ValidateEmail(string Email)
        {
            string result = "";
            if (string.IsNullOrEmpty(Email)) result = "Email can't be empty";
            if (string.IsNullOrEmpty(result))
            {
                //check if email is in the correct format using regular expression
                if (!System.Text.RegularExpressions.Regex.IsMatch(Email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                {
                    result = "Email isn't valid";
                }

            }
            return result;
           
        }
        public async Task<string> ValidateUsername(string Username)
        {
            string result="";
            if (string.IsNullOrEmpty(Username)) result="Username can't be empty" ;

            if (string.IsNullOrEmpty(result))
            {
                //check if email is in the correct format using regular expression
                if (Username.Length<=4)
                {
                    result = "Username must be longer than 4 characters";
                }
                else if(Username.Length > 20)
                {
                    result = "Username must be shorter than 20 characters";
                }
            }
            return result;
        }
        public async Task<string> ValidatePassword(string Password)
        {
            string result = "";
            if (string.IsNullOrEmpty(Password)) result = "Password can't be empty";
            if (string.IsNullOrEmpty(result))
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
                if (Password.Length <= 4)
                {
                    result = "Password must be longer than 4 characters";
                }
                else if(Password.Length > 20) 
                {
                    result = "Password must be shorter than 20 characters";
                }
                else if (!hasCaps)
                {
                    result = "Password must contain capital letters";
                }
                else if (!hasNumbers)
                {
                    result = "Password must contain numbers";
                }
            }
            return result;
        }
    }
}
