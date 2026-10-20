using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories.IGenericRepo;

namespace Core_Layer.InterfaceRepositories
{
    public interface IUserLookUp:IRepository<User>
    {
        Task<bool> IsActiveUserAsync(Guid userId);
    }
}
