using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using Application_Layer.Mappers;
using Application_Layer.Services.Interfaces;
using Application_Layer.Services.Interfaces.IJWT;
using ChatX.Application.DTOs.Request;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories.IGenericRepo;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using Core_Layer.InterfaceSecurity;


namespace Application_Layer.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private IJWTService _jWTService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        public AuthService(IUnitOfWork unitOfWork, IPasswordService passwordService, IJWTService jWTService)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _jWTService = jWTService;
        }

        //public async Task<string> LoginUserAsync(LoginUserRequest request)
        //{
        //    var user = await _unitOfWork.UserRepo.CheakDuplication(request.Identifier);

        //    if (user is not null)
        //    {


        //        var response = _passwordService.VerifyPassword(request.Password, user.PasswordHash!);

        //        if (!response)
        //        {
        //            return "Invalid Creadentials";
        //        }

        //        var token = _jWTService.CreateAccessToken(request);

        //        return ($"User Login Successfully {token} ");

        //    }

        //    return "Invalid Credentials";
        //}


        public async Task<LoginResponse> LoginUserAsync(LoginUserRequest request)
        {
            var user = await _unitOfWork.UserRepo.CheakDuplication(request.Identifier);

            if (user is null || !_passwordService.VerifyPassword(request.Password, user.PasswordHash!))
                return new LoginResponse { Success = false, Message = "Invalid Credentials" };

            return new LoginResponse
            {
                Success = true,
                Message = "User Login Successfully",
                UserId = user.UserId,
                Identifier = request.Identifier
            };
        }


        public async Task<string> RegisterUserAsync(RegisterUserRequest request)
        {
            var existingUser = await _unitOfWork.UserRepo.ExistingUser(request.Email);

            if (existingUser == null)
            {
                var username = UsernameGenerator(request.FirstName, request.LastName);
                var displayname = DisplayName(request.FirstName, request.LastName);
                var user = request.Map(displayname,username);

                user.PasswordHash = _passwordService.HashPassword(request.Password);

                await _unitOfWork.UserRepo.CreateAsync(user);
                await _unitOfWork.SaveChangesAsync();

                return "User registered successfully";
            }
            return "User already exists";

        }
        private string UsernameGenerator(string firstname, string lastname)
        {
            string username = $"{firstname.Trim()}{lastname.Trim()}".ToLowerInvariant().Replace(" ", "");
            return $"{username}{Random.Shared.Next(1000, 9999)}";
        }

        private string DisplayName(string firstname, string lastname)
        {
            return $"{firstname.Trim()} {lastname.Trim()}".Trim();
        }


    }
}