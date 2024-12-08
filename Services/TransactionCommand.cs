using BankingSystem.Models;
using BankingSystem.Repositories;

namespace BankingSystem.Services
{
    public class TransactionCommand : ICommand
    {
        private readonly IBankRepository _repository;

        public TransactionCommand(IBankRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
        Console.WriteLine("Please enter transaction details in <Date> <Account> <Type> <Amount> format (or blank to go back):");
        Console.Write("> ");
  
        var input = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(input)) return;

        var parts = input.Split(' ');
        if (parts.Length != 4 || !ValidateTransactionInput(parts, out var transaction)) return;

        _repository.AddTransaction(transaction);
        Console.WriteLine("Transaction added successfully!");
        }

        private bool ValidateTransactionInput(string[] parts, out Transaction transaction)
        {
            transaction = null;
            if (!DateTime.TryParseExact(parts[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var transactionDate))
            {
                Console.WriteLine("Date must be in YYYYMMdd format.");
                return false;
            }

            if (parts[2].ToUpper() != "D" && parts[2].ToUpper() != "W")
            {
                Console.WriteLine("Invalid transaction type. Use 'D' for deposit or 'W' for withdrawal.");
                return false;
            }

            if (!decimal.TryParse(parts[3], out var amount) || amount <= 0)
            {
                Console.WriteLine("Transaction amount must be greater than zero and up to 2 decimal places.");
                return false;
            }

            transaction = new Transaction
            {
                Date = transactionDate,
                AccountId = parts[1],
                Type = parts[2].ToUpper(),
                Amount = amount
            };

            return true;
        }
    }
}
