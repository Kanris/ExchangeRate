using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateLibrary
{
    public class BankPattern
    {
        private readonly string pattern;

        private BankPattern(string pattern)
        {
            this.pattern = pattern;
        }

        public static BankPattern Create(string pattern)
        {
            if (String.IsNullOrEmpty(pattern))
                throw new Exception("Pattern is null or empty.");

            return new BankPattern(pattern);
        }

        public static implicit operator string(BankPattern bankPattern)
            => bankPattern.pattern;

        public override string ToString()
            => this.pattern;

        public override bool Equals(object obj)
        {
            var bankPattern = obj as BankPattern;

            if (ReferenceEquals(bankPattern, null))
                return false;

            return bankPattern.pattern == this.pattern;
        }

        public override int GetHashCode()
            => this.pattern.GetHashCode();
    }
}
