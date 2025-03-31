namespace BalanceTransfer.Request
{
    public class TransferRequest {
        public int WalletIdFrom { get; set; }
        public int WalletIdTo { get; set; }
        public double Amount { get; set; }
    }
}
