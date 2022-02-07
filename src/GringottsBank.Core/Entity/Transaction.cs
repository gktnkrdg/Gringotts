using System;
using GringottsBank.Core.Enums;

namespace GringottsBank.Core.Entity
{
    public class Transaction : BaseEntity
    {
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public Guid BankAccountId { get; set; }
        public BankAccount BankAccount { get; set; }
    }
}