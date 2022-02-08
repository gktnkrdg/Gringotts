using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using GringottsBank.Application.Models.Token;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace GringottsBank.Api.Handlers
{
    public class TokenHandler : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandler(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public TokenResponse CreateToken(Guid customerId)
        {
            var token = new TokenResponse();
            var symmetricSecurityKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));

            var credentials =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            var securityToken = new JwtSecurityToken(
                _configuration["JWT:ValidAudience"],
                _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(6),
                signingCredentials: credentials,
                claims: new[] { new Claim("CustomerId", customerId.ToString()) }
            );

            var handler = new JwtSecurityTokenHandler();
            token.Token = handler.WriteToken(securityToken);
            return token;
        }
    }
}