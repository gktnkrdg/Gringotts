using System;
using System.Collections.Generic;

namespace GringottsBank.Core.Entity
{
    public class BankAccount : BaseEntity
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        
        public bool IsActive { get; set; }
        public uint ConcurrencyStamp { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}