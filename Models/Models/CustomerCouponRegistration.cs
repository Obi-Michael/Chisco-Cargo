namespace Models.Models
{
    public partial class CustomerCouponRegistration
    {
        public long Id { get; set; }
        public string? CouponCode { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
