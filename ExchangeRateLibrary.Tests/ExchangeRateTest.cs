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

            exchangeRateItem = new BankInfo(bankID, bankName, bankURI, pattern, buyIndex, sellIndex); //initialize BankInfo item

            exchangeRate = new ExchangeRate(); //initialize ExchangeRate class
        }

        [TestMethod]
        public void AddBankInfoItem()
        {
            exchangeRate.AddBankInfo(exchangeRateItem); //add BankInfo item in collection

            int expected = 1; //expected items count
            int actual = exchangeRate.BanksInfo.Count; //actual items count

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AddExistedBankInfoItem()
        {
            exchangeRate.AddBankInfo(exchangeRateItem); //trying to add in collection existed collection item 
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void AddNullBankInfoItem()
        {
            BankInfo bankInfo = null;

            exchangeRate.AddBankInfo(bankInfo); //trying to add null object to collection
        }

        [TestMethod]
        public void RemoveBankInfoItem()
        {
            exchangeRate.RemoveBankInfo(exchangeRateItem.ID); //remove bankinfo from collection

            int expected = 0; //expected items count
            int actual = exchangeRate.BanksInfo.Count; //actual items count

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

            exchangeRate.AddBankInfo(bankID, bankName, bankURI, pattern, buyIndex, sellIndex); //add new item to collection

            int expected = 1; //expected items count
            int actual = exchangeRate.BanksInfo.Count; //actual items count

            Assert.AreEqual(expected, actual);        
        }

        [TestMethod]
        public void GetBuyRate()
        {
            string expected = "2690.00"; //check on web-site buy exchange rate (USD)

            string actual = exchangeRate.GetBuyRate(1).Result; //get buy rate from web-site

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetSellRate()
        {
            string expected = "2745.00"; //check on web-site sell exchange rate (USD)

            string actual = exchangeRate.GetSellRate(1).Result;  //get sell rate from web-site

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void GetBuySellRate()
        {
            var expected = new Tuple<string, string>("2690.00", "2745.00"); //check on web-site buy and sell exchange rate (USD)

            var actual = exchangeRate.GetBuySellRate(1).Result; //get buy and sell rate from web-site

            Assert.AreEqual(expected, actual);
        }
    }
}
