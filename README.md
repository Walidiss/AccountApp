# AccountApp

Une application console .NET pour la gestion et l'analyse de comptes bancaires.

## 📋 Fonctionnalités

- **Calcul de solde** : Obtenir le solde du compte à une date donnée
- **Analyse des dépenses** : Afficher les 3 principales catégories de débit
- **Conversion multi-devises** : Support EUR, USD, JPY avec taux de change
- **Interface console interactive** : Navigation simple et intuitive

## 🏗️ Architecture

L'application suit une architecture en couches (Clean Architecture) :

```
AccountApp/
├── Core/
│   ├── Entities/           # Entités (Transaction, Currency)
│   ├── Interfaces/         # Contrats (IAccountCalculator, ICurrencyConvertor)
│   ├── Dtos/              # Objets de transfert de données
│   └── Mappers/           # Conversion DTO → Entity
├── Application/            # Couche application
│   └── Services/          # Services métier (AccountCalculator)
├── Infrastructure/         # Couche infrastructure
│   ├── Repositories/      # Accès aux données (CsvTransactionLoader)
│   └── Services/          # Services techniques (ExchangeRateService)
└── Presentation/          # Couche présentation
    └── ConsoleApp.cs      # Interface utilisateur console
```

## 🚀 Installation et utilisation

### Prérequis

- .NET 6.0 ou supérieur
- Un fichier CSV au format spécifique (voir structure ci-dessous)

### Démarrage

1. **Cloner le projet** :
   ```bash
   git clone https://github.com/Walidiss/AccountApp.git
   cd AccountApp
   ```

2. **Placer votre fichier CSV** `account_20230228.csv` dans le répertoire de sortie

3. **Compiler et exécuter** :
   ```bash
   dotnet build
   dotnet run
   ```

### Utilisation

1. L'application vous demande une date (format `dd/MM/yyyy`)
2. Entrez une date entre le `01/01/2022` et le `01/03/2023`
3. Consultez le solde et l'analyse des dépenses
4. Tapez `quit` ou `exit` pour quitter
