using Core_Layer.Entities;
using Core_Layer.Enums;
using Core_Layer.InterfaceRepositories;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Infrastructure_Layer.Persistence.ImplementationRepo.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Persistence.ImplementationRepo
{

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

        public async Task<User> ExistingUser(User user)
        {

            var response = await _context.Users.FirstOrDefaultAsync(u => u.UserName == user.UserName || u.Email == user.Email || u.Contact == user.Contact);

            return response!;

        }
        public async Task<bool> IsActiveUserAsync(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId && u.IsActive);
        }


        public async Task<User> GetUserByUserName(string identifier)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == identifier);
            
            if(user is not null)
            {
                return user;
            }
            return null;
        }

        public async Task<List<User>> GetAllPublicUsers()
        {
            return await _context.Users.Where(x=> x.Privacy == PrivacyType.Public).OrderBy(u => Guid.NewGuid()).Take(12).ToListAsync();
        }
    }
}
