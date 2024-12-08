using BankingSystem.Repositories;

namespace BankingSystem.Services
{
    public class CommandFactory
    {
        private readonly IBankRepository _repository;

        public CommandFactory(IBankRepository repository)
        {
            _repository = repository;
        }

        public ICommand GetCommand(string choice)
        {
            return choice switch
            {
                "T" => new TransactionCommand(_repository),
                "I" => new InterestRuleCommand(_repository),
                "P" => new PrintStatementCommand(_repository),
                _ => null
            };
        }
    }
}
