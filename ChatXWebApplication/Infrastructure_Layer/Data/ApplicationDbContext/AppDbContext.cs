using Core_Layer.Entities.Conversation;
using Core_Layer.Entities.Message;
using Core_Layer.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Data.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet <User> Users { get; set; }
        public DbSet <Conversations> Conversations { get; set; }
        public DbSet <Messages> Messages { get; set; }
    }

}
