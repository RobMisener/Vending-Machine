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
        public decimal Total { get; set;}

        public void ReturnChange(decimal amounReturned)
        {
            
            Total = amounReturned;

            while (amounReturned >= .25M)
            {
                Quarters++;
                amounReturned -= .25M;
            }
            while (amounReturned >= .10M)
            {
                Dimes++;
                amounReturned -= 0.10M;
            }
            while (amounReturned >= .05M)
            {
                Nickels++;
                amounReturned -= .05M;
            }
        }
       

    }

}
