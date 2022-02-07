using System;
using GringottsBank.Core.Enums;

namespace GringottsBank.Core.Entity
{
    public class Transaction : BaseEntity
    {
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid AccountId { get; set; }
        public Account Account { get; set; }
      
    }
}