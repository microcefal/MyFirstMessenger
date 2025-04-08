using Messenger.Interfaces;
using Messenger.IServices;
using Messenger.Models;

namespace Messenger.Services
{
    public class ChatService : BaseService<Chat>, IChatService
    {
        public ChatService(IRepository<Chat> repository) : base(repository) { }

        public async Task<IEnumerable<Chat>> GetChatByUserIdAsync(int userId)
        {
            var chats = await _repository.GetAllAsync();
            return chats.Where(chat => chat.Users.Any(user => user.Id == Guid.Parse(userId.ToString())));
        }
    }
}
