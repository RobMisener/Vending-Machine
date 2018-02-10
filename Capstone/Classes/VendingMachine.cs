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
                                IVendingItem chipItem = new Chips();
                                chipItem = AddItem(chipItem, itemParts);
                                items.Add(chipItem);
                                break;
                            case 'B':
                                IVendingItem candy = new Candy();
                                candy = AddItem(candy, itemParts);
                                items.Add(candy);
                                break;
                            case 'C':
                                IVendingItem drink = new Drink();
                                drink = AddItem(drink, itemParts);
                                items.Add(drink);
                                break;
                            case 'D':
                                IVendingItem gum = new Gum();
                                gum = AddItem(gum, itemParts);
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

        private IVendingItem AddItem(IVendingItem item, string[] itemParts)
        {
            item.ItemSlot = itemParts[0];
            item.ItemName = itemParts[1];
            item.ItemPrice = Convert.ToDouble(itemParts[2]);
            item.Stock = 5;
            return item;
        }

        public VendingMachine UpdateBalance(VendingMachine vendingMachine, IVendingItem item)
        {
            vendingMachine.CurrentMoneyProvided -= item.ItemPrice;
            vendingMachine.Balance += item.ItemPrice;
            return vendingMachine;
        }

        public IVendingItem DecreaseStock(IVendingItem item)
        {
            item.Stock--;
            item.SoldItems++;
            return item;
        }

        public void SalesReport(VendingMachine vendingMachine, IVendingItem item)
        {
            if (File.Exists("SalesReport.txt"))
            {
                using (StreamReader sr = new StreamReader("SalesReport.txt"))
                {
                    using (StreamWriter sw = new StreamWriter("TempSalesReport.txt"))
                    {
                        try
                        {
                            while (!sr.EndOfStream)
                            {
                                string stringItem = sr.ReadLine();
                                if (stringItem.Contains(item.ItemName))
                                {
                                    string[] itemParts = stringItem.Split('|');
                                    int previousSold = Convert.ToInt32(itemParts[1]);
                                    int newAmount = (previousSold + 1);
                                    string newText = item.ItemName + "|" + newAmount;
                                    sw.WriteLine(newText);
                                }
                                else
                                {
                                    if (stringItem.Contains("Total Sales"))
                                    {
                                        string newText = "Total Sales|" + vendingMachine.Balance.ToString("C");
                                        sw.WriteLine(newText);
                                    }
                                    else
                                    {
                                        sw.WriteLine(stringItem);
                                    }
                                }
                            }
                        }
                        catch(IOException ex)
                        {
                            Console.WriteLine("Error writing to file");
                        }
                    }
                    sr.Close();
                }
                File.Delete("SalesReport.txt");
                File.Move("TempSalesReport.txt", "SalesReport.txt");
                File.Delete("TempSalesReport.txt");
            }

            else
            {
                using (StreamWriter srr = new StreamWriter("SalesReport.txt"))
                {
                    foreach (IVendingItem vendingItem in vendingMachine.Items)
                    {
                        srr.WriteLine(vendingItem.ItemName + "|" + vendingItem.SoldItems);
                    }
                    srr.WriteLine("Total Sales" + "|" + vendingMachine.Balance);
                }
            }
        }

        public void Purchase(VendingMachine vendingMachine, IVendingItem item, Transaction transaction)
        {
            transaction.Item = item;
            transaction.Machine = vendingMachine;
            transaction.PrintTransaction();
            Console.WriteLine("Item purchase successful");
        }

    }
}

