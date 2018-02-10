using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateLibrary
{
    public class BankInfo
    {
        public int ID { set; get; } // Bank ID

        public string BankName { set; get; } // Bank Name

        public string URI { set; get; } //WebSite address

        public string pattern { set; get; } //"html line" - where to get exchange value

        public int IndexBuy { set; get; } = 0; //which Regex.Match corresponds to the buy rate

        public int IndexSell { set; get; } = 1; //which Regex.Match corresponds to the sell rate

    }
}
