using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InventoryMaster360.Repositories
{
    public interface IGenericRepository<T> where T : class
        //bunun anlamı --> bana göndereceğin şey sadece class olabilir, random bi sayı veya bir string gönderemeszsin.

    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
        Task SaveAsync();
        Task AddAsync(T entity);
        void Remove(T entity);
        T Update(T entity);

    }
}
