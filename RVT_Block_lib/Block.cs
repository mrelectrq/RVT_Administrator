using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;

namespace RVT_Block_lib
{
    [DataContract]
    public class Block
    {
        public int ID { get; private set; }
        [DataMember]
        public DateTime CreatedOn { get; private set; }
        [DataMember]
        public string Hash { get; private set; }
        [DataMember]
        public string PreviousHash { get; private set; }
        [DataMember]
        public int Party_Choosed { get; private set; }
        [DataMember]
        public int Region_Choosed { get; private set; }
        [DataMember]
        public string ChooserName { get; private set; }

        /// <summary>
        ///  Constructor pentru Genesys Block
        /// </summary>
        public Block()
        {
            ID = 1;
            CreatedOn = DateTime.Parse("17.03.2020 00:00:00.000");
            PreviousHash = "111111";
            ChooserName = "Admin";
            Party_Choosed = 352;
            Region_Choosed = 1;
            var data = "initialize/data";
            Hash = getHash(data);
        }

        public Block(Chooser chooser, Block block)
        {
            
             if(string.IsNullOrWhiteSpace(Convert.ToString(chooser.IDNP)))
            {
                throw new ArgumentNullException($"Argument nevalidat", nameof(chooser.IDNP));
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(chooser.Name)))
            {
                throw new ArgumentNullException($"Argument nevalidat", nameof(chooser.Name));
            }

            if (string.IsNullOrWhiteSpace(Convert.ToString(chooser.Surname)))
            {
                throw new ArgumentNullException($"Argument nevalidat", nameof(chooser.Surname));
            }
            //        Format Result DateTime.Now.ToString("MM/dd/yyyy")
            // ---- De completat vereficarile
            ID = block.ID + 1;
            CreatedOn = DateTime.Now;
            PreviousHash = block.Hash;
            ChooserName = chooser.IDVN;
            Region_Choosed = chooser.Region;
            var data = getData(chooser);
            Hash = getHash(data);
        }

        private string getData(Chooser chooser)
        {
            var result = "";
            result += ID.ToString();
            result += CreatedOn.ToString("dd.MM.yyyy HH:mm:ss.fff");
            result += PreviousHash;
            result += ChooserName;
            result += chooser.IDNP;
            result += chooser.Name;
            result += chooser.Surname;
            result += chooser.Gender;
            result += chooser.Birth_date.ToString();
            result += chooser.Vote_date.ToString("dd.MM.yyyy HH: mm:ss.fff");
            result += chooser.Region.ToString();
            result += chooser.Phone_Number;
            result += chooser.Email;
            result += chooser.IDVN;
            return result;
        }

        private string getHash (string data)
        {
            var message = Encoding.ASCII.GetBytes(data);
            var hashString = new SHA256Managed();
            string hex = "";

            var hashValue = hashString.ComputeHash(message);
            foreach(byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public string Serialize()
        {

            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using (var ms = new MemoryStream())
            {
                jsonSerializer.WriteObject(ms, this);
                var result = Encoding.UTF8.GetString(ms.ToArray());
                return result;
            }

        }

        public static Block Deserialize (string json)
        {
            var jsonSerializer = new DataContractJsonSerializer(typeof(Block));

            using (var ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                var result = (Block)jsonSerializer.ReadObject(ms);
                return result;
            }
        }
    }

}
