using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.VendingItems
{
    public class Drink : IVendingItem
    {
        public string ItemName { get; set; }
        public string ItemSlot { get; set; }
        public double ItemPrice { get; set; }
        public string Message => "Glug Glu, Yum!";
    }
}
