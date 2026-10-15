using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories;
using Core_Layer.InterfaceRepositories.IGenericRepo;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Infrastructure_Layer.Persistence.ImplementationRepo.GenericRepo;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Layer.Persistence.ImplementationRepo
{
    
    public class UserRepo : Repository<User>,IUserRepo
    {
        private readonly AppDbContext _context;
        public UserRepo(AppDbContext context) : base(context)
        {
            _context=context;
        }
        public async Task<User> CheakDuplication(string identifier)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == identifier || x.UserName == identifier);
            return existingUser;
        }
    }
}
