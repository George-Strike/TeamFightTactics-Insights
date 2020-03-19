using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TFTInsight.Models
{
    public class SummonerModel
    {
        public int? profileIconId { get; set; }
        public string name { get; set; }
        public string puuid { get; set; }
        public int? summonerLevel { get; set; }
        public string accountId { get; set; }
        [JsonProperty("id")]
        public string SummonerId { get; set; }
        public long? revisionDate { get; set; }

        public string region { get; set; }
        public string Id { get; set; }
        public ApplicationUser ApplicationUser { get; set; }

    }
}
