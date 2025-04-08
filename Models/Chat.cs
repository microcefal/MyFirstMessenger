namespace Messenger.Models
{
    public class Chat
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<User> Users { get; set; } = new();
        public List<Message> Messages { get; set; } = new();
    }
}
