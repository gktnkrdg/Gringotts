using System;
using System.Threading.Tasks;
using GringottsBank.Application.Models.Account;
using GringottsBank.Application.Models.Transaction;

namespace GringottsBank.Application.Services
{
    public interface IAccountService
    {
        public Task<GetCustomerAccountsResponse> GetCustomerAccounts(Guid customerId);
        public Task<GetCustomerAccountsResponse> GetCustomerAccount(Guid customerId,Guid accountId);
        public Task<CreateAccountResponse> CreateAccount(Guid customerId,string accountName);
        
        public Task<CreateAccountResponse> CreateTransaction(Guid customerId,Guid accountId,CreateTransactionCommand createTransaction);
        
        public Task<CreateAccountResponse> GetAccountTransactions(Guid customerId,Guid accountId);
        
    }
}