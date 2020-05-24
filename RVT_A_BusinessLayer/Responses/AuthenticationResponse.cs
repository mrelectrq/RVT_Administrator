using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RVT_A_BusinessLayer.Responses
{
    public class AuthenticationResponse
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string IDVN { get; set; }
        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(AuthenticationResponse));

            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        public static AuthenticationResponse Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(AuthenticationResponse));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                var result = (AuthenticationResponse)jsonSerializer.ReadObject(ms);
                return result;
            }
        }
    }
}
