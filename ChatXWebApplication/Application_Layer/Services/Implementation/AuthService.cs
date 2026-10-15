using Application_Layer.CommanClasses.Mappers;
using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using Application_Layer.Services.Interfaces;
using ChatX.Application.DTOs.Request;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories;
using Core_Layer.InterfaceRepositories.IGenericRepo;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using Core_Layer.InterfaceSecurity;


namespace Application_Layer.Services.Implementation
{
    public class AuthService : IAuthService
    { private readonly IUserRepo _userRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordService _passwordService;
        private readonly IRepository<User> _repository;
        public AuthService(IUnitOfWork unitOfWork, IPasswordService passwordService ,IUserRepo userRepo,IRepository<User> repository)
        {
            _unitOfWork = unitOfWork;
            _passwordService = passwordService;
            _userRepo = userRepo;
            _repository = repository;
        }

        public async Task<AuthenticationResponse> LoginUserAsync(LoginUserRequest request)
        {
            var user = await _unitOfWork.UserRepo.CheakDuplication(request.Identifier);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username/email or password.");
            }
            var response = _passwordService.VerifyPassword(request.Password, user.PasswordHash);

            if (!response)
            {
                throw new UnauthorizedAccessException("Invalid username/email or password.");
            }

            return new AuthenticationResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                Email = user.Email
            };
        }


        public async Task<AuthenticationResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            request.Password= _passwordService.HashPassword(request.Password);
            var response = await _userRepo.GetUserByCreads(request.MapToUserDomain());
            if(response == null)
            {
               await _repository.CreateAsync(response!); 
                return new AuthenticationResponse
                {
                    UserId= response!.UserId,
                    UserName=response.UserName!,
                    ProfileImageUrl=response.ProfileImageUrl,
                    Email=response.Email!
                };
            }
            return new AuthenticationResponse();

        }

    }
}