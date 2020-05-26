using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_Block_lib.Requests
{
    public class VoteAdminResponse
    { 
        public bool VoteStatus { get; set; }

        public string Message { get; set; }
        public DateTime ProcessedTime { get; set; }
    }
}
