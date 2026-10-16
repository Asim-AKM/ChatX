using Application_Layer.Services.Interfaces;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public Task<List<User>> GetUsersAsync()
        {
            return _unitOfWork.UserRepo.GetAllAsync();
        }
    }
}
