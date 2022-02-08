using System;
using System.Threading.Tasks;
using GringottsBank.Application.Models.Transaction;

namespace GringottsBank.Application.Services.Contracts
{
    public interface ITransactionService
    {
        public Task<CustomerTransactionsResponse> GetCustomerTransactions(Guid customerId, DateTime startDate,
            DateTime? endDate);

        public Task<BankAccountTransactionsResponse> GetAccountTransactions(Guid customerId, Guid bankAccountId);
    }
}