using Application_Layer.DTO_s.Request;
using Application_Layer.Mappers;
using Application_Layer.Services.Interfaces;
using Application_Layer.Services.Interfaces.IJWT;
using ChatX.Application.DTOs.Request;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using Core_Layer.InterfaceSecurity;


namespace Application_Layer.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private IJWTService _jWTService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        public AuthService(IUnitOfWork unitOfWork, IPasswordService passwordService,IJWTService jWTService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jWTService= jWTService;
        }

        public async Task<string> LoginUserAsync(LoginUserRequest request)
        {
            var user = await _unitOfWork.UserRepo.CheakDuplication(request.Identifier);

            if (user is not null)
            {


                var response = _passwordService.VerifyPassword(request.Password, user.PasswordHash!);

                if (!response)
                {
                    return "Invalid Creadentials";
                }
                var accessToken = _jWTService.CreateAccessToken(request);
                var refreshToken = _jWTService.CreateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);
                await _unitOfWork.SaveChangesAsync();

                return accessToken;

            }

            return "Invalid Credentials";
        }

        public async Task<string> RegisterUserAsync(RegisterUserRequest request)
        {
            var user = request.Map();
            var existingUser = await _unitOfWork.UserRepo.ExistingUser(user);

            if (existingUser == null)
            {
                
            user.PasswordHash = _passwordService.HashPassword(request.Password);

            await _unitOfWork.UserRepo.CreateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return "User registered successfully";
            }
            return "User already exists";
        }

        public async Task<string> LogoutAsync(string identifier)
        {
            var user = await _unitOfWork.UserRepo.CheakDuplication(identifier);
            if (user is null)
            return "User not found";

            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            await _unitOfWork.SaveChangesAsync();

            return "Logged out successfully";
        }
    }
}