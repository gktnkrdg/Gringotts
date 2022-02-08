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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateBankAccount([FromBody] CreateBankAccountCommand bankAccountCommand)
        {
            var customerId = HttpContext.User.CustomerId();
            var accountResponse = await _bankAccountService.CreateAccount(customerId, bankAccountCommand.Name);

            if (!accountResponse.Success) 
                return BadRequest(accountResponse.Message);

            return Created("", accountResponse.Data);
        }

        [ProducesResponseType(typeof(CustomerBankAccountsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetCustomerBankAccounts()
        {
            var customerId = HttpContext.User.CustomerId();

            var accountResponse = await _bankAccountService.GetCustomerAccounts(customerId);

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
            var accountResponse = await _bankAccountService.GetCustomerAccount(customerId, bankAccountId);

            if (accountResponse == null) 
                return NotFound();

            return Ok(accountResponse);
        }

        [Route("{bankAccountId}/deposit")]
        [ProducesResponseType(typeof(CreateTransactionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateDeposit([FromRoute] Guid bankAccountId,
            [FromBody] DepositCommand command)
        {
            var customerId = HttpContext.User.CustomerId();
            var createDeposit = await _bankAccountService.CreateDeposit(customerId, bankAccountId, command);

            if (!createDeposit.Success) 
                return BadRequest(createDeposit.Message);

            return Ok(createDeposit.Data);
        }

        [Route("{bankAccountId}/withdraw")]
        [ProducesResponseType(typeof(CreateTransactionResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpPost]
        public async Task<IActionResult> CreateWithdrawTransaction([FromRoute] Guid bankAccountId,
            [FromBody] WithdrawCommand command)
        {
            var customerId = HttpContext.User.CustomerId();
            var createWithdrawResult = await _bankAccountService.CreateWithdraw(customerId, bankAccountId, command);

            if (!createWithdrawResult.Success) 
                return BadRequest(createWithdrawResult.Message);

            return Ok(createWithdrawResult.Data);
        }


        [Route("{bankAccountId}/transactions")]
        [ProducesResponseType(typeof(BankAccountTransactionsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetBankAccountTransactions([FromRoute] Guid bankAccountId)
        {
            var customerId = HttpContext.User.CustomerId();

            var account = await _bankAccountService.GetCustomerAccount(customerId, bankAccountId);
            if (account == null)
                return NotFound();

            var transactionResponse = await _transactionService.GetAccountTransactions(customerId, bankAccountId);

            return Ok(transactionResponse);
        }
    }
}