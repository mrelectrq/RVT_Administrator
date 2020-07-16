using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RVT_A_BusinessLayer.Responses
{
    public class AdminAuthResp
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public string Token { get; set; }
        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(AdminAuthResp));

            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        public static AdminAuthResp Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(AdminAuthResp));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                var result = (AdminAuthResp)jsonSerializer.ReadObject(ms);
                return result;
            }
        }
    }
}
