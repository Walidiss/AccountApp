using AccountApp.Core.Entities;
using AccountApp.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AccountApp.Application.Services
{
    public class AccountCalculator : IAccountCalculator
    {
        private readonly ItransactionLoader _transactionLoader;
        private readonly ICurrencyConvertor _currencyConvertor;
        private readonly List<Transaction> _transactions;
        private readonly decimal _initialBalance;

        public AccountCalculator(ItransactionLoader loader, ICurrencyConvertor currencyConvertor)
        {
            _transactionLoader = loader;
            _currencyConvertor = currencyConvertor;
            _transactions = _transactionLoader.LoadTransctions();
            _initialBalance = _transactionLoader.GetInitialBalance();
        }

        /// <summary>
        /// Obtenir le solde a une date donnée
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public decimal GetBalanceAt(DateTime date)
        {
            var transactionsAfterDate = _transactions.Where(t => t.Date > date);
            decimal totalAfterDate = 0;

            foreach (var transaction in transactionsAfterDate)
            {
                totalAfterDate += _currencyConvertor.ConvertToEuro(transaction.Amount, transaction.Currency);
            }

            return _initialBalance - totalAfterDate; 
        }
        /// <summary>
        /// Obtenir les 3 plus grands catégories de débit sur tout l'historique
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<(string Category, decimal Total)> GetTopExpenses(int top = 3)
        {
            return _transactions
                 .Where(t => _currencyConvertor.ConvertToEuro(t.Amount, t.Currency) < 0)
                 .GroupBy(t => t.Category)
                 .Select(g => (Category: g.Key,
                 Total: g.Sum(t => _currencyConvertor.ConvertToEuro(t.Amount, t.Currency))))
                 .OrderBy(x => x.Total) 
                .Take(top)
                .ToList();
        }
    }
}