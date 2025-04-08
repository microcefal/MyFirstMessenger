using Messenger.Data;
using Messenger.Repositories;

namespace Messenger.Implementations
{
    public class ChatRepository : Repository<Models.Chat>
    {
        public ChatRepository(MessengerDbContext context) : base(context) { }
    }
}
