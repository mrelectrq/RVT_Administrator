using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RVTLibrary.Responses
{
    public class RegLbResponse
    {

        /// <summary>
        /// Clasa aceasta este raspuns de catre LoadBalancer catre Administrator. Adica cind am primit raspuns de la nod (NodeRegResponse) convertam in RegLbResponse si transmitem la Admin
        /// </summary>
        public bool Status { get; set; }
        public string Message { get; set; }
        public DateTime ProcessedTime { get; set; }
        public string IDVN { get; set; }
        public string VnPassword { get; set; }

        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(RegLbResponse));
            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }

        public static RegLbResponse Deserialize(string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(RegLbResponse));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {

                var result = (RegLbResponse)jsonSerializer.ReadObject(ms);
                return result;
            }


        }
    }
}
