using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_Block_lib.Requests
{
    public class BalancerResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public Block block { get; set; }
        public DateTime ProcessedTime { get; set; }
    }
}
