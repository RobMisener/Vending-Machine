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

        

        public void FeedMoney(int providedCash)
        {
            double newBalance = CurrentMoneyProvided + providedCash;

            Transaction.PrintTransaction("FEED MONEY", CurrentMoneyProvided, newBalance);

            CurrentMoneyProvided = newBalance;            
        }

        




        public void SalesReport(IVendingItem item)
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
                                        string newText = "Total Sales|" + Balance.ToString("C");
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
                    foreach (IVendingItem vendingItem in Items)
                    {
                        srr.WriteLine(vendingItem.ItemName + "|" + vendingItem.SoldItems);
                    }
                    srr.WriteLine("Total Sales" + "|" + Balance);
                }
            }
        }

        public void Purchase(IVendingItem item)
        {
            item.Stock--;
            item.SoldItems++;

            CurrentMoneyProvided -= item.ItemPrice;
            Balance += item.ItemPrice;

            Transaction.PrintTransaction(item.ItemName, item.ItemSlot, CurrentMoneyProvided, CurrentMoneyProvided - item.ItemPrice);            
            Console.WriteLine("Item purchase successful");
        }

    }
}

