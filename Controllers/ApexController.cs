using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ApexInsight.Models;

namespace ApexInsight.Controllers
{
    [Route("api/[controller]")]
    //[ApiController]
    public class ApexController : Controller
    {
       
        // GET: api/Apex
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RetrieveData([FromBody] DataFromJS data)
        {
            string platform = data.platform, username = data.username;
            var playerData = await Player(platform, username);
            return playerData;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Player(string platform, string username)
        {
            using (var client = new HttpClient())
            {
                try
                {
                    string platformType = platform, playerName = username;
                    client.BaseAddress = new Uri("http://premium-api.mozambiquehe.re");                
                    var response = await client.GetAsync($"/bridge?version=2&platform={platformType}&player={playerName}&auth=addyourauthkey");
                    response.EnsureSuccessStatusCode();

                    var stringResult = await response.Content.ReadAsStringAsync();
                    Dictionary<string, Global> playerDictionary = JsonConvert.DeserializeObject<Dictionary<string, Global>>(stringResult);
                    return Ok(new
                    {
                        Username = playerDictionary["global"].name,
                        Level = playerDictionary["global"].level,
                        NextLvlUpPercent = playerDictionary["global"].toNextLevelPercent,
                        Rank = playerDictionary["global"].rank,
                        Platform = playerDictionary["global"].platform
                    });
                }
                catch (HttpRequestException httpRequestException)
                {
                    return BadRequest($"Error getting from API: {httpRequestException.Message}");
                }
            }
        }     
    }


    public class DataFromJS
    {
        public string platform { get; set; }
        public string username { get; set; }
    }

    public class Realtime
    {
        public string lobbyState { get; set; }
        public int isOnline { get; set; }
        public int isInGame { get; set; }
        public int canJoin { get; set; }
        public int partyFull { get; set; }
        public string selectedLegend { get; set; }
    }

    public class ImgAssets
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Wraith
    {
        public int kills { get; set; }
        public int wins_season_1 { get; set; }
        public int kills_season_1 { get; set; }
        public ImgAssets ImgAssets { get; set; }
    }

    public class Selected
    {
        public Wraith Wraith { get; set; }
    }

    public class Kills
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class CreepingBarrageDamage
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason1
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason2
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data
    {
        public Kills kills { get; set; }
        public CreepingBarrageDamage creeping_barrage_damage { get; set; }
        public WinsSeason1 wins_season_1 { get; set; }
        public WinsSeason2 wins_season_2 { get; set; }
    }

    public class ImgAssets2
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Bangalore
    {
        public Data data { get; set; }
        public ImgAssets2 ImgAssets { get; set; }
    }

    public class Kills2
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }
    public class Top3
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class BeastOfTheHuntKills
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class KillsSeason1
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason22
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data2
    {
        public Kills2 kills { get; set; }
        public Top3 top_3 { get; set; }
        public BeastOfTheHuntKills beast_of_the_hunt_kills { get; set; }
        public KillsSeason1 kills_season_1 { get; set; }
        public WinsSeason22 wins_season_2 { get; set; }
    }

    public class ImgAssets3
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Bloodhound
    {
        public Data2 data { get; set; }
        public ImgAssets3 ImgAssets { get; set; }
    }

    public class Kills3
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Damage
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class GamesPlayed
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class DroppedItemsForSquadmates
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason23
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data3
    {
        public Kills3 kills { get; set; }
        public Damage damage { get; set; }
        public GamesPlayed games_played { get; set; }
        public DroppedItemsForSquadmates dropped_items_for_squadmates { get; set; }
        public WinsSeason23 wins_season_2 { get; set; }
    }

    public class ImgAssets4
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Lifeline
    {
        public Data3 data { get; set; }
        public ImgAssets4 ImgAssets { get; set; }
    }

    public class Kills4
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason24
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data4
    {
        public Kills4 kills { get; set; }
        public WinsSeason24 wins_season_2 { get; set; }
    }

    public class ImgAssets5
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Caustic
    {
        public Data4 data { get; set; }
        public ImgAssets5 ImgAssets { get; set; }
    }

    public class Kills5
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason12
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason25
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data5
    {
        public Kills5 kills { get; set; }
        public WinsSeason12 wins_season_1 { get; set; }
        public WinsSeason25 wins_season_2 { get; set; }
    }

    public class ImgAssets6
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Gibraltar
    {
        public Data5 data { get; set; }
        public ImgAssets6 ImgAssets { get; set; }
    }

    public class Kills6
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason26
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data6
    {
        public Kills6 kills { get; set; }
        public WinsSeason26 wins_season_2 { get; set; }
    }

    public class ImgAssets7
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Mirage
    {
        public Data6 data { get; set; }
        public ImgAssets7 ImgAssets { get; set; }
    }

    public class Kills7
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class PistolKills
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class BeaconsScanned
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason13
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason27
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data7
    {
        public Kills7 kills { get; set; }
        public PistolKills pistol_kills { get; set; }
        public BeaconsScanned beacons_scanned { get; set; }
        public WinsSeason13 wins_season_1 { get; set; }
        public WinsSeason27 wins_season_2 { get; set; }
    }

    public class ImgAssets8
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Pathfinder
    {
        public Data7 data { get; set; }
        public ImgAssets8 ImgAssets { get; set; }
    }

    public class Kills8
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason14
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class KillsSeason12
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason28
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data8
    {
        public Kills8 kills { get; set; }
        public WinsSeason14 wins_season_1 { get; set; }
        public KillsSeason12 kills_season_1 { get; set; }
        public WinsSeason28 wins_season_2 { get; set; }
    }

    public class ImgAssets9
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Wraith2
    {
        public Data8 data { get; set; }
        public ImgAssets9 ImgAssets { get; set; }
    }

    public class Kills9
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class WinsSeason29
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data9
    {
        public Kills9 kills { get; set; }
        public WinsSeason29 wins_season_2 { get; set; }
    }

    public class ImgAssets10
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Octane
    {
        public Data9 data { get; set; }
        public ImgAssets10 ImgAssets { get; set; }
    }

    public class WinsSeason210
    {
        public string name { get; set; }
        public int value { get; set; }
        public string key { get; set; }
    }

    public class Data10
    {
        public WinsSeason210 wins_season_2 { get; set; }
    }

    public class ImgAssets11
    {
        public string icon { get; set; }
        public string banner { get; set; }
    }

    public class Wattson
    {
        public Data10 data { get; set; }
        public ImgAssets11 ImgAssets { get; set; }
    }

    public class All
    {
        public Bangalore Bangalore { get; set; }
        public Bloodhound Bloodhound { get; set; }
        public Lifeline Lifeline { get; set; }
        public Caustic Caustic { get; set; }
        public Gibraltar Gibraltar { get; set; }
        public Mirage Mirage { get; set; }
        public Pathfinder Pathfinder { get; set; }
        public Wraith2 Wraith { get; set; }
        public Octane Octane { get; set; }
        public Wattson Wattson { get; set; }
    }

    public class Legends
    {
        public Selected selected { get; set; }
        public All all { get; set; }
    }

    public class Kills10
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class CreepingBarrageDamage2
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class WinsSeason15
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class WinsSeason211
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class Top32
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class BeastOfTheHuntKills2
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class KillsSeason13
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class Damage2
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class GamesPlayed2
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class DroppedItemsForSquadmates2
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class PistolKills2
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class BeaconsScanned2
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public class Kd
    {
        public int value { get; set; }
        public string name { get; set; }
    }

    public class Total
    {
        public Kills10 kills { get; set; }
        public CreepingBarrageDamage2 creeping_barrage_damage { get; set; }
        public WinsSeason15 wins_season_1 { get; set; }
        public WinsSeason211 wins_season_2 { get; set; }
        public Top32 top_3 { get; set; }
        public BeastOfTheHuntKills2 beast_of_the_hunt_kills { get; set; }
        public KillsSeason13 kills_season_1 { get; set; }
        public Damage2 damage { get; set; }
        public GamesPlayed2 games_played { get; set; }
        public DroppedItemsForSquadmates2 dropped_items_for_squadmates { get; set; }
        public PistolKills2 pistol_kills { get; set; }
        public BeaconsScanned2 beacons_scanned { get; set; }
        public Kd kd { get; set; }
    }

    public class MozambiquehereInternal
    {
        public string claimedBy { get; set; }
        public object APIAccessType { get; set; }
        public string ClusterID { get; set; }
    }

    public class RootObject
    {
        //public Global global { get; set; }
        public Realtime realtime { get; set; }
        public Legends legends { get; set; }
        public Total total { get; set; }
        public MozambiquehereInternal mozambiquehere_internal { get; set; }
    }

}
