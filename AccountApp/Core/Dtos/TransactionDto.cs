using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountApp.Core.Dtos
{
    public class TransactionDto
    {
        public string Date { get; set; } = string.Empty;
        public string Amount { get; set; } = string.Empty;
        public string Currency { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;

    }
}
