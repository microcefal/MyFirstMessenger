using Messenger.Models;

namespace Messenger.IServices
{
    public interface IAuthService
    {
        Task<User?> Register(string username, string password);
        Task<string?> Login(string username, string password);
    }
}
