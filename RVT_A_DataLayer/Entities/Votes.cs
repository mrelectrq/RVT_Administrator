using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class Votes
    {
        public int Id { get; set; }
        public int PartyChoosed { get; set; }
        public int Region { get; set; }

        public virtual Parties PartyChoosedNavigation { get; set; }
        public virtual Regions RegionNavigation { get; set; }
    }
}
