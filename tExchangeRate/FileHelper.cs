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

        public static bool WriteInFile(BankInfo bankInfo)
        {
            if (ReferenceEquals(bankInfo, null))
                return false;

            using (var writer = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write)))
            {
                writer.WriteLine(bankInfo.ToString());
            }

            return true;
        }

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

        public static bool DeleteFromFile(BankInfo bankInfo)
        {
            if (ReferenceEquals(bankInfo, null))
                return false;

            var lines = new List<string>();
            var line = string.Empty;

            using (var reader = new StreamReader(path))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    lines.Add(line);
                }
            }

            lines.RemoveAll(x => ( Convert.ToInt32((x.Split(',').ToArray())[0]) == bankInfo.ID));

            using (var outfile = new StreamWriter(path))
            {
                foreach (var item in lines)
                    outfile.WriteLine(item);
            }

            return true;
        }

        private static BankInfo CreateBankInfo(string lineFromFile)
        {
            var items = lineFromFile.Split(',');
            items = items.Select(x => x.Trim()).ToArray();

            BankID bankID = BankID.Create(Convert.ToInt32(items[0]));
            BankName bankName = BankName.Create(items[1]);
            BankURI bankURI = BankURI.Create(items[2]);
            BankPattern bankPattern = BankPattern.Create(items[3]);
            BankIndex bankBuy = BankIndex.Create(Convert.ToInt32(items[4]));
            BankIndex bankSell = BankIndex.Create(Convert.ToInt32(items[5]));

            return new BankInfo(bankID, bankName, bankURI, bankPattern, bankBuy, bankSell);
        }
    }
}
