using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class InterestRule
    {
        [Key]
        public DateTime Date { get; set; }
        public string RuleId { get; set; }
        public decimal Rate { get; set; }
    }
}
