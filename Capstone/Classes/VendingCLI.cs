using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class VendingCLI
    {
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

        public string DisplayPurchaseMenu(VendingMachine vendingMachine)
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
