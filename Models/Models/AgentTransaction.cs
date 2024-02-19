using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class AgentTransaction
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int AgentId { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionTypes { get; set; }
        public string? BookingReferenceCode { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public DateTime CancelledDate { get; set; }
        public DateTime IssuedDate { get; set; }
        public DateTime RequestDate { get; set; }
        public string? VerifiedBy { get; set; }
        public DateTime VerifiedDate { get; set; }
        public bool IsApproved { get; set; }
        public bool IsVerified { get; set; }
    }
}
