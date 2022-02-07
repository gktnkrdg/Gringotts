using System;

namespace GringottsBank.Core.Entity
{
    public class AuditLog : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string TableName { get; set; }
        public string FieldName { get; set; }
        public string SourceMethod { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}