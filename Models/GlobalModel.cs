using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApexInsight.Models
{
    public class Global
    {
        public string name { get; set; }
        public long uid { get; set; }
        public string platform { get; set; }
        public int level { get; set; }
        public int toNextLevelPercent { get; set; }
        public int internalUpdateCount { get; set; }
        public Rank rank { get; set; }
        public Battlepass battlepass { get; set; }
    }
    public class Battlepass
    {
        public string level { get; set; }
    }
}
