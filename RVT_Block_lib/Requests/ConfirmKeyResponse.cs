using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_Block_lib.Requests
{
    public class ConfirmKeyResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Key { get; set; }

    }
}
