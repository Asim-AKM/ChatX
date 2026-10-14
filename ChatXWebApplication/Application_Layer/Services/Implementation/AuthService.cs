using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using Application_Layer.Services.Interfaces;
using ChatX.Application.DTOs.Request;

namespace Application_Layer.Services.Implementation
{
    public class AuthService : IAuthService
    {
       
        public Task<AuthenticationResponse> LoginUserAsync(LoginUserRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthenticationResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
