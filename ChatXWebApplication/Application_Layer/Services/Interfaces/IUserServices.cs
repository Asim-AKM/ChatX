using Application_Layer.DTO_s.Response;
using Core_Layer.Entities;

namespace Application_Layer.Services.Interfaces
{
    public interface IUserServices
    {
        Task<List<UserDataResponse>> GetUsersAsync();
        Task<UserDataResponse> GetUserByUserName(string Identifier);
    }
}
