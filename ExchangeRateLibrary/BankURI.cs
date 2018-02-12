using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateLibrary
{
    public class BankURI
    {
        private readonly string URI;

        private BankURI(string URI)
        {
            this.URI = URI;
        }

        public static BankURI Create(string URI)
        {
            if (String.IsNullOrEmpty(URI))
                throw new Exception("Uri is null or empty.");

            if (!Uri.IsWellFormedUriString(URI, UriKind.RelativeOrAbsolute))
                throw new Exception($"URI ({URI}) is invalid.");

            return new BankURI(URI);
        }

        public static implicit operator string(BankURI bankURI)
            => bankURI.URI;

        public override string ToString()
            => this.URI;

        public override bool Equals(object obj)
        {
            var bankURI = obj as BankURI;

            if (ReferenceEquals(bankURI, null))
                return false;

            return bankURI.URI == this.URI;
        }

        public override int GetHashCode()
            => this.URI.GetHashCode();
    }
}
