using Messenger.Data;
using Messenger.Models;
using Messenger.Repositories;

namespace Messenger.Implementations
{
    public class MessageRepository : Repository<Message>
    {
        public MessageRepository(MessengerDbContext context) : base(context) { }
    }
}
