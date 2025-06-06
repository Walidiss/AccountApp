using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Exceptions
{
    public class UnkownCurrencyException : Exception
    {
        public UnkownCurrencyException(string currency) : base ($"Unknown currency: {currency}") 
        {
                
        }
    }
}
