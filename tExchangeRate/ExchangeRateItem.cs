using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tExchangeRate
{
    public class ExchangeRateItem
    {
        public string Name { set; get; } // Bank Name

        public string Buy { set; get; } // Buy Rate

        public string Sell { set; get; } // Sell Rate

        public ExchangeRateItem(string name, string buy, string sell)
        {
            this.Name = name;
            this.Buy = buy;
            this.Sell = sell;
        }
    }
}
