using System;
using System.Threading.Tasks;
using GringottsBank.Api.Handlers;
using GringottsBank.Application.Models.Token;
using GringottsBank.Application.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Api.Controllers
{
    [Route("api/v1.0/authentication")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly ITokenHandler _tokenHandler;

        public AuthenticationController(ICustomerService customerService, ITokenHandler tokenHandler)
        {
            _customerService = customerService;
            _tokenHandler = tokenHandler;
        }

        [Route("login")]
        [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand request)
        {
            var customerLoginResult = await _customerService.CheckCustomerLogin(request.Email, request.Password);
            if(!customerLoginResult.Success  || customerLoginResult.Data == null || customerLoginResult.Data.CustomerId == Guid.Empty )
                return BadRequest(customerLoginResult.Message);
            
            var tokenResult = _tokenHandler.CreateToken(customerLoginResult.Data.CustomerId);
            if (tokenResult?.Token == null)
                return BadRequest();
            
            return Ok(tokenResult.Token);
        }
    }
}