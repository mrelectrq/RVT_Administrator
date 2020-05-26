using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class Regions
    {
        public Regions()
        {
            Blocks = new HashSet<Blocks>();
        }

        public int Idreg { get; set; }
        public string Region { get; set; }

        public virtual ICollection<Blocks> Blocks { get; set; }
    }
}
