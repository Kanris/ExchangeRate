using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRateLibrary.Tests
{
    [TestClass]
    public class BankIDTest
    {
        [TestMethod]
        public void CreateBankID_NormalValue()
        {
            var expected = 0;
            var actual = BankID.Create(0);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateBankID_BorderValue()
        {
            var borderValue = int.MaxValue;

            var actual = BankID.Create(borderValue);

            Assert.AreEqual(borderValue, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankID_GreaterBorderValue()
        {
            Int64 borderValue = unchecked (int.MaxValue + 1);
            var actual = BankID.Create(Convert.ToInt32(borderValue));

            Assert.AreEqual(borderValue, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankID_LessThanZero()
        {
            var bankID = BankID.Create(-1);
        }

        [TestMethod]
        public void ConvertBankID_ToString()
        {
            var expected = "1";

            var actual = BankID.Create(1).ToString();

            Assert.AreEqual(expected, actual);
        }
    }
}
