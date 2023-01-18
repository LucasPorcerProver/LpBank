using LP.Bank.Domain.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LP.Bank.Infra.Data.Bank
{
    public abstract class AuditableDbContext : DbContext
    {
        public AuditableDbContext(DbContextOptions options) : base(options)
        {
        }

        public virtual async Task<int> SaveChangesAsync(string username = "SYSTEM")
        {
            foreach (var entry in base.ChangeTracker.Entries<BaseDomainEntity>()
                .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.DateCreated = DateTime.Now;
                    entry.Entity.Id = Guid.NewGuid();
                }
            }

            var result = await base.SaveChangesAsync();

            return result;
        }
    }
}
