using System;
using System.Threading.Tasks;
using GringottsBank.Application.Models;
using GringottsBank.Application.Models.Customer;

namespace GringottsBank.Application.Services.Contracts
{
    public interface ICustomerService
    {
        public Task<ResultModel<CreateUserResponse>> CreateCustomer(CreateCustomerCommand createCustomerCommand);
        public Task<CustomerResponse> GetCustomer(Guid customerId);
        public Task<ResultModel<CheckCustomerLoginResponse>> CheckCustomerLogin(string email, string password);
    }
}