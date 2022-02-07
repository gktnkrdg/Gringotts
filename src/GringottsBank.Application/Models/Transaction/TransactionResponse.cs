using System;
using GringottsBank.Core.Enums;

namespace GringottsBank.Application.Models.Transaction
{
    public class TransactionResponse
    {
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public string AccountName { get; set; }
        public Guid AccountId { get; set; }
    }
}