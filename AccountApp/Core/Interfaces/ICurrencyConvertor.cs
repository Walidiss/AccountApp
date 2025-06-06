using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Interfaces
{
    public interface ICurrencyConvertor
    {
        decimal ConvertToEuro(decimal amount, string currency);
    }
}
