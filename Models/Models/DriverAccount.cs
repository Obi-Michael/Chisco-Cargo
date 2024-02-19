namespace Models.Models
{
    public partial class DriverAccount
    {
        public Guid Id { get; set; }
        public string? DriverCode { get; set; }
        public string? Password { get; set; }
        public string? ConfirmationCode { get; set; }
        public string? DeviceToken { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
