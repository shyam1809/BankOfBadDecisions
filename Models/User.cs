namespace BankOfBadDecisions.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public decimal Balance { get; set; }
        public int BadCreditScore { get; set; } // Higher = worse in this parody app

        public ICollection<BankTransaction> Transactions { get; set; } = new List<BankTransaction>();
        public ICollection<Badge> Badges { get; set; } = new List<Badge>();
    }
}
