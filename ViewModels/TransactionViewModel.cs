namespace BankingSystem.ViewModels
{
    public class TransactionViewModel
    {
        public string TransactionId { get; set; }
        public string Date { get; set; }
        public string AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
    }
}
