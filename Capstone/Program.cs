using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes;
using Capstone.Classes.VendingItems;
using Capstone.Exceptions;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingCLI cli = new VendingCLI();
            cli.DisplayVendingMachine();            
        }
    }
}
