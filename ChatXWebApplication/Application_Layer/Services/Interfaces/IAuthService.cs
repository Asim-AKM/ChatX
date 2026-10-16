
using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using ChatX.Application.DTOs.Request;

namespace Application_Layer.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResponse> RegisterUserAsync(RegisterUserRequest request);
        Task<string> LoginUserAsync(LoginUserRequest request);
    }
}
