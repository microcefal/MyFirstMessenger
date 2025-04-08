using Messenger.Data;
using Messenger.Models;
using Messenger.Repositories;

namespace Messenger.Implementations
{
    public class UserRepository : Repository<User>
    {
        public UserRepository(MessengerDbContext context) : base(context) { }
    }
}
