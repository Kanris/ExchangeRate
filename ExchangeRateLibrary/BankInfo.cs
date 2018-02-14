using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateLibrary
{
    public class BankInfo
    {
        public BankID ID { private set; get; } // Bank ID

        public BankName Name { private set; get; } // Bank Name

        public BankURI URI { private set; get; } //WebSite address

        public BankPattern Pattern { private set; get; } //"html line" - where to get exchange value

        public BankIndex Buy { private set; get; } //which Regex.Match corresponds to the buy rate

        public BankIndex Sell { private set; get; } //which Regex.Match corresponds to the sell rate

        public BankInfo(BankID id, BankName name, BankURI uri,
            BankPattern pattern, BankIndex buy, BankIndex sell)
        {
            CheckParameter(id, nameof(id));
            CheckParameter(name, nameof(name));
            CheckParameter(uri, nameof(uri));
            CheckParameter(pattern, nameof(pattern));
            CheckParameter(buy, nameof(buy));
            CheckParameter(sell, nameof(sell));

            this.ID = id;
            this.Name = name;
            this.URI = uri;
            this.Pattern = pattern;
            this.Buy = buy;
            this.Sell = sell;
        }

        public void ChangeID(BankID bankID)
        {
            CheckParameter(bankID, nameof(bankID));

            this.ID = bankID;
        }

        public void ChangeBankName(BankName bankName)
        {
            CheckParameter(bankName, nameof(bankName));

            this.Name = bankName;
        }


        public void ChangeURI(BankURI bankURI)
        {
            CheckParameter(bankURI, nameof(bankURI));

            this.URI = bankURI;
        }

        public void ChangePattern(BankPattern bankPattern)
        {
            CheckParameter(bankPattern, nameof(bankPattern));

            this.Pattern = bankPattern;
        }

        public void ChangeBuyIndex(BankIndex buyIndex)
        {
            CheckParameter(buyIndex, nameof(buyIndex));

            this.Buy = buyIndex;
        }

        public void ChangeSellIndex(BankIndex sellIndex)
        {
            CheckParameter(sellIndex, nameof(sellIndex));

            this.Sell = sellIndex;
        }

        public override string ToString()
            => $"{ID}, {Name}, {URI}, {Pattern}, {Buy}, {Sell}";

        private void CheckParameter<T>(T param, string paramName)
        {
            if (ReferenceEquals(param, null))
                throw new ArgumentNullException($"{paramName} is null.");
        }

        public override bool Equals(object obj)
        {
            var bankInfo = obj as BankInfo;

            if (ReferenceEquals(bankInfo, null))
                return false;

            if (bankInfo.ID.Equals(this.ID) && bankInfo.Name.Equals(this.Name) &&
                bankInfo.URI.Equals(this.URI) && bankInfo.Pattern.Equals(this.Pattern) &&
                bankInfo.Buy.Equals(this.Buy) && bankInfo.Sell.Equals(this.Sell))
                return true;

            return false;
        }
    }
}
