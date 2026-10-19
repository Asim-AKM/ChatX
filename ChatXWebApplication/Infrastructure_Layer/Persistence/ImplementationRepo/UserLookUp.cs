using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Infrastructure_Layer.Persistence.ImplementationRepo.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Persistence.ImplementationRepo
{
    public class UserLookUp :Repository<User>,IUserLookUp
    {
        private readonly AppDbContext _context;

        public UserLookUp(AppDbContext context):base(context)
        {
            _context = context;
        }

        public async Task<bool> IsActiveUserAsync(Guid userId)
        {
            return await _context.Users.AnyAsync(u => u.UserId == userId && u.IsActive);
        }
    }
}
