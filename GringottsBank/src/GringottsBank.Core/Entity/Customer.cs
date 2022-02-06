using System.Collections.Generic;

namespace GringottsBank.Core.Entity
{
    public class Customer: BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public IList<Account> Accounts { get; set; }
    }
}