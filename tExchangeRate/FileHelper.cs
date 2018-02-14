using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using ExchangeRateLibrary;

namespace tExchangeRate
{
    public static class FileHelper
    {
        private static string path = "BanksInfo.csv";

        //write bankInfo in file
        public static bool WriteInFile(BankInfo bankInfo)
        {
            if (ReferenceEquals(bankInfo, null)) //if bankinfo is null return false
                return false;

            //add bankinfo in file
            using (var writer = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write)))
            {
                writer.WriteLine(bankInfo.ToString());
            }

            return true;
        }

        //read all info from file
        public static List<BankInfo> ReadFromFile()
        {
            var bankInfoArray = new List<BankInfo>();

            using (var reader = new StreamReader(new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read)))
            {
                string line = string.Empty;

                while ( (line = reader.ReadLine()) != null )
                {
                    var bankInfo = CreateBankInfo(line);
                    bankInfoArray.Add(bankInfo);
                }
            }

            return bankInfoArray;
        }

        //delete line from file
        public static bool DeleteFromFile(BankInfo bankInfo)
        {
            if (ReferenceEquals(bankInfo, null)) //if bankinfo is null return false
                return false;

            var lines = new List<string>(); 
            var line = string.Empty; //line from file

            using (var reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            lines.RemoveAll(x => ( Convert.ToInt32((x.Split(',').ToArray())[0]) == bankInfo.ID)); //remove line with the same ID

            using (var outfile = new StreamWriter(path)) //write back all info
            {
                foreach (var item in lines)
                    outfile.WriteLine(item);
            }

            return true;
        }

        private static BankInfo CreateBankInfo(string lineFromFile)
        {
            var items = lineFromFile.Split(',');

            items = items.Select(x => x.Trim()).ToArray(); //remove spaces in words
            items = items.Where(x => !String.IsNullOrEmpty(x)).ToArray(); //remove empty items

            if (items.Length == 6)
            {
                BankID bankID = BankID.Create(Convert.ToInt32(items[0]));
                BankName bankName = BankName.Create(items[1]);
                BankURI bankURI = BankURI.Create(items[2]);
                BankPattern bankPattern = BankPattern.Create(items[3]);
                BankIndex bankBuy = BankIndex.Create(Convert.ToInt32(items[4]));
                BankIndex bankSell = BankIndex.Create(Convert.ToInt32(items[5]));

                return new BankInfo(bankID, bankName, bankURI, bankPattern, bankBuy, bankSell);
            }
            else
            {
                throw new Exception("Wrong data in file");
            }

        }
    }
}
