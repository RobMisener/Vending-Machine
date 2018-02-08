using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Classes
{
    public class Change
    {
        public int Nickels { get; set; }
        public int Dimes { get; set; }
        public int Quarters { get; set; }
        public double Total { get; set;}

        public void ReturnChange(double amounReturned)
        {
            Total = amounReturned;

            while (amounReturned >= .25)
            {
                Quarters++;
                amounReturned -= 0.25;
            }
            while (amounReturned >= .10)
            {
                Dimes++;
                amounReturned -= 0.10;
            }
            while (amounReturned >= .05)
            {
                Dimes++;
                amounReturned -= 0.05;
            }
        }
       

    }

}
