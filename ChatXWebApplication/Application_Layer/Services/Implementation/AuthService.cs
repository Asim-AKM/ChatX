using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;
using Application_Layer.Services.Interfaces;
using ChatX.Application.DTOs.Request;
using Core_Layer.InterfaceSecurity;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;

namespace Application_Layer.Services.Implementation
{
    public class AuthService : IAuthService
    {
        private readonly IPasswordService _PasswordService;
        private readonly AppDbContext _context;
        public AuthService(IPasswordService passwordService, AppDbContext appDbContext)
        {
            _context = appDbContext;
            _PasswordService = passwordService;
        }
        public async Task<AuthenticationResponse> LoginUserAsync(LoginUserRequest request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == request.Identifier || x.Email == request.Identifier);


            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid username/email or password.");
            }
           
            var isPasswordValid = _PasswordService.VerifyPassword(request.Password, user.PasswordHash);

            if (!isPasswordValid)
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


        public Task<AuthenticationResponse> RegisterUserAsync(RegisterUserRequest request)
        {
            throw new NotImplementedException();
        }
    }
}