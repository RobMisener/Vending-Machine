using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public interface IVendingItem
    {
        string ItemName { get; set; }

        string ItemSlot { get; set; }

        double ItemPrice { get; set; }

        string Message { get; }

        int Stock { get; set; }



    }
}
