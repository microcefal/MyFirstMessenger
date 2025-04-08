using Microsoft.EntityFrameworkCore;
using Messenger.Models;

namespace Messenger.Data
{
    public class MessengerDbContext : DbContext
    {
        public MessengerDbContext(DbContextOptions<MessengerDbContext> options) : base(options)
        {
            Database.Migrate();
                
        }

       public DbSet<User> Users { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Message>()
       .Property(m => m.SentAt)
       .HasConversion(
           v => v.ToUniversalTime(),  // Сохраняем в UTC
           v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // Достаём из базы как UTC
       );

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Chat>()
                .HasMany(c => c.Users)
                .WithMany(u => u.Chats);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId);

            modelBuilder.Entity<Message>()
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ChatId);
        }
    }

}
