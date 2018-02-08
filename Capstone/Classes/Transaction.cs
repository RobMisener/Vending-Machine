using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Transaction
    {
        public Transaction(IVendingItem item)
        {
            Item = item;
        }

        public IVendingItem Item { get; }
    }
}
