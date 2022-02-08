using System;
using System.Linq;
using System.Threading.Tasks;
using GringottsBank.Application.Models.Transaction;
using GringottsBank.Application.Services.Contracts;
using GringottsBank.Data;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Services.Implementations
{
    public class TransactionService : ITransactionService
    {
        private readonly GringottsBankDbContext _dbContext;

        public TransactionService(GringottsBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerTransactionsResponse> GetCustomerTransactions(Guid customerId, DateTime startDate,
            DateTime? endDate)
        {
            var customerTransactions = _dbContext.Transactions.Include(c => c.BankAccount).ThenInclude(c => c.Customer)
                .Where(c => c.BankAccount.Customer.Id == customerId && c.CreatedDate > startDate);
            if (endDate.HasValue) customerTransactions = customerTransactions.Where(c => c.CreatedDate < endDate);

            return new CustomerTransactionsResponse
            {
                CustomerId = customerId,
                Transactions = customerTransactions.Select(c => new CustomerTransaction
                {
                    Amount = c.Amount,
                    AccountId = c.BankAccountId,
                    AccountName = c.BankAccount.Name,
                    CreateDate = c.CreatedDate,
                    TransactionType = c.TransactionType.ToString()
                }).ToList()
            };
        }

        public async Task<BankAccountTransactionsResponse> GetAccountTransactions(Guid customerId, Guid bankAccountId)
        {
            var transactions = _dbContext.Transactions.Include(c => c.BankAccount).Where(c =>
                c.BankAccount.CustomerId == customerId && c.BankAccountId == bankAccountId).ToList();
            return new BankAccountTransactionsResponse
            {
                CustomerId = customerId,
                AccountId = bankAccountId,
                Transactions = transactions.Select(c => new BankAccountTransaction
                {
                    Amount = c.Amount,
                    CreateDate = c.CreatedDate,
                    TransactionType = c.TransactionType.ToString()
                }).ToList()
            };
        }
    }
}