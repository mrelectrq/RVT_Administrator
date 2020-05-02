using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_A_BusinessLayer.Responses
{
    public class AuthenticationResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string IDVN { get; set; }
    }
}
