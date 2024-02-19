namespace Models.Models
{
    public partial class TripSetting
    {
        public Guid Id { get; set; }
        public Guid TripSettingId { get; set; }
        public int RouteId { get; set; }
        public int WeekDays { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
