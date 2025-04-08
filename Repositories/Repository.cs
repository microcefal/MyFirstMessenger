using Messenger.Data;
using Messenger.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Messenger.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly MessengerDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(MessengerDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
        public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        public async Task UpdateAsync(T entity) => _dbSet.Update(entity);
        public async Task DeleteAsync(T entity) => _dbSet.Remove(entity);
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public Task DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
