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
        public string TransactionType { get; set; }

        public IVendingItem Item { get; set; }

        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;

        public VendingMachine Machine { get; set; }

        public double PreviousBalance { get; set; }

        public void PrintTransaction()
        {
            try
            {
                using (StreamWriter sw = new StreamWriter("Log.txt", true))
                {
                    if (TransactionType != "" && TransactionType != null)
                    {
                        sw.WriteLine(TransactionDate + " " + TransactionType + " " + PreviousBalance.ToString("C") + " " + Machine.CurrentMoneyProvided.ToString("C"));
                    }
                    else
                    {
                        sw.WriteLine(TransactionDate + " " + Item.ItemName + " " + Item.ItemSlot + " " + PreviousBalance.ToString("C") + " " + Machine.CurrentMoneyProvided.ToString("C"));
                    }

                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing transaction.");
            }
        }
    }
}
