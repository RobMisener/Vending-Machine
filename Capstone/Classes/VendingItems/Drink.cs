using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.VendingItems
{
    public class Drink : IVendingItem
    {
        public string ItemName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string ItemSlot { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public double ItemPrice { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Message => "Glug Glu, Yum!";
    }
}
