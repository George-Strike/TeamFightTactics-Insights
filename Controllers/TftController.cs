using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TFTInsight.Models;
using TFTInsight.Data;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using TFTInsight.Services;

namespace TFTInsight.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class TftController : Controller
    {
        private ApplicationDbContext _context;
        private readonly IUserService _userService = new UserService();
        //Dependency inject within constructor
        public TftController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/Tft
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RetrieveData([FromBody] DataFromJS data)
        {
            string platform = data.region, username = data.username, signedInUserName = data.signedInUserName;
            var playerData = await Player(platform, username, signedInUserName);
            return playerData;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> CheckPlayerData([FromBody] DataFromJS data)
        {
            string signedInUserName = data.signedInUserName;
            ApplicationUser currentUser = _context.Users.Where(u => u.UserName == signedInUserName).FirstOrDefault();
            SummonerModel currentSummonerDetails = _context.SummonerModel.Where(u => u.Id == currentUser.Id).FirstOrDefault();
            SummonerModel playerDetails = await _userService.GetPlayerData(currentSummonerDetails.region, signedInUserName, currentSummonerDetails.name, _context);

            return Ok(new
            {
                ProfileIconId = playerDetails.profileIconId,
                Username = playerDetails.name,
                Region = playerDetails.region,
                Level = playerDetails.summonerLevel
            });
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Player(string region, string username, string signedInUserName)
        {
            // Context contains a list of the users
            // to get the current user, query the context based on the username sent from front-end
            ApplicationUser currentUser = _context.Users.Where(u => u.UserName == signedInUserName).FirstOrDefault();
            try
            {
                //Get Player Details
                SummonerModel playerDetails = await _userService.GetPlayerData(region, signedInUserName, username, _context);
                if (!string.IsNullOrEmpty(signedInUserName) && currentUser != null)
                {
                    // We need to check if the user has a record in the database for their summoner befpre we add
                    // a new record to the db
                    SummonerModel currentSummonerDetails = _context.SummonerModel.Where(u => u.Id == currentUser.Id).FirstOrDefault();
                    if (currentSummonerDetails == null || string.IsNullOrEmpty(currentSummonerDetails.Id))
                    {
                        AddToSummonerModel(signedInUserName, region, playerDetails, currentUser);
                    }
                }
                return Ok(new
                {
                    ProfileIconId = playerDetails.profileIconId,
                    Username = playerDetails.name,
                    Region = region,
                    Level = playerDetails.summonerLevel
                });
            }
            catch (HttpRequestException httpRequestException)
            {
                return BadRequest($"Error getting from API: {httpRequestException.Message}");
            }

        }
        public void AddToSummonerModel(string signedInUserName, string region, SummonerModel summonerDetails, ApplicationUser currentUser)
        {
            using (_context)
            {
                try
                {

                    var summoner = new SummonerModel()
                    {
                        Id = currentUser.Id,
                        SummonerId = summonerDetails.SummonerId,
                        name = summonerDetails.name,
                        puuid = summonerDetails.puuid,
                        accountId = summonerDetails.accountId,
                        profileIconId = summonerDetails.profileIconId,
                        revisionDate = summonerDetails.revisionDate,
                        summonerLevel = summonerDetails.summonerLevel,
                        region = region
                    };
                    _context.SummonerModel.Add(summoner);
                    _context.SaveChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e); //TODO: Set up looging to log this to a server. 
                }
            }
        }

    }


    public class DataFromJS
    {
        public string region { get; set; }
        public string username { get; set; }
        public string signedInUserName { get; set; }
    }
}
