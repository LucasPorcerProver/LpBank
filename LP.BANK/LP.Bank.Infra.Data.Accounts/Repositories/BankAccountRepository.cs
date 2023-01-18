using LP.Bank.Application.Contracts.Persistence;
using LP.Bank.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LP.Bank.Infra.Data.Bank.Repositories
{
    public class BankAccountRepository : GenericRepository<BankAccount>, IBankAccountRepository
    {
        private readonly BankAccountsDbContext _dbContext;

        public BankAccountRepository(BankAccountsDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task CreateNew(BankAccount account)
        {
            await _dbContext.AddAsync(account);
        }

        public async Task<bool> AccountExists(Guid accountId)
        {
            return await _dbContext.BankAccounts.AnyAsync(q => q.Id == accountId);
        }

        public async Task<bool> AccountNumberExists(int accountNumber)
        {
            return await _dbContext.BankAccounts.AnyAsync(q => q.Number == accountNumber);
        }

        public void UpdateAccount(BankAccount account)
        {
            _dbContext.Update(account);
        }
    }
}
