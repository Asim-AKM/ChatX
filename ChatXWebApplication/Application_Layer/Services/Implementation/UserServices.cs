using Application_Layer.DTO_s.Response;
using Application_Layer.Services.Interfaces;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;

namespace Application_Layer.Services.Implementation
{
    public class UserServices : IUserServices
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDataResponse> GetUserByUserName(string Identifier)
        {
            var user = await _unitOfWork.UserRepo.GetUserByUserName(Identifier);

            if (user == null)
            {
                return null;
            }

            return new UserDataResponse()
            {
                UserID = user.UserId,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                ProfileImageUrl = user.ProfileImageUrl,
                IsActive = user.IsActive,
            };
        }

        public async Task<List<UserDataResponse>> GetUsersAsync()
        {
            var user = await _unitOfWork.UserRepo.GetAllPublicUsers();

            List<UserDataResponse> users = new List<UserDataResponse>();

            foreach (var item in user)
            {
                UserDataResponse response = new UserDataResponse()
                {
                    UserID = item.UserId,
                    UserName = item.UserName,
                    DisplayName = item.DisplayName,
                    ProfileImageUrl = item.ProfileImageUrl,
                    IsActive = item.IsActive
                };

                users.Add(response);
            }

            return users;
        }
    }
}
