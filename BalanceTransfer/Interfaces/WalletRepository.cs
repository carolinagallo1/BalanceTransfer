using BalanceTransfer.Models;
using BalanceTransfer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BalanceTransfer.Interfaces
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context) {
                _context = context;
        }
        /// <summary>
        /// Creates a new wallet in db
        /// </summary>
        /// <param name="wallet">object to create</param>
        /// <returns></returns>
        public async Task CreateWalletAsync(Wallet wallet)
        {
            _context.Wallet.Add(wallet);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Deletes a wallet of db
        /// </summary>
        /// <param name="id">id to wallet to remove</param>
        /// <returns></returns>
        public async Task DeleteWalletAsync(int id)
        {
            var wallet = await _context.Wallet.FindAsync(id);
            if(wallet != null) {
                _context.Wallet.Remove(wallet);
                await _context.SaveChangesAsync();
            }
        }
        /// <summary>
        /// Gets all wallets of db when id be equals to request
        /// </summary>
        /// <param name="id">item for filter in db</param>
        /// <returns></returns>
        public async Task<Wallet?> GetWalletByIdAsync(int id) => await _context.Wallet.FindAsync(id);

        /// <summary>
        /// Gets all wallets of db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Wallet>> GetAllAsync() => await _context.Wallet.ToListAsync();       

        /// <summary>
        /// Updates an especific wallet
        /// </summary>
        /// <param name="wallet">wallet to modify</param>
        /// <returns></returns>
        public async Task UpdateWalletAsync(Wallet wallet) { 
                _context.Wallet.Update(wallet);
                await _context.SaveChangesAsync();
            
        }
    }
}
