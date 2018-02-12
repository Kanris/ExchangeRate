using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateLibrary
{
    public class BankIndex
    {
        private readonly int index;

        private BankIndex(int index)
        {
            this.index = index;
        }

        public static BankIndex Create(int index)
        {
            if (index < 0)
                throw new Exception($"Index ({index}) must be greater or equal to zero.");

            return new BankIndex(index);
        }

        public static implicit operator int(BankIndex bankIndex)
            => bankIndex.index;

        public override string ToString()
            => this.index.ToString();

        public override bool Equals(object obj)
        {
            var bankIndex = obj as BankIndex;

            if (ReferenceEquals(bankIndex, null))
                return false;

            return bankIndex.index == this.index;
        }

        public override int GetHashCode()
            => this.index.GetHashCode();
    }
}
