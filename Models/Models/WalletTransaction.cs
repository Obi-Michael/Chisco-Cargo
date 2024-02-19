namespace Models.Models
{
    public partial class WalletTransaction
    {
        public Guid Id { get; set; }
        public string TransactionType { get; set; }
        public Guid TransactionSourceId { get; set; }
        public string? UserId { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public decimal LineBalance { get; set; }
        public int WalletId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string? TransDescription { get; set; }
        public int PayTypeDiscription { get; set; }
        public string? TransactedBy { get; set; }
        public bool IsSum { get; set; }
        public bool IsApproved { get; set; }
        public string? IsApprovedBy { get; set; }
        public DateTime? IsApprovedDate { get; set; }
        public bool IsCaptured { get; set; }
        public bool IsVerified { get; set; }
        public string? IsVerifiedBy { get; set; }
        public DateTime? IsVerifiedDate { get; set; }
        public int AmountType { get; set; }
        public int TenantId { get; set; }
        public string? TransCode { get; set; }
        public int PayMethod { get; set; }
        public decimal? CreditAmount { get; set; }
        public decimal? DebitAmount { get; set; }
        public string? AgentAccountName { get; set; }
        public string? BookingReferenceCode { get; set; }
    }
}
