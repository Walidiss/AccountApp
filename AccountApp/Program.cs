using AccountApp.Application.Services;
using AccountApp.Core.Interfaces;
using AccountApp.Infrastructure.Repositories;
using AccountApp.Infrastructure.Services;
using AccountApp.Presentation;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();

// le path vers le fichier csv

string csvFilePath = "account_20230228.csv";

services.AddSingleton<ItransactionLoader>(provider => new
CsvTransactionLoader(csvFilePath));

services.AddSingleton<ICurrencyConvertor, ExchangeRateService>();
services.AddSingleton<IAccountCalculator, AccountCalculator>();
services.AddSingleton<ConsoleApp>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<ConsoleApp>();
app?.Run();

