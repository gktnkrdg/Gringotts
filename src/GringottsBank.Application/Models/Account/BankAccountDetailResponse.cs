using System;

namespace GringottsBank.Application.Models.Account
{
    public class BankAccountDetailResponse
    {
        public Guid AccountId { get; set; }
        public string AccountName { get; set; }
    }
    
}