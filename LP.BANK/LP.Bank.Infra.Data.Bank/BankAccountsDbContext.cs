using LP.Bank.Domain;
using Microsoft.EntityFrameworkCore;

namespace LP.Bank.Infra.Data.Bank
{
    public class BankAccountsDbContext : AuditableDbContext
    {
        public BankAccountsDbContext(DbContextOptions<BankAccountsDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankAccountsDbContext).Assembly);
        }
       
        public DbSet<BankAccount> BankAccounts { get; set; }
    }
}
