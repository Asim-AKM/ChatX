using Core_Layer.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Data.ApplicationDbContext
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {

        public DbSet<User> Users { get; set; }
        public DbSet<UserConnection> UserConnections { get; set; }
        public DbSet<Notifications> Notifications { get; set; }
        public DbSet<MessageReceipt> MessageReceipts { get; set; }
        public DbSet<ConversationMember> ConversationMembers { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Conversation> Conversations { get; set; }

    }
}

