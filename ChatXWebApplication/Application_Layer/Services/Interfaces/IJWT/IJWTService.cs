using ChatX.Application.DTOs.Request;

namespace Application_Layer.Services.Interfaces.IJWT
{
    public interface IJWTService
    {
        string CreateAccessToken(LoginUserRequest request);
        string CreateRefreshToken();
    }
}
