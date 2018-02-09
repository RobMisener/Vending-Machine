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
            int userInput = 0;
            int purchaseInputOption = 0;
            int providedCash = 0;
            bool isDone = false;
            List<IVendingItem> items = new List<IVendingItem>();

            while (isDone != true)
            {

                Console.WriteLine("\n(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) Quit");
                userInput = Convert.ToInt32(Console.ReadLine());

                if (userInput == 1)
                {
                    foreach (IVendingItem item in vendingMachine.Items)
                    {
                        //Console.WriteLine(item.ItemName + "|" + item.GetType());
                        string stockString = (item.Stock > 0) ? item.Stock.ToString() : "SOLD OUT";
                        
                        Console.WriteLine(item.ItemSlot + ": " + item.ItemName + "..." + item.ItemPrice.ToString("c") + " Qty: " + stockString);
                    }
                }
                else if (userInput == 2)
                {
                    purchaseInputOption = DisplayPurchaseMenu(vendingMachine);
                    while (purchaseInputOption != 3)
                    {
                        if (purchaseInputOption == 1)
                        {

                            Console.Write("How much money are you putting in?: ");
                            providedCash = Convert.ToInt32(Console.ReadLine());
                            Transaction transaction = new Transaction();
                            transaction.PreviousBalance = vendingMachine.CurrentMoneyProvided;
                            vendingMachine.CurrentMoneyProvided += providedCash;
                            transaction.TransactionType = "FEED MONEY";
                            transaction.Machine = vendingMachine;
                            transaction.PrintTransaction();
                        }
                        else if (purchaseInputOption == 2)
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
                                        vendingMachine.CurrentMoneyProvided -= item.ItemPrice;
                                        vendingMachine.Balance += item.ItemPrice;
                                        item.Stock--;
                                        items.Add(item);
                                        transaction.Item = item;
                                        transaction.Machine = vendingMachine;
                                        transaction.PrintTransaction();
                                        Console.WriteLine("Item purchase successful");
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Item out of stock");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Product not found");
                            }
                        }
                        purchaseInputOption = DisplayPurchaseMenu(vendingMachine);
                    }
                    if (purchaseInputOption == 3)
                    {
                        Change change = new Change();
                        change.ReturnChange(vendingMachine.CurrentMoneyProvided);

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
                else if (userInput == 3)
                {
                    return;
                }
            }
        }

        private static int DisplayPurchaseMenu(VendingMachine vendingMachine)
        {
            int purchaseInputOption = 0;

            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine("Current Money Provided: " + vendingMachine.CurrentMoneyProvided.ToString("C"));
            purchaseInputOption = Convert.ToInt32(Console.ReadLine());
            return purchaseInputOption;
        }

        private static List<IVendingItem> GetItems()
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

        //    private static i
    }
}
