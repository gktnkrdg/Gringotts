using System;
using System.Collections.Generic;
using GringottsBank.Core.Enums;

namespace GringottsBank.Application.Models.Transaction
{
    public class BankAccountTransactionsResponse
    {
        public Guid CustomerId { get; set; }
        public Guid AccountId { get; set; }
        public List<BankAccountTransaction> Transactions { get; set; }
    }

    public class BankAccountTransaction
    {
        public string TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
    }
}