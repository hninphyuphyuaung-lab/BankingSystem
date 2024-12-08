using BankingSystem.Data;
using BankingSystem.Repositories;
using BankingSystem.Services;

namespace BankingSystem.Controllers
{
    public class AppController
    {
        private readonly CommandFactory _commandFactory;

        public AppController(BankingDbContext dbContext)
        {
            var repository = new BankRepository(dbContext);
            _commandFactory = new CommandFactory(repository);
        }

        public void Run()
        {
            string choice;
            do
            {
                Console.WriteLine();
                Console.WriteLine("Welcome to AwesomeGIC Bank! What would you like to do?");
                Console.WriteLine("[T] Input transactions");
                Console.WriteLine("[I] Define interest rules");
                Console.WriteLine("[P] Print statement");
                Console.WriteLine("[Q] Quit");
                Console.Write("> ");
                choice = Console.ReadLine()?.Trim().ToUpper();

                var command = _commandFactory.GetCommand(choice);
                command?.Execute();
            } while (choice != "Q");

            Console.WriteLine("Thank you for banking with AwesomeGIC Bank. Have a nice day!");
        }
    }
}
