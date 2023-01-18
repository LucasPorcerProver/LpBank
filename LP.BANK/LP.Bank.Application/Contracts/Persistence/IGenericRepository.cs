using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LP.Bank.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(Guid id);
        Task<IReadOnlyList<T>> GetAll();
        Task<T> Add(T entity);
        Task<bool> Exists(Guid id);
        Task Update(T entity);
        Task Delete(T entity);
    }
}
