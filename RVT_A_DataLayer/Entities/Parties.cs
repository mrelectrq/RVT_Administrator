using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class Parties
    {
        public Parties()
        {
            Votes = new HashSet<Votes>();
        }

        public int Idpart { get; set; }
        public string Party { get; set; }

        public virtual ICollection<Votes> Votes { get; set; }
    }
}
