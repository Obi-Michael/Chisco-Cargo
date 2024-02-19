using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AccountTransaction
    {
        public Guid Id { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Narration { get; set; }
        public int AccountType { get; set; }
        public int TransactionType { get; set; }
        public Guid TransactionSourceId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
