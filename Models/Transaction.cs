using System.ComponentModel.DataAnnotations;

namespace BankingSystem.Models
{
    public class Transaction
    {
        [Key]
        public string TransactionId { get; set; }
        public DateTime Date { get; set; }
        public string AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
