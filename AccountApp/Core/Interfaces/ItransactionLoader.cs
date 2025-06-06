using AccountApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Interfaces
{
    public interface ItransactionLoader
    {
        List<Transaction> LoadTransctions();

        decimal GetInitialBalance();

        Currency GetExchangeRate();

    }
}
