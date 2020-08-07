using RVT_A_BusinessLayer.BusinessModels;
using RVT_Block_lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_A_BusinessLayer.Responses
{
   public class RegionResponse
    {
        public string Name { get; set; }
        public List<VoteStatistics> TotalVotes { get; set; }
        public int Votants { get; set; }
        public DateTime Time { get; set; }
        public int Population { get; set; }
        public int Pending { get; set; }
        public GenderStatistic GenderStatistics { get; set; }
    }
}
