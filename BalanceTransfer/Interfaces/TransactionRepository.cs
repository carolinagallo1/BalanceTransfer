using BalanceTransfer.Models;
using BalanceTransfer.Server.Data;
using Microsoft.EntityFrameworkCore;

namespace BalanceTransfer.Interfaces
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;
        public TransactionRepository(ApplicationDbContext context) {
            _context = context;
        }
        /// <summary>
        /// Creates a new transaction in db
        /// </summary>
        /// <param name="wallet">object to create</param>
        /// <returns></returns>
        public async Task CreateTransactionAsync(Transactions transaction) {
            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();
        }
        /// <summary>
        /// Gets all transactions of db
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Transactions>> GetAllAsync() => await _context.Transactions.ToListAsync();
        /// <summary>
        /// Gets all transactions of db when id be equals to request
        /// </summary>
        /// <param name="id">item for filter in db</param>
        /// <returns></returns>
        public async Task<Wallet?> GetTransactionByIdAsync(int id) => await _context.Wallet.FindAsync(id);
    }
}
