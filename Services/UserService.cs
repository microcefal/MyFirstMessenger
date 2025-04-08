using Messenger.Interfaces;
using Messenger.IServices;
using Messenger.Models;

namespace Messenger.Services
{
    public class UserService : BaseService<User>, IUserService
    {
       public UserService(IRepository<User> repository) : base(repository) { }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var users = await _repository.GetAllAsync();
            return users.FirstOrDefault(x => x.Email == email);
        }
        public async Task<IEnumerable<User>> SearchUserByNameAsync(string name)
        {
            var users = await _repository.GetAllAsync();
            return users.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
