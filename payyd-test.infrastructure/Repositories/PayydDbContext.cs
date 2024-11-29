using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.EntityFrameworkCore;
using payyd_test.domain.Model;
using payyd_test.domain.Model.Transactions;
using payyd_test.domain.Model.Users;
using payyd_test.domain.Model.Wallet;
using payyd_test.domain.Model.Webhook;
namespace payyd_test.infrastructure.Repositories
{
    public class PayydDbContext : DbContext
    {

        public DbSet<UserModel> UserModel { get; set; }
        public DbSet<WalletModel> WalletModel { get; set; }
        public DbSet<WebHookModel> WebHookModel { get; set; }
        public DbSet<TransactionModel> TransactionModel { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=MyLocalDb;Trusted_Connection=True;");
        }
    }
}
