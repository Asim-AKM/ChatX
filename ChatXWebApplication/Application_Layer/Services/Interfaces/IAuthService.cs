
using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using ChatX.Application.DTOs.Request;

namespace Application_Layer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUserAsync(RegisterUserRequest request);
        Task<LoginResponse> LoginUserAsync(LoginUserRequest request);
    }
}
