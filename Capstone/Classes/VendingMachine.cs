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

        public List<IVendingItem> ReStockEachItem(List<IVendingItem> items)
        {
            foreach (IVendingItem item in items)
            {
                item.Stock = 5;
            }
            return items;
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
            catch (IOException ex)
            {
                Console.WriteLine("Input error: " + ex);
            }

            items = ReStockEachItem(items);

            return items;
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
            int countLine = 1;
            if (File.Exists("SalesReport.txt"))
            {
                using (StreamReader sr = new StreamReader("SalesReport.txt"))
                {
                    while (!sr.EndOfStream)
                    {


                        if (File.Exists("SalesReport.txt"))
                        {

                            using (StreamWriter srr = new StreamWriter("SalesReport.txt"))
                            {
                                string stringItem = sr.ReadLine();


                                if (stringItem.Contains(item.ItemName))
                                {
                                    string[] itemParts = stringItem.Split('|');
                                    int previousSold = Convert.ToInt32(itemParts[1]);
                                    int newAmount = (previousSold + item.SoldItems);
                                    string newText = item.ItemName + "|" + newAmount;
                                    lineChanger(newText, "SalesReport.txt", countLine);
                                }

                                countLine++;

                            }
                        }

                    }

                }
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

            static void lineChanger(string newText, string fileName, int line_to_edit)
        {
            string[] arrLine = File.ReadAllLines(fileName);
            arrLine[line_to_edit - 1] = newText;
            File.WriteAllLines(fileName, arrLine);
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

