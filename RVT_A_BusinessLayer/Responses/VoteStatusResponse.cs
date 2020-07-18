using System;
using System.Collections.Generic;
using System.Text;
using RVT_A_BusinessLayer.BusinessModels;
using RVT_A_DataLayer.Entities;
using RVT_Block_lib.Models;

namespace RVT_A_BusinessLayer.Responses
{
    public class VoteStatusResponse
    {
        public List<VoteStatistics> TotalVotes{ get; set; }
        public int Votants { get; set; }
        public DateTime Time { get; set; }
        public int Population { get; set; }
        public int Pending { get; set; }
        public GenderStatistic GenderStatistics { get; set; }
    }
    
}
