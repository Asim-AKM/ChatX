using Core_Layer.Entities;

namespace Core_Layer.InterfaceRepositories
{
    public interface IUserRepo 
    {
        Task<User> CheakDuplication(string identifier);
        Task<User> GetUserByCreads(User user);
    }
}
