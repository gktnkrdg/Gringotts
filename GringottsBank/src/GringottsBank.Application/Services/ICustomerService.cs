using System;
using System.Threading.Tasks;
using GringottsBank.Application.Models.Customer;

namespace GringottsBank.Application.Services
{
    public interface ICustomerService
    {
        public Task<CreateUserResponse> CreateCustomer(CreateCustomerCommand createCustomerCommand);
        public Task<GetUserResponse> GetCustomer(Guid customerId);
    }
}