using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Interfaces
{
    public interface IAccountCalculator
    {
        decimal GetBalanceAt(DateTime date);
        List<(string Category, decimal Total)> GetTopExpenses(int top = 3);
    }
}
