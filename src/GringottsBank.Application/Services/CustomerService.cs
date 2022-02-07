using System;
using System.Threading.Tasks;
using GringottsBank.Application.Models.Customer;

namespace GringottsBank.Application.Services
{
    public class CustomerService : ICustomerService
    {
        public async Task<CreateUserResponse> CreateCustomer(CreateCustomerCommand createCustomerCommand)
        {
            throw new NotImplementedException();
        }

        public async Task<GetUserResponse> GetCustomer(Guid customerId)
        {
            throw new NotImplementedException();
        }
    }
}