using System;
using System.Linq;
using System.Threading.Tasks;
using GringottsBank.Application.Models;
using GringottsBank.Application.Models.Customer;
using GringottsBank.Application.Services.Contracts;
using GringottsBank.Core.Entity;
using GringottsBank.Data;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Application.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly GringottsBankDbContext _dbContext;

        public CustomerService(GringottsBankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<ResultModel<CreateUserResponse>> CreateCustomer(CreateCustomerCommand createCustomerCommand)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.Email == createCustomerCommand.Email);
            if (customer != null)
                return new ResultModel<CreateUserResponse>()
                {
                    Data = null,
                    Message = "Daha önce kayıtlı email adresiyle tekrar kayıt olamazsınız.",
                    Success = false
                };
            customer = new Customer()
            {
                Email = createCustomerCommand.Email,
                Password = createCustomerCommand.Password, //todo hash,
                CreatedDate = DateTime.Now,
                FirstName = createCustomerCommand.FirstName,
                LastName = createCustomerCommand.LastName,
                PhoneNumber = createCustomerCommand.PhoneNumber
            };
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return new ResultModel<CreateUserResponse>()
            {
                Data = new CreateUserResponse() { CustomerId = customer.Id },
                Success = true
            };
        }

        public async Task<CustomerResponse> GetCustomer(Guid customerId)
        {
            return await _dbContext.Customers.Where(c => c.Id == customerId).Select(c => new CustomerResponse()
            {
                Email = c.Email,
                CustomerId = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName
            }).FirstOrDefaultAsync();
        }


        public async Task<ResultModel<CheckCustomerLoginResponse>> CheckCustomerLogin(string email,string password)
        {
            var customerLoginResult = await _dbContext.Customers.Where(c => c.Email == email && c.Password == password)
                .FirstOrDefaultAsync();
            if (customerLoginResult == null)
            {
                return new ResultModel<CheckCustomerLoginResponse>
                {
                    Data = null,
                    Success = false,
                    Message = "Geçersiz login bilgisi",
                };
            }
            return new ResultModel<CheckCustomerLoginResponse>
            {
                Data =  new CheckCustomerLoginResponse(){ CustomerId = customerLoginResult.Id},
                Success = true
            };
        }
    }
}