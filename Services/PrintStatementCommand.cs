using System;
using BankingSystem.Repositories;

namespace BankingSystem.Services
{
    public class PrintStatementCommand : ICommand
    {
        private readonly IBankRepository _repository;

        public PrintStatementCommand(IBankRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.WriteLine("Please enter account and month to generate the statement <Account> <Year><Month> (or blank to go back):");
            Console.Write("> ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;

            var parts = input.Split(' ');
            if (parts.Length != 2)
            {
                Console.WriteLine("Invalid input format. Example: AC001 202312");
                return;
            }

            var accountId = parts[0];
            var yearMonth = parts[1];
            if (yearMonth.Length != 6 || !int.TryParse(yearMonth, out _))
            {
                Console.WriteLine("Invalid YearMonth format. Use YYYYMM.");
                return;
            }

            PrintAccountStatement(accountId, yearMonth);
        }

        private void PrintAccountStatement(string accountId, string yearMonth)
        {
            var account = _repository.GetAccount(accountId);
            if (account == null)
            {
                Console.WriteLine($"Account {accountId} not found.");
                return;
            }

            Console.WriteLine($"Account: {account.AccountId}");
            Console.WriteLine("| Date       | Txn Id       | Type | Amount   | Balance   |");
            Console.WriteLine("----------------------------------------------------------");

            DateTime transactionDate = DateTime.ParseExact(yearMonth, "yyyyMM", null);

            var transactions = _repository.GetTransactions(accountId, transactionDate);
            decimal runningBalance = account.Balance;

            foreach (var txn in transactions)
            {
                runningBalance += txn.Type == "D" ? txn.Amount : -txn.Amount;

                Console.WriteLine($"| {txn.Date} | {txn.TransactionId,-12} | {txn.Type,-4} | {txn.Amount,8:F2} | {runningBalance,8:F2} |");
            }
        }
    }
}
