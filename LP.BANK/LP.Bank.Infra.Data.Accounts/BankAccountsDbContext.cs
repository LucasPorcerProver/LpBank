using LP.Bank.Domain;
using LP.Bank.Infra.Data.Bank.Configurations.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace LP.Bank.Infra.Data.Bank
{
    public class BankAccountsDbContext : DbContext
    {
        public BankAccountsDbContext(DbContextOptions<BankAccountsDbContext> options) : base(options)
        {
        }

        public DbSet<BankAccount> BankAccounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankAccountConfiguration).GetTypeInfo().Assembly);
        }
    }
}
