using LP.Bank.Domain;
using System;
using System.Threading.Tasks;

namespace LP.Bank.Application.Contracts.Persistence
{
    public interface IBankAccountRepository : IGenericRepository<BankAccount>
    {
        Task CreateNew(BankAccount account);
        Task<bool> AccountExists(Guid accountId);
        void UpdateAccount(BankAccount account);
        Task<bool> AccountNumberExists(int accountNumber);
    }
}
