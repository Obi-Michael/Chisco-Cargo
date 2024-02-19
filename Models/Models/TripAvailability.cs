namespace Models.Models
{
    public partial class TripAvailability
    {
        public Guid Id { get; set; }
        public string? EbmUsername { get; set; }
        public string? AssginedVehicle { get; set; }
        public Guid TripSettingId { get; set; }
        public Guid TripId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
