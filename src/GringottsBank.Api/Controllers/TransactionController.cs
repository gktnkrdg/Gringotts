using System;
using System.Threading.Tasks;
using GringottsBank.Api.Extensions;
using GringottsBank.Application.Models.Account;
using GringottsBank.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GringottsBank.Api.Controllers
{
    [Route("api/v1.0/transactions")]
    [ApiController]
    [Authorize]
    public class TransactionController : Controller
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [ProducesResponseType(typeof(CustomerBankAccountsResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<IActionResult> GetTransactions(DateTime startDate, DateTime? endDate)
        {
            var customerId = HttpContext.User.CustomerId();
            var customerTransactions =
                await _transactionService.GetCustomerTransactions(customerId, startDate, endDate);


            return Ok(customerTransactions);
        }
    }
}