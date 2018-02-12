using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRateLibrary.Tests
{
    [TestClass]
    public class BankURITest
    {
        [TestMethod]
        public void CreateBankURI_Normal()
        {
            var expected = "https://www.ukrgasbank.com/";

            var actual = BankURI.Create(expected);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankURI_Empty()
        {
            var actual = BankURI.Create(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankURI_Null()
        {
            var actual = BankURI.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankURI_WrongURI()
        {
            var actual = BankURI.Create("2013.05.29_14:33:41");
        }

        [TestMethod]
        public void ConvertBankURI_ToString()
        {
            var expected = "https://www.ukrgasbank.com/";

            var actual = BankURI.Create(expected);

            Assert.AreEqual(expected, actual);
        }
    }
}
