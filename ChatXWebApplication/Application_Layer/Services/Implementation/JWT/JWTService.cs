using Application_Layer.Services.Interfaces.IJWT;
using ChatX.Application.DTOs.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application_Layer.Services.Implementation.JWT
{
    public class JWTService : IJWTService
    {
        private readonly IConfiguration _configuration;
        public JWTService(IConfiguration configuration) => _configuration = configuration;

        public string CreateAccessToken(LoginUserRequest request)
        {
            var secret = _configuration["JWT:SecretKey"];
            if (string.IsNullOrEmpty(secret) || secret.Length < 32)
            {
                return ("Token not found");
            }

            var issuer = _configuration["JWT:Issuer"];
            var Audiouns = _configuration["JWT:Audiouns"];

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, request.Identifier),

            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var encryptedtoken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
                (
                issuer: issuer,
                audience: Audiouns,
                claims: claims,
                signingCredentials: encryptedtoken
                );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }


    }
}
