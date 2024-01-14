using DungeonCodex.Data.Model.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonCodex.Data.Repositories
{
    public interface ISimpleDataRepository<T>
        where T : class, IPersisted, new()
    {
        Task<T> GetAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetWhereAsync(Func<T, bool> predicate);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<T> DeleteAsync(T entity);
        Task<T?> FirstOrDefaultAsync(Func<T, bool> predicate, bool throwIfNotFound = false);
    }
}
