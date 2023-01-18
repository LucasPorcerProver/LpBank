using System;
using System.Threading.Tasks;

namespace LP.Bank.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        IBankAccountRepository BankAccountRepository { get; }
        Task Save();
    }
}
