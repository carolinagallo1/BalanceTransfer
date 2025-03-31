using System.ComponentModel.DataAnnotations;

namespace BalanceTransfer.Request
{
    public class WalletRequest {
        public int Id { get; set; } = 0;
        public string DocumentId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public double Balance { get; set; } = 0;
    }
}
