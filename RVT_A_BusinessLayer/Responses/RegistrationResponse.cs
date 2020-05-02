using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_A_BusinessLayer.Responses
{
    public class RegistrationResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Password { get; set; }
        public string IDVN { get; set; }
        public string Email { get; set; }
        public string ConfirmKey { get; set; }

    }
}
