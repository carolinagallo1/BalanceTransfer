using BalanceTransfer.Models;

namespace BalanceTransfer.Interfaces {
    public interface IWalletRepository {
        Task<IEnumerable<Wallet>> GetAllAsync();
        Task CreateWalletAsync(Wallet wallet);
        Task<Wallet?> GetWalletByIdAsync(int id);
        Task UpdateWalletAsync(Wallet wallet);        
        Task DeleteWalletAsync(int id);        
    }
}
