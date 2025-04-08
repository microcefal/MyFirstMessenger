using Messenger.Models;

namespace Messenger.IServices
{
  
    public interface IMessageService : IService<Message>
    {
        Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId);
    }
}
