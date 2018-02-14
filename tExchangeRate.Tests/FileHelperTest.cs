using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ExchangeRateLibrary;

namespace tExchangeRate.Tests
{
    [TestClass]
    public class FileHelperTest
    {
        /*
         * 2, "УКРГАЗБАНК", "https://www.ukrgasbank.com/", @"<td class=""val"">(\d+\.\d+)</td>", 0, 1
         */
        private static BankInfo bankInfo, bankInfo2;

        [ClassInitialize]
        public static void Initialize(TestContext testContext)
        {
            File.Delete("BanksInfo.csv"); //delete file if it exist

            var bankID = BankID.Create(1);
            var bankName = BankName.Create("Укргазбанк");
            var bankURI = BankURI.Create("https://www.ukrgasbank.com/");
            var bankPattern = BankPattern.Create(@"<td class=""val"">(\d+\.\d+)</td>");
            var bankBuyIndex = BankIndex.Create(0);
            var bankSellIndex = BankIndex.Create(1);

            bankInfo = new BankInfo(bankID, bankName, bankURI, bankPattern, bankBuyIndex, bankSellIndex);
            bankInfo2 = new BankInfo(BankID.Create(2), bankName, bankURI, bankPattern, bankBuyIndex, bankSellIndex);
        }

        [TestMethod]
        public void WriteInFile() //write bank information in file
        {
            var actual = FileHelper.WriteInFile(bankInfo);

            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ReadFromFile() //read bank information from file
        {
            var actual = FileHelper.ReadFromFile()[0];

            var expected = bankInfo;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteFromFile() //delete added bank information in file
        {
            var actual = FileHelper.DeleteFromFile(bankInfo);

            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void WriteThreeRowsInFile() //write bank information in file three times
        {
            bool actual = false;

            if (FileHelper.WriteInFile(bankInfo2) && FileHelper.WriteInFile(bankInfo)
                && FileHelper.WriteInFile(bankInfo))
            {
                actual = true;
            }

            var expected = true;

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void DeleteFirstRowAndReadFromFile() //delete bank information with id = 2 and read the rest information from file
        {
            var actual = true;

            FileHelper.DeleteFromFile(bankInfo2); //delete bank info with id = 2

            var list = FileHelper.ReadFromFile(); //read rest information from file

            if (list.Count > 2) actual = false; //if in file more than two items then bank info with id 2 wasn't delete

            var index = 0;
            while (index < list.Count && actual) 
            {
                if (!list[index].Equals(bankInfo)) actual = false; //if items doesn't equals to bankInfo, then wrong information was deleted

                index++;
            }

            var expected = true;

            Assert.AreEqual(expected, actual);
        }
    }
}
