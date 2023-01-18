using LP.Bank.Application.Constants;
using LP.Bank.Application.Contracts.Persistence;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LP.Bank.Infra.Data.Bank.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly BankAccountsDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private IBankAccountRepository _bankAccountRepository;     


        public UnitOfWork(BankAccountsDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            this._httpContextAccessor = httpContextAccessor;
        }

        public IBankAccountRepository BankAccountRepository => 
            _bankAccountRepository ??= new BankAccountRepository(_context);
        
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task Save() 
        {
            var username = _httpContextAccessor.HttpContext.User.FindFirst(CustomClaimTypes.Uid)?.Value;

            await _context.SaveChangesAsync(username);
        }
    }
}
