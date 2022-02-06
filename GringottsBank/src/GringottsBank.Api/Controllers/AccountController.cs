using System;
using System.Threading.Tasks;
using GringottsBank.Api.Extensions;
using GringottsBank.Application.Models.Account;
using GringottsBank.Application.Models.Transaction;
using GringottsBank.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Api.Controllers
{
    [Route("api/v1.0/accounts")]
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [ProducesResponseType(typeof(CreateAccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateAccount([FromRoute]string accountName)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _accountService.CreateAccount(customerId,accountName);

            if (accountResponse == null)
            {
                return BadRequest();
            }
            return Created("",accountResponse);
        }
        

        [Route("{accountId}")]
        [ProducesResponseType(typeof(GetCustomerAccountsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetAccountInformation([FromRoute] Guid accountId)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _accountService.GetCustomerAccount(customerId,accountId);

            if (accountResponse == null)
            {
                return NotFound();
            }
            return Ok(accountResponse);
        }
        
        [Route("{accountId}/transactions")]
        [ProducesResponseType(typeof(GetCustomerAccountsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromRoute] Guid accountId,[FromBody] CreateTransactionCommand createCommand)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _accountService.CreateTransaction(customerId,accountId,createCommand);

            if (accountResponse == null)
            {
                return NotFound();
            }
            return Ok(accountResponse);
        }
        
        [Route("{accountId}/transactions")]
        [ProducesResponseType(typeof(GetCustomerAccountsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetTransactions([FromRoute] Guid accountId)
        {
            var customerId = HttpContext.User.CustomerId();
            var transactionResponse = await _accountService.GetAccountTransactions(customerId,accountId);

            if (transactionResponse == null)
            {
                return NotFound();
            }
            return Ok(transactionResponse);
        }
        
        
     
    
    }
}