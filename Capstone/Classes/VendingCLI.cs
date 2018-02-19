using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingCLI
    {
        VendingMachine vendingMachine;
        List<IVendingItem> items = new List<IVendingItem>();


        public VendingCLI()
        {
            vendingMachine = new VendingMachine();

            VendingMachineStocker stocker = new VendingMachineStocker();
            vendingMachine.Items = stocker.GetItems();
        }
        

        public string GetValidInput()
        {
            string userInput = Console.ReadLine();
            while (userInput != "1" && userInput != "2" && userInput != "3")
            {
                Console.Write("Invalid input. Please try again: ");
                userInput = Console.ReadLine();
            }

            return userInput;
        }

        public void DisplayVendingMachine()
        {                       
            string userInput = "";
            string purchaseInputOption = "";
            bool isDone = false;

            while (isDone != true)
            {
                Console.WriteLine("\n(1) Display Vending Machine Items");
                Console.WriteLine("(2) Purchase");
                Console.WriteLine("(3) Quit");
                userInput = GetValidInput();

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
                    purchaseInputOption = DisplayPurchaseMenu();

                    while (purchaseInputOption != "3")
                    {
                        if (purchaseInputOption == "1")
                        {
                            PromptUserForMoney();                            
                        }
                        else if (purchaseInputOption == "2")
                        {
                            PromptForPurchase();                            
                        }
                        purchaseInputOption = DisplayPurchaseMenu();

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

                        
                        Transaction.PrintTransaction("GIVE CHANGE", vendingMachine.CurrentMoneyProvided, 0);                        
                    }
                }
                else if (userInput == "3")
                {
                    return;
                }
            }
        }

        private void PromptForPurchase()
        {            
            Console.Write("Please enter the product code: ");
            string productChoice = Console.ReadLine();

            IVendingItem item = vendingMachine.Items.Find(p => p.ItemSlot == productChoice);

            if (item != null)
            {
                if (item.Stock > 0)
                {
                    if (item.ItemPrice <= vendingMachine.CurrentMoneyProvided)
                    {
                        vendingMachine.Purchase(item);
                        items.Add(item);
                        
                        vendingMachine.SalesReport(item);
                    }
                    else
                    {
                        Console.WriteLine("\nPlease provide more money\n");
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

        private void PromptUserForMoney()
        {
            Console.Write("How much money are you putting in?: ");

            if (int.TryParse(Console.ReadLine(), out int providedCash))
            {
                vendingMachine.FeedMoney(providedCash);
            }
            else
            {
                Console.WriteLine("Invalid amount entered.");
            }                        
        }
      

        public string DisplayPurchaseMenu()
        {
            string purchaseInputOption = "";

            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine("Current Money Provided: " + vendingMachine.CurrentMoneyProvided.ToString("C"));
            purchaseInputOption = this.GetValidInput();
            return purchaseInputOption;
        }
    }
}
