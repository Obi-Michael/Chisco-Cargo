namespace Models.Models
{
    public partial class VehicleMileage
    {
        public string VehicleRegistrationNumber { get; set; } = null!;
        public int ServiceLevel { get; set; }
        public Guid Id { get; set; }
        public int CurrentMileage { get; set; }
        public DateTime? LastServiceDate { get; set; }
        public DateTime? DateDue { get; set; }
        public bool IsDue { get; set; }
        public bool IsDeactivated { get; set; }
        public int NotificationLevel { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
