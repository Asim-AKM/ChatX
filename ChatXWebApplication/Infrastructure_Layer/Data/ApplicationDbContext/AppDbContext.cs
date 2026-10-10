using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Data.ApplicationDbContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {
            
        }
    }
}
