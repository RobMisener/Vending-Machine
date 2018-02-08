using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.Items = GetItems();

        }

        private static List<IVendingItem> GetItems()
        {
            List<IVendingItem> items = new List<IVendingItem>();

            try
            {
                using(StreamReader sr = new StreamReader("vendingmachine.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string stringItem = sr.ReadLine();

                        string[] itemParts = stringItem.Split('|');
                        IVendingItem item;
                        

                    }
                }
            }
            catch(IOException ex)
            {
                Console.WriteLine("Input error: " + ex);
            }

            return items;
        }
    }
}
