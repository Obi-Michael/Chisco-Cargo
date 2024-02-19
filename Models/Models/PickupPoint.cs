namespace Models.Models
{
    public partial class PickupPoint
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Name { get; set; }
        public string? PickupTime { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Image { get; set; }
        public Guid TripId { get; set; }
        public int? TerminalId { get; set; }
        public int? RouteId { get; set; }
    }
}
