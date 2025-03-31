using BalanceTransfer.Models;

namespace BalanceTransfer.Interfaces
{
    public interface ITransactionRepository {
        Task<IEnumerable<Transactions>> GetAllAsync();
        Task CreateTransactionAsync(Transactions transaction);
        Task<Wallet?> GetTransactionByIdAsync(int id);
    }
}
