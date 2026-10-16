using Core_Layer.Entities;

namespace Application_Layer.Services.Interfaces
{
    public interface IUserServices
    {
        Task<List<User>> GetUsersAsync();
    }
}
