using System;
using System.Threading.Tasks;
using GringottsBank.Application.Models;
using GringottsBank.Application.Models.Account;
using GringottsBank.Application.Models.Transaction;

namespace GringottsBank.Application.Services.Contracts
{
    public interface IBankAccountService
    {
        public Task<CustomerBankAccountsResponse> GetCustomerAccounts(Guid customerId);
        public Task<BankAccountDetailResponse> GetCustomerAccount(Guid customerId,Guid accountId);
        public Task<ResultModel<CreateBankAccountResponse>> CreateAccount(Guid customerId,string accountName);

        public Task<ResultModel<CreateTransactionResponse>> CreateDeposit(Guid customerId,Guid accountId,DepositCommand depositCommand);
        public Task<ResultModel<CreateTransactionResponse>> CreateWithdraw(Guid customerId,Guid accountId,WithdrawCommand deposit);
    }
}