using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class AdminSession
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string Ip { get; set; }
        public DateTime LastTime { get; set; }
    }
}
