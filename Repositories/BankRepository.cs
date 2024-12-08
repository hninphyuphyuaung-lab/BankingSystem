using BankingSystem.Data;
using BankingSystem.Models;
using BankingSystem.ViewModels;

namespace BankingSystem.Repositories
{
    public class BankRepository : IBankRepository
    {
        private readonly BankingDbContext _dbContext;

        public BankRepository(BankingDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddTransaction(Transaction transaction)
        {
           // Check if the account exists
            var account = _dbContext.Accounts.SingleOrDefault(a => a.AccountId == transaction.AccountId);

            if (account == null)
            {
                if (transaction.Type.ToUpper() == "W")
                {
                    throw new InvalidOperationException("Cannot process withdrawal for a non-existing account.");
                }

                // Create a new account
                account = new Account
                {
                    AccountId = transaction.AccountId,
                    Balance = 0 // Initial balance
                };
                _dbContext.Accounts.Add(account);
            }

            // Update account balance based on the transaction type
            if (transaction.Type.ToUpper() == "D") // Deposit
            {
                account.Balance += transaction.Amount;
            }
            else if (transaction.Type.ToUpper() == "W") // Withdrawal
            {
                if (account.Balance < transaction.Amount)
                {
                    Console.WriteLine("Insufficient balance for withdrawal.");
                    return;
                }
                account.Balance -= transaction.Amount;
            }

            // Generate unique transaction ID
            var transactionDate = transaction.Date.ToString("yyyyMMdd");
            var transactionCount = _dbContext.Transactions
                                   .Count(t => t.AccountId == transaction.AccountId &&
                                                 t.Date.Year == transaction.Date.Year &&
                                                 t.Date.Month == transaction.Date.Month &&
                                                 t.Date.Day == transaction.Date.Day) + 1;
            transaction.TransactionId = $"{transactionDate}-{transactionCount:D2}";

            _dbContext.Transactions.Add(transaction);
            _dbContext.SaveChanges();
        }


        public void AddInterestRule(InterestRule rule)
        {
            var existingRule = _dbContext.InterestRules.SingleOrDefault(r => r.Date == rule.Date);
            if (existingRule != null) _dbContext.InterestRules.Remove(existingRule);

            _dbContext.InterestRules.Add(rule);
            _dbContext.SaveChanges();
        }

        public Account GetAccount(string accountId)
        {
            return _dbContext.Accounts.SingleOrDefault(a => a.AccountId == accountId);
        }

        public IEnumerable<TransactionViewModel> GetTransactions(string accountId, DateTime transactionDate)
        {
            return _dbContext.Transactions.Where(t => t.AccountId == accountId && t.Date.Year == transactionDate.Year && t.Date.Month == transactionDate.Month)
                .Select(transaction => new TransactionViewModel
                {
                    TransactionId = transaction.TransactionId,
                    Date = transaction.Date.ToString("yyyyMMdd"),
                    AccountId = transaction.AccountId,
                    Type = transaction.Type,
                    Amount = transaction.Amount
                })
                .ToList();
        }

        public IEnumerable<InterestRuleViewModel> GetInterestRules()
        {
            return _dbContext.InterestRules.OrderBy(i => i.Date).Select(rule => new InterestRuleViewModel
            {
                RuleId = rule.RuleId,
                Date = rule.Date.ToString("yyyMMdd"),
                Rate = rule.Rate
            }).ToList();
        }
    }
}
