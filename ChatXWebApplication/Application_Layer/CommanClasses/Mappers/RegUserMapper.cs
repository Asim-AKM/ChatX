using Application_Layer.DTO_s.Request;
using Core_Layer.Entities;

namespace Application_Layer.CommanClasses.Mappers
{
    public static class RegUserMapper
    {
        public static User MapToUserDomain(this RegisterUserRequest request)
        {
            if(request == null) throw new ArgumentNullException(nameof(request));
            else
            {
                return new User
                {
                    UserId=Guid.NewGuid(),
                    UserName=request.UserName,
                    Email=request.Email,
                    CreatedAt=DateTime.Now,
                    PasswordHash=request.Password,
                    ProfileImageUrl=request.ProfileImageUrl,
                };
            }
        }
    }
}
