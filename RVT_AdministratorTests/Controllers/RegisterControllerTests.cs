using Microsoft.VisualStudio.TestTools.UnitTesting;
using RVT_A_BusinessLayer.Responses;
using RVT_Administrator.Controllers;
using RVT_Block_lib.Algoritms;
using RVT_Block_lib.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_Administrator.Controllers.Tests
{
    [TestClass()]
    public class RegisterControllerTests
    {
        [TestMethod()]
        public void RegActTest()
        {

            RegistrationMessage message = new RegistrationMessage();
            message.Email = "tiora100bass@gmail.com";
            message.Birth_date = DateTime.Parse("09/07/1997");
            message.Gender = "M";
            message.IDNP = "2007048011894";
            message.Phone_Number = "37367413773";
            message.Region = "Chisinau";
            message.RegisterDate = DateTime.Now;
            message.Surname = "Alexandru";
            message.Name = "Tiora";
            message.Ip_address = "5123512351";

            var controller = new RegisterController();
            var resp = controller.RegAct(message);

            var json_format = message.Serialize();

            //{"Birth_date":"\/Date(868395600000+0300)\/","Email":"tiora100bass@gmail.com","Gender":"M","IDNP":"2007048011894","Ip_address":"5123512351","Name":"Tiora","Phone_Number":"37367413773","Region":"Chisinau","RegisterDate":"\/Date(1585056255396+0200)\/","Surname":"Alexandru"}


            //Random random = new Random();
            //int number = random.Next(12452, 98762);
            //var array = Encoding.ASCII.GetBytes("gwasgwashawsg");
            //Crc32 crc32 = new Crc32();
            //String hash = crc32.Get(array).ToString();

            // "843105719"
            //var control = new RegistrationResponse { Message = "LOH", Status = true };
            //var message = new RegistrationMessage();
            //message.IDNP = "51231";
            //message.Ip_address = "gaesfea";
            //message.Name = "Cristi";

            //var controller = new RegisterController();
            //var resp = controller.RegAct(message);


            Assert.IsFalse(true);
        }
    }
}