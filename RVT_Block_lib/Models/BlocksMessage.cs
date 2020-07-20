using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace RVT_Block_lib.Models
{
    public class BlocksMessage
    {
        public bool Status { get; set; }
        public string Serialize()
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(BlocksMessage));
            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }
        }
    }
}
