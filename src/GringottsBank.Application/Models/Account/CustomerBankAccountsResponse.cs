using System;
using System.Collections.Generic;

namespace GringottsBank.Application.Models.Account
{
    public class CustomerBankAccountsResponse
    {
        public Guid CustomerId { get; set; }
        public List<BankAccountDetailResponse> Accounts { get; set; }
    }
}