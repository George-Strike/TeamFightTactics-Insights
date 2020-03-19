using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using TFTInsight.Data;
using TFTInsight.Models;

namespace TFTInsight.Services
{
    public interface IUserService
    {
        Task<SummonerModel> GetPlayerData(string region, string signedInUserName, string username, ApplicationDbContext context);
    }
    public class UserService : IUserService
    {
        public async Task<SummonerModel> GetPlayerData(string region, string signedInUserName, string summonerName, ApplicationDbContext context)
        {
            ApplicationUser currentUser = context.Users.Where(u => u.UserName == signedInUserName).FirstOrDefault();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri($"https://{region}.api.riotgames.com");
                HttpResponseMessage response = await client.GetAsync($"/tft/summoner/v1/summoners/by-name/{summonerName}?api_key=YourRiotAPIKey");
                response.EnsureSuccessStatusCode();

                string stringResult = await response.Content.ReadAsStringAsync();
                SummonerModel playerDetails = JsonConvert.DeserializeObject<SummonerModel>(stringResult);

                return playerDetails;
            }
        }
    }
}

