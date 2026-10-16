using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using Application_Layer.Services.Interfaces;
using ChatX.Application.DTOs.Request;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories.IGenericRepo;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using Core_Layer.InterfaceSecurity;


namespace Application_Layer.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        public AuthService(IUnitOfWork unitOfWork, IPasswordService passwordService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
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
                return "User Login Successfully";

            }

            return "Invalid Credentials";
        }

        public Task<AuthenticationResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}