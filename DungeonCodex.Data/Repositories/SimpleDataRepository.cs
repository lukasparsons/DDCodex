using Dawn;
using DungeonCodex.Common;
using DungeonCodex.Common.Exceptions;
using DungeonCodex.Data.Extensions;
using DungeonCodex.Data.Model.Interface;
using Microsoft.EntityFrameworkCore;

namespace DungeonCodex.Data.Repositories
{
    public class SimpleDataRepository<T> : ISimpleDataRepository<T>
        where T : class, IPersisted, new()
    {
        private readonly DCContext _context;
        private readonly DbSet<T> _set;

        public SimpleDataRepository(DCContext context)
        {
            _context = Guard.Argument(context).NotNull().Value;
            _set = _context.ResolveDbSet<T>();
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.Id = entity.Id.NewIdIfNull();
            _set.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> DeleteAsync(T entity)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public Task<T?> FirstOrDefaultAsync(Func<T, bool> predicate, bool throwIfNotFound = false)
        {
            var entity = _set.WithDefaultIncludes().FirstOrDefault(predicate);
            if (entity is null && throwIfNotFound)
            {
                throw new ResourceNotFoundException<T>($"The provided predicate for type {typeof(T).Name} returned no results.");
            }

            return Task.FromResult(entity);
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            var entities = _set.WithDefaultIncludes();
            return Task.FromResult(entities ?? new List<T>());
        }

        public async Task<T> GetAsync(string id)
        {
            var entity = await _set.FindAsync(id)
                ?? throw new ResourceNotFoundException<T>(id);

            return entity;
        }

        public Task<IEnumerable<T>> GetWhereAsync(Func<T, bool> predicate)
        {
            var entities = _set.Where(predicate);
            return Task.FromResult(entities ?? new List<T>());
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _set.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
    }
}
