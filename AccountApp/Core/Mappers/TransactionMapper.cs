using AccountApp.Core.Dtos;
using AccountApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Mappers
{
    public static class TransactionMapper
    {
        public static Transaction ToEntity(TransactionDto transactionDto)
        {
            try
            {
                return new Transaction
                {
                    Date = DateTime.ParseExact(transactionDto.Date, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Amount = decimal.Parse(transactionDto.Amount, CultureInfo.InvariantCulture),
                    Currency = transactionDto.Currency?.Trim() ?? string.Empty,
                    Category = transactionDto.Category?.Trim() ?? string.Empty,
                };
            }
            catch (FormatException ex)
            {
                throw new FormatException($"Erreur de conversion- Date: '{transactionDto.Date}', Amount: '{transactionDto.Amount}' - {ex.Message}", ex);
            }
        }
    }
}
