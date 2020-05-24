using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class Regions
    {
        public Regions()
        {
            Votes = new HashSet<Votes>();
        }

        public int Idreg { get; set; }
        public int Region { get; set; }

        public virtual ICollection<Votes> Votes { get; set; }
    }
}
