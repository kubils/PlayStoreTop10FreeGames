using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Application
{
    public interface IGetJwtToken
    {
        Task<string> GenerateToken(User user);
    }
    
    public class GetJwtToken : IGetJwtToken
    {
        private readonly IConfiguration _configuration;
        public GetJwtToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  Task<string> GenerateToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["JwtConfig:JwtKey"]);
            
            List<Claim> claims = new()
            {
                new Claim("Id", user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };

            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
                SecurityAlgorithms.HmacSha256);

            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(issuer: _configuration["JwtConfig:Issuer"],
                audience: _configuration["JwtConfig:Issuer"], claims: claims, signingCredentials: signingCredentials,
                expires: DateTime.Now.AddHours(2));

            return Task.FromResult(tokenHandler.WriteToken(jwtSecurityToken));
        }
    }
}