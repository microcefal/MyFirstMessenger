using Messenger.Models;

namespace Messenger.IServices
{
    public interface IChatService : IService<Chat>
    {
        Task<IEnumerable<Chat>> GetChatByUserIdAsync(int userdId);
    }
}
