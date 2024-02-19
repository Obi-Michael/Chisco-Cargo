namespace Models.Models
{
    public partial class CustomerBio
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
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? Title { get; set; }
        public string? DeviceToken { get; set; }
        public string? Referrer { get; set; }
        public string? ReferralCode { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhone { get; set; }
        public int LoginDeviceType { get; set; }
        public int? WalletId { get; set; }
        public int Gender { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Photo { get; set; }
        public int? CompanyId { get; set; }
        public int? LocationId { get; set; }
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public decimal? WalletBalance { get; set; }
    }
}
