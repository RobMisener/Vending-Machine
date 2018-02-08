﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes.VendingItems
{
    public class Chips : IVendingItem
    {
        public string ItemName { get; set; }
        public string ItemSlot { get; set; }
        public double ItemPrice { get; set; }
        public string Message => "Crunch Crunch, Yum!";
        public int Stock
        {
            get
            {
                return 5;
            }
            set { }
        }
    }
}
