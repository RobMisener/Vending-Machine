using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.Exceptions
{
    public class ItemOutOfStockException : Exception
    {
        public ItemOutOfStockException()
        {

        }

        public ItemOutOfStockException(string message)
        : base(message)
        {
        }

        public ItemOutOfStockException(string message, Exception inner)
        : base(message, inner)
        {
        }
    }
}
