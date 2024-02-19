namespace Models.Models
{
    public partial class Wallet
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? WalletNumber { get; set; }
        public decimal Balance { get; set; }
        public string? UserType { get; set; }
        public string? UserId { get; set; }
        public bool IsReset { get; set; }
        public DateTime? LastResetDate { get; set; }
        public decimal OldBalance { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? WalletLastUpdated { get; set; }
        public int? TenantId { get; set; }
    }
}
