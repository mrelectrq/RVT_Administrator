using RVT_Block_lib;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_A_BusinessLayer.Responses
{
    public class VoteLbResponse
    {
        public Block block { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedTime { get; set; }

    }
}
