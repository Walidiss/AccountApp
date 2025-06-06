using AccountApp.Core.Dtos;
using AccountApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Mappers
{
    public static class TransactionMapper
    {
        public static Transaction ToEntity(TransactionDto transactionDto)
        {

            return new Transaction
            {
                Date = DateTime.Parse(transactionDto.Date),
                Amount = decimal.Parse(transactionDto.Amount),
                Currency = transactionDto.Currency,
                Category = transactionDto.Category,
            };

        }
    }
}
