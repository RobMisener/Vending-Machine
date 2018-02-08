using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes;
using Capstone.Classes.VendingItems;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.Items = GetItems();
            foreach (IVendingItem item in vendingMachine.Items)
            {
                Console.WriteLine(item.ItemName + "|" + item.GetType());
            }
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
                        switch (itemParts[0][0])
                        {
                            case 'A':
                                Chips chipItem = new Chips();
                                chipItem.ItemSlot = itemParts[0];
                                chipItem.ItemName = itemParts[1];
                                chipItem.ItemPrice = Convert.ToDouble(itemParts[2]);
                                items.Add(chipItem);
                                break;
                            case 'B':
                                Candy candy = new Candy();
                                candy.ItemSlot = itemParts[0];
                                candy.ItemName = itemParts[1];
                                candy.ItemPrice = Convert.ToDouble(itemParts[2]);
                                items.Add(candy);
                                break;
                            case 'C':
                                Drink drink = new Drink();
                                drink.ItemSlot = itemParts[0];
                                drink.ItemName = itemParts[1];
                                drink.ItemPrice = Convert.ToDouble(itemParts[2]);
                                items.Add(drink);
                                break;
                            case 'D':
                                Gum gum = new Gum();
                                gum.ItemSlot = itemParts[0];
                                gum.ItemName = itemParts[1];
                                gum.ItemPrice = Convert.ToDouble(itemParts[2]);
                                items.Add(gum);
                                break;
                        }
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
