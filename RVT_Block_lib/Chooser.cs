using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RVT_Block_lib
{
    [DataContract]
    public class Chooser
    {
        [DataMember]
        public string IDNP { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }
        [DataMember]
        public string Gender { get; set; }
        [DataMember]
        public DateTime? Birth_date { get; set; }
        [DataMember]
        public DateTime Vote_date { get; set; }
        [DataMember]
        public int Region { get; set; }
        [DataMember]
        public string Phone_Number { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        /// <summary>
        /// IDUN ID unic numeric a votantului
        /// </summary>
        /// 
    
        public string IDVN { get; set; }
  

        public string Serialize()
        {

            var jsonSerializer = new DataContractJsonSerializer(typeof(Chooser));

            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        public static Chooser Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Chooser));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var result = (Chooser)jsonSerializer.ReadObject(ms);
                return result;
            }
        }

    }
}
