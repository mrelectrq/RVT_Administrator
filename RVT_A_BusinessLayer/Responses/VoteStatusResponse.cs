using System;
using System.Collections.Generic;
using System.Text;
using RVT_A_BusinessLayer.BusinessModels;
using RVT_A_DataLayer.Entities;

namespace RVT_A_BusinessLayer.Responses
{
    public class VoteStatusResponse
    {
        public List<VoteStatistics> TotalVotes{ get; set; }
        public int Votants { get; set; }
        public DateTime Time { get; set; }
        public int Population { get; set; }
    }
    
}
