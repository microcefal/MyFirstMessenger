using Messenger.Interfaces;
using Messenger.IServices;
using Messenger.Models;

namespace Messenger.Services
{
    public class MessageService : BaseService<Message>, IMessageService
    {
        public MessageService(IRepository<Message> repository) : base(repository) { }

        public async Task<IEnumerable<Message>> GetMessagesByChatIdAsync(Guid chatId)
        {
            var messages = await _repository.GetAllAsync();
            return messages.Where(message => message.ChatId == chatId);
        }
    }
}