using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tExchangeRate
{
    public class ExchangeRateItem
    {
        public int ID { set; get; }

        public string Name { set; get; } // Bank Name

        public string Buy { set; get; } // Buy Rate

        public string Sell { set; get; } // Sell Rate

        public ExchangeRateItem(int id, string name, string buy, string sell)
        {
            this.ID = id;
            this.Name = name;
            this.Buy = buy;
            this.Sell = sell;
        }
    }
}
