using System.Threading.Tasks;
using GringottsBank.Application.Models.Token;

namespace GringottsBank.Application.Services
{
    public interface ITokenService
    {
        public Task<TokenResponse> CreateToken(CreateTokenCommand createToken);
    }
}