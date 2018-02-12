using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRateLibrary.Tests
{
    [TestClass]
    public class BankNameTest
    {
        [TestMethod]
        public void CreateBankName_EnglishName()
        {
            var expected = "Bank";

            var actual = BankName.Create(expected);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CreateBankName_RussianName()
        {
            var expected = "Банк";

            var actual = BankName.Create(expected);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankName_Empty()
        {
            var bankName = BankName.Create("");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankName_Null()
        {
            var bankName = BankName.Create(null);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankName_WithDigits()
        {
            var bankName = BankName.Create("12Bank2");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void CreateBankName_SpecialSymbols()
        {
            var bankName = BankName.Create("Bank*_");
        }

        [TestMethod]
        public void ConvertBankName_ToString()
        {
            var expected = "Bank";

            var actual = BankName.Create("Bank");

            Assert.AreEqual(expected, actual);
        }
    }
}
