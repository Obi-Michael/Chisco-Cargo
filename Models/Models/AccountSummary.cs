using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AccountSummary
    {
        public Guid Id { get; set; }
        public string? AccountName { get; set; }
        public double Balance { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
