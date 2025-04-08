using Messenger.Models;

namespace Messenger.IServices
{
    public interface IUserService : IService<User>
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> SearchUserByNameAsync(string name);
    }
}
