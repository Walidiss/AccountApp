using AccountApp.Core.Entities;
using AccountApp.Core.Exceptions;
using AccountApp.Core.Interfaces;
using AccountApp.Infrastructure.Repositories;

namespace AccountApp.Infrastructure.Services
{
    public class ExchangeRateService : ICurrencyConvertor
    {
        private readonly Currency _currency;

        /// <summary>
        /// charger les taux de changes
        /// </summary>
        public ExchangeRateService()
        {
            var repo = new CsvTransactionLoader("account_20230228.csv");
            _currency = repo.GetExchangeRate();
        }

        /// <summary>
        /// convertir un montant d'une devise donnée vers l'euro
        /// </summary>
        /// <param name="amount"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        /// <exception cref="UnkownCurrencyException"></exception>
        public decimal ConvertToEuro(decimal amount, string currency)
        {
            return currency switch
            {
                "EUR" => amount,
                "USD" => amount * _currency.EurToUsd,
                "JPY" => amount * _currency.EurToJpy,
                _ => throw new UnkownCurrencyException(currency)
            };
        }
    }
}