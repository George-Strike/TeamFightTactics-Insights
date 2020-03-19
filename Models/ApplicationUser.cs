using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TFTInsight.Models
{
    public class ApplicationUser : IdentityUser
    {
        public SummonerModel SummonerModel { get; set; }
    }
}
