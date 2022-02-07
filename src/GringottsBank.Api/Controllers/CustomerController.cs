using System.Threading.Tasks;
using GringottsBank.Api.Extensions;
using GringottsBank.Application.Models.Customer;
using GringottsBank.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Api.Controllers
{
    [Route("api/v1.0/customers")]
    [ApiController]
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [ProducesResponseType(typeof(CreateUserResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser([FromBody] CreateCustomerCommand createUser)
        {
            var createUserResult = await _customerService.CreateCustomer(createUser);
            if (createUserResult.Success)
                return Created("", createUserResult.Data);
            return BadRequest(createUserResult.Message);
        }

        [Route("me")]
        [ProducesResponseType(typeof(CustomerResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetCustomerInformation()
        {
            var customerId = HttpContext.User.CustomerId();
            var userResult = await _customerService.GetCustomer(customerId);

            if (userResult == null)
            {
                return NotFound();
            }

            return Ok(userResult);
        }
    }
}