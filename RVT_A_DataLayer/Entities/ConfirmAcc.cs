using System;
using System.Collections.Generic;

namespace RVT_A_DataLayer.Entities
{
    public partial class ConfirmAcc
    {
        public string Idvn { get; set; }
        public string ConfirmKey { get; set; }
        public DateTime? DateConfirm { get; set; }
        public DateTime? DateRegKey { get; set; }
        public DateTime? DateExpirationKey { get; set; }
    }
}
