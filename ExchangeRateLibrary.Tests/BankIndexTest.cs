using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRateLibrary.Tests
{
    [TestClass]
    public class BankIndexTest
    {
        [TestMethod]
        public void CreateBankIndex_NormalValue()
        {
            var expected = 0;

            var actual = BankIndex.Create(0);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateBankIndex_BorderValue()
        {
            var borderValue = int.MaxValue;

            var actual = BankID.Create(borderValue);

            Assert.AreEqual(borderValue, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankIndex_GreaterBorderValue()
        {
            Int64 greaterBorderValue = unchecked(int.MaxValue + 1);

            var actual = BankIndex.Create(Convert.ToInt32(greaterBorderValue));

            Assert.AreEqual(greaterBorderValue, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankIndex_LessThanZero()
        {
            var bankId = BankIndex.Create(-1);
        }

        [TestMethod]
        public void ConvertBankIndex_ToString()
        {
            var expected = "1";

            var actual = BankIndex.Create(1);

            Assert.AreEqual(expected, actual.ToString());
        }
    }
}
