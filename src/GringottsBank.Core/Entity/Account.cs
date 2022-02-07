using System;
using System.Collections.Generic;

namespace GringottsBank.Core.Entity
{
    public class Account : BaseEntity
    {
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public Guid CustomerID { get; set; }
        public Customer Customer { get; set; }
        public IList<Transaction> Transactions { get; set; }
    }
}