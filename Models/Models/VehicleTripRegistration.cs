namespace Models.Models
{
    public partial class VehicleTripRegistration
    {
        public Guid Id { get; set; }
        public string? PhysicalBusRegistrationNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public bool IsVirtualBus { get; set; }
        public bool IsBusFull { get; set; }
        public bool IsBlownBus { get; set; }
        public bool ManifestPrinted { get; set; }
        public string? DriverCode { get; set; }
        public string? OriginalDriverCode { get; set; }
        public int? BookingTypeId { get; set; }
        public int JourneyType { get; set; }
        public Guid TripId { get; set; }
        public int? VehicleModelId { get; set; }
        public int? BookingId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string? LoadingInformation { get; set; }
    }
}
