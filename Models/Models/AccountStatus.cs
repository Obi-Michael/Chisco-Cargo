using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AccountStatus
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int CompanyId { get; set; }
        public string? AccountStatus1 { get; set; }
        public string? AccountStatusDescription { get; set; }
        public int? UserType { get; set; }
    }
}
