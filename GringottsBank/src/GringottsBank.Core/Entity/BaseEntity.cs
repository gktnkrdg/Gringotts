using System;
using System.ComponentModel.DataAnnotations;

namespace GringottsBank.Core.Entity
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}