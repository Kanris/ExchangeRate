using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace ExchangeRateLibrary
{
    public class BankName
    {
        private readonly string name;

        private BankName(string name)
        {
            this.name = name;
        }

        public static BankName Create(string name)
        {
            if (String.IsNullOrEmpty(name))
                throw new Exception("Bank name is null or empty.");

            if (!name.All(Char.IsLetter))
                throw new Exception($"Bank name - {name} is invalid.");

            return new BankName(name);
        }

        public static implicit operator string(BankName bankName)
            => bankName.name;

        public override string ToString()
            => this.name;

        public override bool Equals(object obj)
        {
            var bankName = obj as BankName;

            if (ReferenceEquals(bankName, null))
                return false;

            return bankName.name == this.name;
        }

        public override int GetHashCode()
            => this.name.GetHashCode();
    }
}
