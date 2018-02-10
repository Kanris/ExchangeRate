using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ExchangeRateLibrary.Tests
{
    //Tests for ExchangeRate class
    [TestClass]
    public class ExchangeRateTest
    {
        private static BankInfo exchangeRateItem;
        private static ExchangeRate exchangeRate;

        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            int bankID = 1;
            string bankName = "Ощадбанк";
            string bankURI = "https://www.oschadbank.ua/ru/private/currency/currency_rates/";
            string pattern = @"<td class=""text-right"">(\d+\.\d+)</td>";
            int buyIndex = 1;
            int sellIndex = 2;

            exchangeRateItem = new BankInfo(bankID, bankName, bankURI, pattern, buyIndex, sellIndex);

            exchangeRate = new ExchangeRate();
        }

        [TestMethod]
        public void AddBankInfoItem()
        {
            exchangeRate.AddBankInfo(exchangeRateItem);

            int expected = 1;
            int actual = exchangeRate.BanksInfo.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddExistedBankInfoItem()
        {
            exchangeRate.AddBankInfo(exchangeRateItem);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddNullBankInfoItem()
        {
            BankInfo bankInfo = null;

            exchangeRate.AddBankInfo(bankInfo);
        }

        [TestMethod]
        public void RemoveBankInfoItem()
        {
            exchangeRate.RemoveBankInfo(exchangeRateItem.ID);

            int expected = 0;
            int actual = exchangeRate.BanksInfo.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void AddBankInfoWithParams()
        {
            int bankID = 1;
            string bankName = "Ощадбанк";
            string bankURI = "https://www.oschadbank.ua/ru/private/currency/currency_rates/";
            string pattern = @"<td class=""text-right"">(\d+\.\d+)</td>";
            int buyIndex = 1;
            int sellIndex = 2;

            exchangeRate.AddBankInfo(bankID, bankName, bankURI, pattern, buyIndex, sellIndex);

            int expected = 1;
            int actual = exchangeRate.BanksInfo.Count;

            Assert.AreEqual(expected, actual);        
        }

        [TestMethod]
        public void GetBuyRate()
        {
            string expected = "2690.00"; //check on web-site buy exchange rate (USD)

            string actual = exchangeRate.GetBuyRate(1).Result;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSellRate()
        {
            string expected = "2745.00"; //check on web-site sell exchange rate (USD)

            string actual = exchangeRate.GetSellRate(1).Result; 

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetBuySellRate()
        {
            var expected = new Tuple<string, string>("2690.00", "2745.00"); //check on web-site buy and sell exchange rate (USD)

            var actual = exchangeRate.GetBuySellRate(1).Result;

            Assert.AreEqual(expected, actual);
        }
    }
}
