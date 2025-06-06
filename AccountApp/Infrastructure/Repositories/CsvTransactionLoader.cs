using AccountApp.Core.Entities;
using AccountApp.Core.Interfaces;
using System.Globalization;

namespace AccountApp.Infrastructure.Repositories
{
    public class CsvTransactionLoader : ItransactionLoader
    {
        private readonly string _path;

        public CsvTransactionLoader(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Extrait les taux de change de fichier csv
        /// </summary>
        /// <returns></returns>
        public Currency GetExchangeRate()
        {
            var lines = File.ReadLines(_path).ToList();
            var jpyLine = lines[1];
            var usdLine = lines[2]; 

            return new Currency
            {
                EurToJpy = decimal.Parse(jpyLine.Split(':')[1].Trim(), CultureInfo.InvariantCulture), 
                EurToUsd = decimal.Parse(usdLine.Split(':')[1].Trim(), CultureInfo.InvariantCulture)  
            };
        }

        /// <summary>
        /// Extrait le solde initial de compte depuis le csv
        /// </summary>
        /// <returns></returns>
        public decimal GetInitialBalance()
        {
            var line = File.ReadLines(_path).First();
            var value = line.Split(':')[1].Split("EUR")[0].Trim();
            return decimal.Parse(value, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// charger toutes les transactions depuis le fichier csv
        /// </summary>
        /// <returns></returns>
        /// <exception cref="FileLoadException"></exception>
        public List<Transaction> LoadTransctions()
        {
            try
            {
                var lines = File.ReadAllLines(_path).Skip(5);
                return lines.Where(line => !string.IsNullOrWhiteSpace(line))
                           .Select(line =>
                           {
                               var parts = line.Split(';');
                               return new Transaction
                               {
                                   Date = DateTime.Parse(parts[0]),
                                   Amount = decimal.Parse(parts[1], CultureInfo.InvariantCulture),
                                   Currency = parts[2],
                                   Category = parts[3]
                               };
                           }).ToList();
            }
            catch
            {
                throw new FileLoadException(_path);
            }
        }
    }
}