using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class FiscData
    {
        public string Idnp { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string Region { get; set; }
    }
}
