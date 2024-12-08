using BankingSystem.Models;
using BankingSystem.ViewModels;


namespace BankingSystem.Repositories
{
    public interface IBankRepository
    {
        void AddTransaction(Transaction transaction);
        void AddInterestRule(InterestRule rule);
        Account GetAccount(string accountId);
        IEnumerable<TransactionViewModel> GetTransactions(string accountId, DateTime transactionDate);
        IEnumerable<InterestRuleViewModel> GetInterestRules();
    }
}
