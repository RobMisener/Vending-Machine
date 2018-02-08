using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public List<IVendingItem> Items { get; set; }

        public double CurrentMoneyProvided { get; set; }

        public double Balance { get; set; }

        public void Purchase()
        {

        }

    }
}
