using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AgentApplicant
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? NextOfKin { get; set; }
        public string? Username { get; set; }
        public string? NextOfKinPhone { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public int AgentStatus { get; set; }
        public DateTime? TransactionDate { get; set; }
    }
}
