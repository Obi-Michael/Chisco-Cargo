namespace Models.Models
{
    public partial class HireBu
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? DepartureAddress { get; set; }
        public string? DestinationAddress { get; set; }
        public decimal Amount { get; set; }
        public int VehicleId { get; set; }
        public string? DriverCode { get; set; }
        public DateTime DepartureDate { get; set; }
        public int UserLocationId { get; set; }
        public Guid VehicleTripRegistrationId { get; set; }
        public int DepartureTerminalId { get; set; }
        public DateTime? ArrivalDate { get; set; }
        public bool? IsManifestPrinted { get; set; }
        public int? ReceivedLocationId { get; set; }
    }
}
