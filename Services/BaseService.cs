using Messenger.Interfaces;
using Messenger.IServices;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Messenger.Services
{
    public class BaseService<T> : IService<T> where T : class
    {
        protected readonly IRepository<T> _repository;
        public BaseService(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T> GetByIdAsync(Guid id) => await _repository.GetByIdAsync(id);
        public async Task<IEnumerable<T>> GetAllAsync() => await _repository.GetAllAsync();
        public async Task AddAsync(T entity) => await _repository.AddAsync(entity);
        public async Task UpdateAsync(T entity) => await _repository.UpdateAsync(entity);
        public async Task DeleteAsync(Guid id) => await _repository.DeleteAsync(id);
    }

}

