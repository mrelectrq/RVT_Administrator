using RVT_Block_lib.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_Block_lib.Models
{
    public class NodeRegMessage
    {   
        public string IDVN { get; set; }
        public RegistrationMessage regMessage { get; set; }
        public List<Node>  NeighBoors { get; set; }
    }
}
