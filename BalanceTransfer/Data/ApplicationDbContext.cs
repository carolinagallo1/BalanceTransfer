using BalanceTransfer.Models;
using Microsoft.EntityFrameworkCore;

namespace BalanceTransfer.Server.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }



        public DbSet<Wallet> Wallet { get; set; }
        public DbSet<Transactions> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transactions>()
                .HasOne<Wallet>()
                .WithMany()
                .HasForeignKey(t => t.WalletId)
                .IsRequired();

        }

    }
}