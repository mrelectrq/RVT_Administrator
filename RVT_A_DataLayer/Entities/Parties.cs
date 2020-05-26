using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class Parties
    {
        public Parties()
        {
            Blocks = new HashSet<Blocks>();
        }

        public int Idpart { get; set; }
        public string Party { get; set; }

        public virtual ICollection<Blocks> Blocks { get; set; }
    }
}
