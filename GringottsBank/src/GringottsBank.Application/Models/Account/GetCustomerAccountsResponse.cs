using System;
using System.Collections.Generic;

namespace GringottsBank.Application.Models.Account
{
    public class GetCustomerAccountsResponse
    {
         public Guid CustomerId { get; set; }
         public List<AccountResponse> Accounts { get; set; }
    }

    public class AccountResponse
    {
        public Guid AccountId { get; set; }
    }
}