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
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.Items = vendingMachine.GetItems();
            string userInput = "";
            string purchaseInputOption = "";
            int providedCash = 0;
            bool isDone = false;
            List<IVendingItem> items = new List<IVendingItem>();
            VendingCLI cli = new VendingCLI();

            while (isDone != true)
            {
                Console.WriteLine("\n(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) Quit");
                userInput = cli.GetValidInput();

                if (userInput == "1")
                {
                    foreach (IVendingItem item in vendingMachine.Items)
                    {
                        //Console.WriteLine(item.ItemName + "|" + item.GetType());
                        string stockString = (item.Stock > 0) ? item.Stock.ToString() : "SOLD OUT";

                        Console.WriteLine(item.ItemSlot + ": " + item.ItemName + "..." + item.ItemPrice.ToString("c") + " Qty: " + stockString);
                    }
                }
                else if (userInput == "2")
                {
                    purchaseInputOption = cli.DisplayPurchaseMenu(vendingMachine);
                    while (purchaseInputOption != "3")
                    {
                        if (purchaseInputOption == "1")
                        {
                            providedCash = vendingMachine.GetProvidedCash();
                            vendingMachine.FeedMoney(providedCash, vendingMachine);
                        }
                        else if (purchaseInputOption == "2")
                        {
                            string productChoice = "";
                            Console.Write("Please enter the product code: ");
                            productChoice = Console.ReadLine();
                            IVendingItem item = vendingMachine.Items.Find(p => p.ItemSlot == productChoice);
                            if (item != null)
                            {
                                if (item.Stock > 0)
                                {
                                    if (item.ItemPrice <= vendingMachine.CurrentMoneyProvided)
                                    {
                                        Transaction transaction = new Transaction();
                                        transaction.PreviousBalance = vendingMachine.CurrentMoneyProvided;
                                        double remainingChange = vendingMachine.CurrentMoneyProvided - item.ItemPrice;
                                        vendingMachine.UpdateBalance(vendingMachine, item);
                                        item = vendingMachine.DecreaseStock(item);
                                        items.Add(item);
                                        vendingMachine.Purchase(vendingMachine, item, transaction);
                                        vendingMachine.SalesReport(vendingMachine, item);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("\nItem out of stock\n");
                                    //throw new ItemOutOfStockException("Item out of stock.");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Product was not found.");
                                //throw new ProductNotFoundException("Product was not found.");
                            }
                        }
                        purchaseInputOption = cli.DisplayPurchaseMenu(vendingMachine);
                    }
                    if (purchaseInputOption == "3")
                    {
                        Change change = new Change();
                        change.ReturnChange((decimal)vendingMachine.CurrentMoneyProvided);

                        Console.WriteLine("You get back " + vendingMachine.CurrentMoneyProvided.ToString("C") + " in " + change.Quarters + " quarters, " + change.Dimes + " dimes, and " + change.Nickels + " nickels");

                        foreach (IVendingItem item in items)
                        {
                            Console.WriteLine(item.Message);
                        }

                        Transaction transaction = new Transaction();
                        transaction.PreviousBalance = vendingMachine.CurrentMoneyProvided;
                        vendingMachine.CurrentMoneyProvided = 0.00;
                        transaction.TransactionType = "GIVE CHANGE";
                        transaction.Machine = vendingMachine;
                        transaction.PrintTransaction();
                    }
                }
                else if (userInput == "3")
                {
                    return;
                }
            }
        }
    }
}
