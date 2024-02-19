namespace Models.Models
{
    public partial class Referral
    {
        public long Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public int UserType { get; set; }
        public string? ReferralCode { get; set; }
    }
}
