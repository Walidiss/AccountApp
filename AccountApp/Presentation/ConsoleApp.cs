using AccountApp.Core.Interfaces;
using AccountApp.Infrastructure.Services;
using System;
using System.Globalization;

namespace AccountApp.Presentation
{
    public class ConsoleApp
    {
        private readonly IAccountCalculator _calculator;

        public ConsoleApp(IAccountCalculator calculator)
        {
            _calculator = calculator;
        }

        public void Run()
        {
            DateTime date;
            while (true)
            {
                Console.WriteLine("Entrez une date(dd/MM/yyy)" +
                " entre le 01/01/2022 " +
                "et le 01/03/2023:");
                var input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input) || input.ToLower() == "quit"
                    || input.ToLower() == "exit")
                {
                    Console.WriteLine("Au revoir !");
                    return;
                }
                try
                {
                    var culture = (CultureInfo)CultureInfo.InvariantCulture.Clone();
                    culture.DateTimeFormat.DateSeparator = "/";
                    date = DateTime.ParseExact(input.Trim(), "dd/MM/yyyy", culture);

                    // vérifier si la date est dans la plage autorisée
                    var minDate = new DateTime(2022, 1, 1);
                    var maxDate = new DateTime(2023, 3, 1);
                    if (date < minDate || date > maxDate)
                    {
                        Console.WriteLine($"Erreur: la date doit étre comprise entre le " +
                            $"{minDate:dd/MM/yyyy} et le {maxDate:dd/MM/yyyy}.");
                        Console.WriteLine("Veuillez réssayer.");
                        continue;
                    }
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Erreur: Format de date invalide. " +
                        "Utilisez le format dd/MM/yyyy (ex: 28/02/2023).");
                    Console.WriteLine("Veuillez réessayer");

                }
                catch (ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Erreur: Date invalide.Vérifiez le jour, le mois et l'année.");
                    Console.WriteLine("Veuillez réessayer");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erreur: {ex.Message}");
                    Console.WriteLine("Veuillez réessayer");
                }
            }
            try
            {
                var balance = _calculator.GetBalanceAt(date);
                Console.WriteLine($"Solde de compte le {date:dd/MM/yyyy}:{balance:F2} EUR");
                Console.WriteLine("Les 3 plus grands catégories de débit sur tout l'historique:");
                var top = _calculator.GetTopExpenses();
                foreach (var (category, total) in top)
                {
                    Console.WriteLine($"{category}: {total:F2} EUR");
                }
                Console.WriteLine("Voulez-vous essayer une autre date ?(o/n)");
                var response = Console.ReadLine();
                if (response?.ToLower() == "o" || response?.ToLower() == "oui")
                {
                    // ligne vide pour clarité puis recommencer
                    Console.WriteLine();
                    Run();
                }
                else
                {
                    Console.WriteLine("Au revoir!");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de calcul: {ex.Message}");
            }
        }
    }
}