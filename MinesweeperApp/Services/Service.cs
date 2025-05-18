using MinesweeperApp.Models;
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
        public string GetImagesBaseAddress()
        {
            return Service.ImageBaseAddress;
        }
        public async Task<ServerResponse<AppUser>> Logout()
        {
            string url = BaseAddress + "Logout";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    responseResult = new(true, result, LoggedUser);
                    LoggedUser = null;
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
        public async Task<ServerResponse<List<GameData>>> GetUserGames(string email)
        {
            string url = BaseAddress + "GetUserGames";
            ServerResponse<List<GameData>> responseResult = new();
            try
            {
                url += "?email=" + email;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    List<GameData> games = JsonSerializer.Deserialize<List<GameData>>(result, options);
                    responseResult = new(true, result, games);
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
        public async Task<ServerResponse<List<GameData>>> GetUserRecords(string email)
        {
            string url = BaseAddress + "GetRecords";
            ServerResponse<List<GameData>> responseResult = new();
            try
            {
                url += "?email=" + email;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    List<GameData> games = JsonSerializer.Deserialize<List<GameData>>(result, options);
                    responseResult = new(true, result, games);
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
        public async Task<ServerResponse<AppUser>> EditUser(AppUser user)
        {
            string url = BaseAddress + "EditUser";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                if (user == null)
                {
                    responseResult = new("No user given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(user);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    AppUser userResult= JsonSerializer.Deserialize<AppUser>(result);
                    responseResult = new(true, "Report accepted successfuly, user unable to aquire new friends", userResult);
                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<UserReport>> AcceptUserReport(UserReport r)
        {
            string url = BaseAddress + "AcceptUserReport";
            ServerResponse<UserReport> responseResult = new();
            try
            {
                if (r == null)
                {
                    responseResult = new("No report given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(r);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();

                    responseResult = new(true, "Report accepted successfuly, user unable to aquire new friends", r);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<UserReport>> AbsolveUserReport(UserReport r)
        {
            string url = BaseAddress + "AbsolveUserReport";
            ServerResponse<UserReport> responseResult = new();
            try
            {
                if (r == null)
                {
                    responseResult = new("No report given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(r);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();

                    responseResult = new(true, "Report Absolved successfuly", r);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<UserReport>> ReportUser(AppUser u, string Description)
        {
            string url = BaseAddress + "ReportUser";
            ServerResponse<UserReport> responseResult = new();
            try
            {
                if (u == null)
                {
                    responseResult = new("No user given");
                    return responseResult;
                }
                UserReport report = new()
                {
                    User=new AppUser(u),

                    Status = new Status()
                    {
                        Id = 1,
                        Name = "",
                    },
                    Description = Description,
                };
                string json = JsonSerializer.Serialize(report);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    UserReport userReportResult = JsonSerializer.Deserialize<UserReport>(result, options);
                    responseResult = new(true, "Report sent successfuly", userReportResult);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<UserReport>> RemoveUserReport(UserReport r)
        {
            string url = BaseAddress + "RemoveUserReport";
            ServerResponse<UserReport> responseResult = new();
            try
            {
                if (r == null)
                {
                    responseResult = new("No report given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(r);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    //GameReport gameReportResult = JsonSerializer.Deserialize<GameReport>(result, options);

                    responseResult = new(true, "Report removed successfuly", r);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<UserData>> RemoveUser(UserData u)
        {
            string url = BaseAddress + "RemoveUser";
            ServerResponse<UserData> responseResult = new();
            try
            {
                if (u == null)
                {
                    responseResult = new("No user given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(u);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();

                    responseResult = new(true, "user removed successfuly", u);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<AppUser>> UnblockUser(AppUser user)
        {
            string url = BaseAddress + "UnblockUser";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                string json = JsonSerializer.Serialize(user);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string resultString = await response.Content.ReadAsStringAsync();
                    responseResult = new(true, "User unblocked successfuly", user);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<List<FriendRequest>>> GetAllFriendRequests()
        {
            string url = BaseAddress + "GetAllFriendRequests";
            ServerResponse<List<FriendRequest>> responseResult = new();
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    List<FriendRequest> requests = JsonSerializer.Deserialize<List<FriendRequest>>(result, options);
                    responseResult = new(true, result, requests);
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
        public async Task<ServerResponse<AppUser>> RemoveFriend(AppUser user)
        {
            string url = BaseAddress + "RemoveFriend";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                LoginInfo toBlock = new()
                {
                    Name = user.Name,
                };
                string json = JsonSerializer.Serialize(toBlock);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string resultString = await response.Content.ReadAsStringAsync();
                    AppUser removedFriend = JsonSerializer.Deserialize<AppUser>(resultString, options);
                    responseResult = new(true, "Friend removed successfuly", removedFriend);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<AppUser>> BlockUser(string username)
        {
            string url = BaseAddress + "BlockUser";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                LoginInfo toBlock = new()
                {
                    Name = username,
                };
                string json = JsonSerializer.Serialize(toBlock);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string resultString = await response.Content.ReadAsStringAsync();
                    AppUser recieved = JsonSerializer.Deserialize<AppUser>(resultString, options);
                    responseResult = new(true, "User blocked successfuly", recieved);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<AppUser>> DeclineFriendRequest(FriendRequest request)
        {
            string url = BaseAddress + "DeclineFriendRequest";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                string json = JsonSerializer.Serialize(request);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string resultString = await response.Content.ReadAsStringAsync();
                    AppUser recieved = JsonSerializer.Deserialize<AppUser>(resultString, options);
                    responseResult = new(true, "Friend request declined successfuly", recieved);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<AppUser>> AcceptFriendRequest(FriendRequest request)
        {
            string url = BaseAddress + "AcceptFriendRequest";
            ServerResponse<AppUser> responseResult = new();
            try
            {
                string json = JsonSerializer.Serialize(request);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string resultString = await response.Content.ReadAsStringAsync();
                    AppUser recieved = JsonSerializer.Deserialize<AppUser>(resultString, options);
                    responseResult = new(true, "Friend added successfuly", recieved);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<FriendRequest>> SendFriendRequest(string username)
        {
            string url = BaseAddress + "SendFriendRequest";
            ServerResponse<FriendRequest> responseResult = new();
            try
            {
                FriendRequest request = new()
                {
                    UserSending=new(){
                        Name=this.LoggedUser.Name,
                    },
                    UserRecieving= new()
                    {
                        Name = username,
                    },
                    Status = new()
                    {
                        Name="aaa",
                        Id=0,
                    },
                };

                string json = JsonSerializer.Serialize(request);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    //GameReport gameReportResult = JsonSerializer.Deserialize<GameReport>(result, options);

                    responseResult = new(true, result, request);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<GameReport>> AcceptGameReport(GameReport r)
        {
            string url = BaseAddress + "AcceptGameReport";
            ServerResponse<GameReport> responseResult = new();
            try
            {
                if (r == null)
                {
                    responseResult = new("No report given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(r);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    //GameReport gameReportResult = JsonSerializer.Deserialize<GameReport>(result, options);

                    responseResult = new(true, "Report accepted successfuly, game removed from leaderboards", r);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<GameReport>> AbsolveGameReport(GameReport r)
        {
            string url = BaseAddress + "AbsolveGameReport";
            ServerResponse<GameReport> responseResult = new();
            try
            {
                if (r == null)
                {
                    responseResult = new("No report given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(r);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    //GameReport gameReportResult = JsonSerializer.Deserialize<GameReport>(result, options);

                    responseResult = new(true, "Report Absolved successfuly", r);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<GameReport>> ReportGame(GameData g,string Description)
        {
            string url = BaseAddress + "ReportGame";
            ServerResponse<GameReport> responseResult = new();
            try
            {
                if (g == null)
                {
                    responseResult = new("No game given");
                    return responseResult;
                }
                GameReport report = new()
                {
                    Game = new FinishedGame()
                    {
                        Id= g.Id,
                        User=new(g.User),
                        Difficulty=g.Difficulty,
                        Date=g.Date.Value,
                        TimeInSeconds=g.TimeInSeconds,
                    },
                    Status=new Status()
                    {
                        Id=1,
                        Name="",
                    },
                    Description = Description,
                };
                string json = JsonSerializer.Serialize(report);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url,content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    GameReport gameReportResult = JsonSerializer.Deserialize<GameReport>(result, options);
                    responseResult = new(true, "Report sent successfuly", gameReportResult);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<GameReport>> RemoveGameReport(GameReport r)
        {
            string url = BaseAddress + "RemoveGameReport";
            ServerResponse<GameReport> responseResult = new();
            try
            {
                if (r == null)
                {
                    responseResult = new("No report given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(r);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    //GameReport gameReportResult = JsonSerializer.Deserialize<GameReport>(result, options);

                    responseResult = new(true, "Report removed successfuly", r);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch (Exception ex)
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<GameData>> RemoveGame(GameData g)
        {
            string url = BaseAddress + "RemoveGame";
            ServerResponse<GameData> responseResult = new();
            try
            {
                if (g==null)
                {
                    responseResult = new("No game given");
                    return responseResult;
                }
                string json = JsonSerializer.Serialize(g);
                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(url, content);
                if (response.IsSuccessStatusCode)
                {
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    //GameData gameResult= JsonSerializer.Deserialize<GameData>(result, options);

                    responseResult = new(true, "game removed successfuly", g);

                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }
                return responseResult;
            }
            catch(Exception ex) 
            {
                responseResult = new(ex.Message);
                return responseResult;
            }
        }
        public async Task<ServerResponse<List<Object>>> GetCollectionbyType(string type)
        {
            string url = BaseAddress + "GetCollection";
            ServerResponse<List<Object>> responseResult = new();
            try
            {
                if (LoggedUser == null)
                {
                    responseResult = new("No user is logged in");
                    return responseResult;
                }
                url += "?type=" + type;
                HttpResponseMessage response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    List<object> listResult = new List<object>();
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    string result = await response.Content.ReadAsStringAsync();
                    if (!(type.Contains("users") || type.Contains("games")) && type.Contains("social"))
                    {
                        List<FriendRequest> list =JsonSerializer.Deserialize<List<FriendRequest>>(result, options);
                        foreach(FriendRequest f in list)
                        {
                            listResult.Add(f);
                        }
                    }
                    if (type.Contains("games"))
                    {
                        if (type.Contains("reports") && type.Contains("admin"))
                        {
                            List<GameReport> list = JsonSerializer.Deserialize<List<GameReport>>(result, options);
                            foreach (GameReport r in list)
                            {
                                listResult.Add(r);
                            }
                        }
                        else if (type.Contains("social"))
                        {
                            List<GameData> list = JsonSerializer.Deserialize<List<GameData>>(result, options);
                            foreach (GameData g in list)
                            {
                                listResult.Add(g);
                            }
                        }
                        else
                        {
                            List<GameData> list = JsonSerializer.Deserialize<List<GameData>>(result, options);
                            foreach (GameData g in list)
                            {
                                listResult.Add(g);
                            }
                        }
                    }
                    if (type.Contains("users"))
                    {
                        if (type.Contains("reports") && type.Contains("admin"))
                        {
                            List<UserReport> list = JsonSerializer.Deserialize<List<UserReport>>(result, options);
                            foreach (UserReport r in list)
                            {
                                listResult.Add(r);
                            }
                        }
                        else
                        {
                            List<UserData> list = JsonSerializer.Deserialize<List<UserData>>(result, options);
                            foreach (UserData u in list)
                            {
                                listResult.Add(u);
                            }
                        }
                    }
                    responseResult=new(true, await response.Content.ReadAsStringAsync(),listResult);
                }
                else
                {
                    responseResult = new(await response.Content.ReadAsStringAsync());
                }


                return responseResult;
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
                    Difficulty = game.Diff,
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
        public async Task<ServerResponse<AppUser?>> UploadProfileImage(string imagePath)
        {
            //Set URI to the specific function API
            string url = $"{this.baseUrl}uploadprofileimage";
            try
            {
                //Create the form data
                MultipartFormDataContent form = new MultipartFormDataContent();
                var fileContent = new ByteArrayContent(File.ReadAllBytes(imagePath));
                form.Add(fileContent, "file", imagePath);
                //Call the server API
                HttpResponseMessage response = await client.PostAsync(url, form);
                //Check status
                if (response.IsSuccessStatusCode)
                {
                    //Extract the content as string
                    string resContent = await response.Content.ReadAsStringAsync();
                    //Desrialize result
                    JsonSerializerOptions options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    AppUser? result = JsonSerializer.Deserialize<AppUser>(resContent, options);
                    return new(true, "Successfuly uploaded image", result);
                }
                else
                {
                    return new(response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                return new(ex.Message);
            }
        }


        public async Task<string> ValidateEmail(string Email)
        {
            string result = string.Empty;
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
            string result=string.Empty;
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
            string result = string.Empty;
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
