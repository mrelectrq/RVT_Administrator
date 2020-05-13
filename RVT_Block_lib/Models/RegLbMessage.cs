using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RVTLibrary.Models
{
    public class RegLbMessage
    {
        [DataMember]
        public string IDVN { get; set; }
        [DataMember]
        public string VnPassword { get; set; }
        [DataMember]
        public string IDNP { get; set; }


        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(RegLbMessage));
            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        public static RegLbMessage Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(RegLbMessage));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                var result = (RegLbMessage)jsonSerializer.ReadObject(ms);
                return result;
            }
        }

    }
}
