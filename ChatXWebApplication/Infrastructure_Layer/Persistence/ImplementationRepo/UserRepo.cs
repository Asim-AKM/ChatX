using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Persistence.ImplementationRepo
{
    
    public class UserRepo : IUserRepo

    public class UserRepo : Repository<User>, IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<User> CheakDuplication(string identifier)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == identifier || x.UserName == identifier || x.Contact == identifier);
            return existingUser;
        }
        public async Task<User> GetUserByCreads(User user)
        {
            var response = await _context.Users.
                  FirstOrDefaultAsync
                  (u => u.UserName == user.UserName
                  || u.Email == user.Email);

            return response!;
        }

    }
}
