using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateLibrary
{
    public class BankID
    {
        private readonly int id;

        private BankID(int id)
        {
            this.id = id;
        }

        public static BankID Create(int id)
        {
            if (id < 0)
                throw new Exception($"Bank id ({id}) must be greater or equal to zero");

            return new BankID(id);
        }

        public static implicit operator int(BankID bankID)
            => bankID.id;

        public override string ToString()
            => this.id.ToString();

        public override bool Equals(object obj)
        {
            var bankID = obj as BankID;

            if (ReferenceEquals(obj, null))
                return false;

            return bankID.id == this.id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
