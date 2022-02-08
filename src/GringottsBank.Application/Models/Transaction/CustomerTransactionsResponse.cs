using System;
using System.Collections.Generic;
using GringottsBank.Core.Enums;

namespace GringottsBank.Application.Models.Transaction
{
    public class CustomerTransactionsResponse
    {
        public Guid CustomerId { get; set; }
        public List<CustomerTransaction> Transactions { get; set; }
    }

    public class CustomerTransaction
    {
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string AccountName { get; set; }
        public Guid AccountId { get; set; }
    }
}