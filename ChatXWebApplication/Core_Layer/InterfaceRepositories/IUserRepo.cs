using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories.IGenericRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.InterfaceRepositories
{
    public interface IUserRepo : IRepository<User>
    {
        Task<User> CheakDuplication(string identifier);
        Task<User> ExistingUser(string user);
        Task<bool> IsActiveUserAsync(Guid userId);
        Task<User> GetUserByUserName(string identifier);

    }
}
