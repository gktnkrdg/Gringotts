using System;
using GringottsBank.Application.Models.Token;

namespace GringottsBank.Api.Handlers
{
    public interface ITokenHandler
    {
        public TokenResponse CreateToken(Guid customerId);
    }
}