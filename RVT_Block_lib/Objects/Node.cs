using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace RVT_Block_lib.Objects
{
    public class Node
    {
        public IPAddress IP { get; set; }
        public string NodeId { get; set; }
        public string SoftwareVersion { get; set; }

    }
}
