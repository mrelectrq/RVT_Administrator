using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RVT_Block_lib.Models
{
    public class AuthenticationMessage
    {
        public string IDNP { get; set; }
        public string VnPassword { get; set; }

        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(AuthenticationMessage));

            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        public static AuthenticationMessage Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(AuthenticationMessage));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                var result = (AuthenticationMessage)jsonSerializer.ReadObject(ms);
                return result;
            }
        }
    }
}
