namespace BalanceTransfer.Models
{
    public class Transactions {
        public int Id { get; set; }
        public int WalletId { get; set; }
        public double Amount { get; set; }
        public string Type {  get; set; } = string.Empty;
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
