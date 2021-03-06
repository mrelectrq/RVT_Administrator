﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace RVT_Block_lib
{
    
    public class Block
    {
        public int BlockId { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public int PartyChoosed { get; set; }
        public int RegionChoosed { get; set; }
        public string Gender { get; set; }
        public int? YearToBirth { get; set; }
        public string Idbd { get; set; }
    }

}
