using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRateLibrary.Tests
{
    [TestClass]
    public class BankPatternTest
    {
        [TestMethod]
        public void CreateBankPattern_Normal()
        {
            var expected = @"<td class=""val"">(\d+\.\d+)</td>";

            var actual = BankPattern.Create(expected);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankPattern_Empty()
        {
            var bankPattern = BankPattern.Create(string.Empty);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankPattern_Null()
        {
            var bankPattern = BankPattern.Create(null);
        }

        [TestMethod]
        public void ConvertBankPattern_ToString()
        {
            var expceted = @"<td class=""val"">(\d+\.\d+)</td>";

            var actual = BankPattern.Create(@"<td class=""val"">(\d+\.\d+)</td>");

            Assert.AreEqual(expceted, actual);
        }
    }
}
