using GringottsBank.Core.Enums;

namespace GringottsBank.Application.Models.Transaction
{
    public class CreateTransactionCommand
    {
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}