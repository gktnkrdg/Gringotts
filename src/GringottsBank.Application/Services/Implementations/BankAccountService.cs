using System;
using System.Linq;
using System.Threading.Tasks;
using GringottsBank.Application.Models;
using GringottsBank.Application.Models.Account;
using GringottsBank.Application.Models.Transaction;
using GringottsBank.Application.Services.Contracts;
using GringottsBank.Core.Entity;
using GringottsBank.Core.Enums;
using GringottsBank.Data;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Services.Implementations
{
    public class BankAccountService : IBankAccountService
    {
        private readonly GringottsBankDbContext _dbContext;

        public BankAccountService(GringottsBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<CustomerBankAccountsResponse> GetCustomerAccounts(Guid customerId)
        {
            var customerBankAccounts = _dbContext.BankAccounts.Where(c => c.CustomerId == customerId).ToList();
            if (!customerBankAccounts.Any())
                return null;

            return new CustomerBankAccountsResponse()
            {
                CustomerId = customerId,
                Accounts = customerBankAccounts.Select(c => new BankAccountDetailResponse()
                {
                    AccountId = c.Id,
                    AccountName = c.Name
                }).ToList()
            };
        }

        public async Task<BankAccountDetailResponse> GetCustomerAccount(Guid customerId, Guid accountId)
        {
            return await _dbContext.BankAccounts.Where(c => c.CustomerId == customerId).Select(c =>
                new BankAccountDetailResponse()
                {
                    AccountId = c.Id,
                    AccountName = c.Name
                }).FirstOrDefaultAsync();
        }

        public async Task<ResultModel<CreateBankAccountResponse>> CreateAccount(Guid customerId, string accountName)
        {
            var bankAccount = _dbContext.BankAccounts
                .FirstOrDefault(c => c.CustomerId == customerId && c.Name == accountName);
            if (bankAccount != null)
            {
                return new ResultModel<CreateBankAccountResponse>()
                {
                    Message = "Aynı isimli hesabınız bulunmaktadır.",
                    Success = false
                };
            }

            Random rand = new Random();
            bankAccount = new BankAccount()
            {
                CustomerId = customerId,
                Name = accountName,
                Balance = 0,
                CreatedDate = DateTime.Now
            };
            _dbContext.BankAccounts.Add(bankAccount);
            await _dbContext.SaveChangesAsync();
            return new ResultModel<CreateBankAccountResponse>()
            {
                Data = new CreateBankAccountResponse() { BankAccountId = bankAccount.Id },
                Success = true
            };
        }

        public async Task<ResultModel<CreateTransactionResponse>> CreateWithdraw(Guid customerId, Guid accountId,
            WithdrawCommand withdrawCommand)
        {
            var account = _dbContext.BankAccounts.Where(c => c.Id == accountId).FirstOrDefault();
            if (account == null)
            {
                return new ResultModel<CreateTransactionResponse>()
                {
                    Message = "Bank account cannot exist",
                    Success = false,
                };
            }

            if (account.Balance < withdrawCommand.Amount)
            {
                return new ResultModel<CreateTransactionResponse>()
                {
                    Message = "Your balance is lower than you want to withdraw",
                    Success = false,
                };
            }
            var transaction = new Transaction()
            {
                Amount = withdrawCommand.Amount,
                BankAccountId = accountId,
                CreatedDate = DateTime.Now,
                TransactionType = TransactionType.Withdraw,
            };
            using var databaseTransaction = await _dbContext.Database.BeginTransactionAsync();
            {
                try
                {
                    account.Balance = account.Balance - withdrawCommand.Amount;
                    _dbContext.BankAccounts.Update(account);
                    
                    _dbContext.Transactions.Add(transaction);
                    await _dbContext.SaveChangesAsync();
                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    return new ResultModel<CreateTransactionResponse>()
                    {
                        Message = "An unidentified error occurred please try again later",
                        Success = false
                    };
                }
                return new ResultModel<CreateTransactionResponse>()
                {
                    Data = new CreateTransactionResponse(){TransactionId = transaction.Id},
                    Success = true
                };
            }
        }

        public async Task<ResultModel<CreateTransactionResponse>> CreateDeposit(Guid customerId, Guid accountId,
            DepositCommand depositCommand)
        {
            var account = _dbContext.BankAccounts.FirstOrDefault(c => c.Id == accountId);
            if (account == null)
            {
                return new ResultModel<CreateTransactionResponse>()
                {
                    Message = "Bank account cannot exist",
                    Success = false,
                };
            }
            var transaction = new Transaction()
            {
                Amount = depositCommand.Amount,
                BankAccountId = accountId,
                CreatedDate = DateTime.Now,
                TransactionType = TransactionType.Withdraw,
            };
            using var databaseTransaction = await _dbContext.Database.BeginTransactionAsync();
            {
                try
                {
                    account.Balance = account.Balance + depositCommand.Amount;
                    _dbContext.BankAccounts.Update(account);
                    
                    _dbContext.Transactions.Add(transaction);
                    await _dbContext.SaveChangesAsync();
                    await databaseTransaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    await databaseTransaction.RollbackAsync();
                    return new ResultModel<CreateTransactionResponse>()
                    {
                        Message = "An unidentified error occurred please try again later",
                        Success = false
                    };
                }
                return new ResultModel<CreateTransactionResponse>()
                {
                    Data = new CreateTransactionResponse(){TransactionId = transaction.Id},
                    Success = true
                };
            }
        }
    }
}