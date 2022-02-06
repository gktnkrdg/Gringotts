using System;
using System.Threading.Tasks;
using GringottsBank.Application.Models.Transaction;

namespace GringottsBank.Application.Services
{
    public interface ITransactionService
    {
        public Task<CustomerTransactionsResponse> GetCustomerTransactions(Guid customerId, DateTime startDate, DateTime? endDate);
    }
}