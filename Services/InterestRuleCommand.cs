using System;
using BankingSystem.Models;
using BankingSystem.Repositories;

namespace BankingSystem.Services
{
    public class InterestRuleCommand : ICommand
    {
        private readonly IBankRepository _repository;

        public InterestRuleCommand(IBankRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            Console.WriteLine("Please enter interest rules details in <Date> <RuleId> <Rate in %> format (or blank to go back):");
            Console.Write("> ");
                
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input)) return;

            var parts = input.Split(' ');
            if (parts.Length != 3 || !ValidateInterestRuleInput(parts, out var interestRule)) return;

            _repository.AddInterestRule(interestRule);
            Console.WriteLine("Interest rule added successfully!");
            PrintInterestRules();
            
        }

        private bool ValidateInterestRuleInput(string[] parts, out InterestRule interestRule)
        {
            interestRule = null;
            if (!DateTime.TryParseExact(parts[0], "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out _))
            {
                Console.WriteLine("Invalid date format.");
                return false;
            }

            if (!decimal.TryParse(parts[2], out var rate) || rate <= 0 || rate >= 100)
            {
                Console.WriteLine("Invalid rate. Must be greater than 0 and less than 100.");
                return false;
            }

            interestRule = new InterestRule
            {
                Date = DateTime.ParseExact(parts[0], "yyyyMMdd", null),
                RuleId = parts[1],
                Rate = rate
            };

            return true;
        }

        private void PrintInterestRules()
        {
            var rules = _repository.GetInterestRules();
            Console.WriteLine("Interest Rules:");
            Console.WriteLine("| Date       | RuleId    | Rate (%) |");
            foreach (var rule in rules)
            {
                Console.WriteLine($"| {rule.Date} | {rule.RuleId,-10} | {rule.Rate,8:F2} |");
            }
        }
    }
}
