using AccountApp.Core.Dtos;
using AccountApp.Core.Entities;
using AccountApp.Core.Interfaces;
using AccountApp.Core.Mappers;
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
                var lines = File.ReadAllLines(_path).Skip(4);
                return lines.Where(line => !string.IsNullOrWhiteSpace(line))
                           .Select((line, index) =>
                           {
                               try
                               {
                                   var parts = line.Split(';');
                                   if (parts.Length < 4)
                                   {
                                       throw new FormatException($"Ligne {index + 5} : pas assez de colonnes ({parts.Length}/4)");
                                   }

                                   var dto = new TransactionDto
                                   {
                                       Date = parts[0],
                                       Amount = parts[1],
                                       Currency = parts[2],
                                       Category = parts[3]
                                   };

                                   return TransactionMapper.ToEntity(dto);
                               }
                               catch (Exception ex)
                               {
                                   throw new FormatException($"Erreur ligne {index + 5}: '{line}' - {ex.Message}", ex);
                               }
                           }).ToList();
            }
            catch (Exception ex)
            {
                throw new FileLoadException($"Erreur lors du chargement du fichier {_path}: {ex.Message}", ex);
            }
        }
    }
}