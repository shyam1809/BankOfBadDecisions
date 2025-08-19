namespace BankOfBadDecisions.Models
{
    public class BankTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime Date { get; set; } = DateTime.UtcNow;
        public decimal Amount { get; set; } // negative = expense/fee, positive = income/loan
        public string Category { get; set; } = "General";
        public string Description { get; set; } = string.Empty;

        public bool IsFee => Category.ToLower().Contains("fee");
        public bool IsLoan => Category.ToLower().Contains("loan");
        public bool IsExpense => Amount < 0 && !IsFee && !IsLoan;
    }
}
