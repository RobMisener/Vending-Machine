using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Capstone.Classes.VendingItems;

namespace Capstone.Classes
{
    public class VendingMachine
    {
        public List<IVendingItem> Items { get; set; }

        public double CurrentMoneyProvided { get; set; }

        public double Balance { get; set; }

        public int GetProvidedCash()
        {
            int providedCash = 0;
            Console.Write("How much money are you putting in?: ");
            providedCash = Convert.ToInt32(Console.ReadLine());
            return providedCash;
        }

        public VendingMachine FeedMoney(int providedCash, VendingMachine vendingMachine)
        { 

            Transaction transaction = new Transaction();
            transaction.PreviousBalance = vendingMachine.CurrentMoneyProvided;
            vendingMachine.CurrentMoneyProvided += providedCash;
            transaction.TransactionType = "FEED MONEY";
            transaction.Machine = vendingMachine;
            transaction.PrintTransaction();
            return vendingMachine;
            }


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
                        switch (itemParts[0][0])
                        {
                            case 'A':
                                Chips chipItem = new Chips();
                                chipItem.ItemSlot = itemParts[0];
                                chipItem.ItemName = itemParts[1];
                                chipItem.ItemPrice = Convert.ToDouble(itemParts[2]);
                                chipItem.Stock = 5;
                                items.Add(chipItem);
                                break;
                            case 'B':
                                Candy candy = new Candy();
                                candy.ItemSlot = itemParts[0];
                                candy.ItemName = itemParts[1];
                                candy.ItemPrice = Convert.ToDouble(itemParts[2]);
                                candy.Stock = 5;
                                items.Add(candy);
                                break;
                            case 'C':
                                Drink drink = new Drink();
                                drink.ItemSlot = itemParts[0];
                                drink.ItemName = itemParts[1];
                                drink.ItemPrice = Convert.ToDouble(itemParts[2]);
                                items.Add(drink);
                                drink.Stock = 5;
                                break;
                            case 'D':
                                Gum gum = new Gum();
                                gum.ItemSlot = itemParts[0];
                                gum.ItemName = itemParts[1];
                                gum.ItemPrice = Convert.ToDouble(itemParts[2]);
                                gum.Stock = 5;
                                items.Add(gum);
                                break;
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Input error: " + ex);
            }

            return items;
        }

    }
}
