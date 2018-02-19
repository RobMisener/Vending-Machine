using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Capstone.Classes
{
    public class Transaction
    {
        public static void PrintTransaction(string itemName, string itemSlot, double previousBalance, double newBalance)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Log.txt", true))
                {
                    sw.WriteLine(DateTime.UtcNow + " " + itemName + " " + itemSlot + " " + previousBalance.ToString("C") + " " + newBalance.ToString("C"));
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing transaction.");
            }
        }

        public static void PrintTransaction(string transactionType, double previousBalance, double newBalance)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Log.txt", true))
                {
                    sw.WriteLine(DateTime.UtcNow + " " + transactionType + " " + previousBalance.ToString("C") + " " + newBalance.ToString("C"));
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing transaction.");
            }
        }
    }
}
