using Capstone.Classes.VendingItems;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingMachineStocker
    {
        private const int Col_SlotId = 0;
        private const int Col_Name = 1;
        private const int Col_Cost = 2;
        private const int Default_Quantity = 5;

        public List<IVendingItem> GetItems()
        {
            List<IVendingItem> items = new List<IVendingItem>();

            try
            {
                using (StreamReader sr = new StreamReader("vendingmachine.csv"))
                {
                    while (!sr.EndOfStream)
                    {
                        string stringItem = sr.ReadLine();
                        string[] itemParts = stringItem.Split('|');

                        IVendingItem item = GetItemFromItemPart(itemParts[Col_SlotId][0]);                       
                        SetItemFields(item, itemParts);

                        items.Add(item);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Input error: " + ex);
            }
            return items;
        }

        private IVendingItem GetItemFromItemPart(char firstCharacter)
        {
            IVendingItem item = null;

            switch (firstCharacter)
            {
                case 'A':
                    item = new Chips();
                    break;
                case 'B':
                    item = new Candy();
                    break;
                case 'C':
                    item = new Drink();
                    break;
                case 'D':
                    item = new Gum();
                    break;
            }

            return item;
        }

        private void SetItemFields(IVendingItem item, string[] itemParts)
        {
            item.ItemSlot = itemParts[Col_SlotId];
            item.ItemName = itemParts[Col_Name];
            item.ItemPrice = Convert.ToDouble(itemParts[Col_Cost]);
            item.Stock = Default_Quantity;
            
        }

    }
}
