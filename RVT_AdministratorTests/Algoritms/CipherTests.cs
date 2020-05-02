using Microsoft.VisualStudio.TestTools.UnitTesting;
using RVT_Block_lib.Algoritms;
using System;
using System.Collections.Generic;
using System.Text;

namespace RVT_Block_lib.Algoritms.Tests
{
    [TestClass()]
    public class CipherTests
    {
        [TestMethod()]


        //1K3FERr79hKY43h4BAXyeGt0HkYswC9nyySn75889DA=
        public void EncryptTest()
        {
            var encrypt = Cipher.Encrypt("1gahegdsagdsagdaghaegsdegafa", "555");

            Assert.Fail();
        }

        [TestMethod()]
        public void DecryptTest()
        {
            var dec = Cipher.Decrypt("1K3FERr79hKY43h4BAXyeGt0HkYswC9nyySn75889DA=", "555");


            Assert.Fail();
        }
    }
}