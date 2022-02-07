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
            TokenResponse token = new TokenResponse();
            SymmetricSecurityKey symmetricSecurityKey =
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]))
                ;
            SigningCredentials credentials =
                new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidAudience"],
                audience: _configuration["Token:ValidAudience"],
                expires: DateTime.Now.AddHours(6),
                notBefore: DateTime.Now,
                signingCredentials: credentials,
                claims: new Claim[]{new Claim("CustomerId", customerId.ToString())}
            );

            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            token.Token = handler.WriteToken(securityToken);
            return token;
        }
    }
}