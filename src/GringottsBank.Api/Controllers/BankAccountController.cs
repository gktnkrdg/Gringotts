using System;
using System.Threading.Tasks;
using GringottsBank.Api.Extensions;
using GringottsBank.Application.Models.Account;
using GringottsBank.Application.Models.Transaction;
using GringottsBank.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Api.Controllers
{
    [Route("api/v1.0/bank-accounts")]
    [ApiController]
    [Authorize]
    public class BankAccountController : Controller
    {
        private readonly IBankAccountService _bankAccountService;
        private readonly ITransactionService _transactionService;

        public BankAccountController(IBankAccountService bankAccountService, ITransactionService transactionService)
        {
            _bankAccountService = bankAccountService;
            _transactionService = transactionService;
        }

        [ProducesResponseType(typeof(CreateBankAccountResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromRoute]string accountName)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _bankAccountService.CreateAccount(customerId,accountName);

            if (accountResponse == null)
            {
                return BadRequest();
            }
            return Created("",accountResponse);
        }

        [ProducesResponseType(typeof(CustomerBankAccountsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetCustomerBankAccounts()
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _bankAccountService.GetCustomerAccounts(customerId);

            if (accountResponse == null)
            {
                return NotFound();
            }
            return Ok(accountResponse);
        }

        [Route("{bankAccountId}")]
        [ProducesResponseType(typeof(BankAccountDetailResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetCustomerBankAccountInformation([FromRoute] Guid bankAccountId)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _bankAccountService.GetCustomerAccount(customerId,bankAccountId);

            if (accountResponse == null)
            {
                return NotFound();
            }
            return Ok(accountResponse);
        }

        [Route("{bankAccountId}/deposit")]
        [ProducesResponseType(typeof(CustomerBankAccountsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateDeposit([FromRoute] Guid bankAccountId,[FromBody] DepositCommand command)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _bankAccountService.CreateDeposit(customerId,bankAccountId,command);

            if (accountResponse == null)
            {
                return NotFound();
            }
            return Ok(accountResponse);
        }

        [Route("{bankAccountId}/withdraw")]
        [ProducesResponseType(typeof(CustomerBankAccountsResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromRoute] Guid bankAccountId,[FromBody] WithdrawCommand command)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _bankAccountService.CreateWithdraw(customerId,bankAccountId,command);

            if (accountResponse == null)
            {
                return NotFound();
            }
            return Ok(accountResponse);
        }


        [Route("{bankAccountId}/transactions")]
        [ProducesResponseType(typeof(CustomerBankAccountsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetBankAccountTransactions([FromRoute] Guid bankAccountId)
        {
            var customerId = HttpContext.User.CustomerId();
            var transactionResponse = await  _transactionService.GetAccountTransactions(customerId,bankAccountId);

            if (transactionResponse == null)
            {
                return NotFound();
            }
            return Ok(transactionResponse);
        }
    }
}