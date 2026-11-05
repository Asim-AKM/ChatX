using Application_Layer.DTO_s.Request;
using Azure.Core;
using Core_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application_Layer.Mappers
{
    public static class MapToUser
    {
        public static User Map(this RegisterUserRequest Request,string Name,string UserName)
        {
            return new User
            {
                UserId = Guid.NewGuid(),
                DisplayName = Name,
                UserName = UserName,
                Email = Request.Email,
                PasswordHash = Request.Password,
                CreatedAt = DateTime.Now,

            };
        }
    }
}
