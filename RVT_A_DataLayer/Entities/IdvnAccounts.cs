using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class IdvnAccounts
    {
        public string Idvn { get; set; }
        public string VnPassword { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? RegisterDate { get; set; }
        public string IpAddress { get; set; }
        public string StatusNumber { get; set; }
    }
}
