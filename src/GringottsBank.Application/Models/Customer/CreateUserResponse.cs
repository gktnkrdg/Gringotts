using System;

namespace GringottsBank.Application.Models.Customer
{
    public class CreateUserResponse
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
   
}