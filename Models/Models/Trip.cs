namespace Models.Models
{
    public partial class Trip
    {
        public Guid Id { get; set; }
        public string? DepartureTime { get; set; }
        public string? TripCode { get; set; }
        public bool AvailableOnline { get; set; }
        public string? ParentRouteDepartureTime { get; set; }
        public int RouteId { get; set; }
        public int? VehicleModelId { get; set; }
        public int? ParentRouteId { get; set; }
        public Guid? ParentTripId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
    }
}
