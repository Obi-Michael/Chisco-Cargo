namespace Models.Models
{
    public partial class Manifest
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int NumberOfSeats { get; set; }
        public bool IsPrinted { get; set; }
        public decimal? Amount { get; set; }
        public decimal? Dispatch { get; set; }
        public DateTime? ManifestPrintedTime { get; set; }
        public Guid VehicleTripRegistrationId { get; set; }
        public int? VehicleModelId { get; set; }
        public string? Employee { get; set; }
        public string? DispatchSource { get; set; }
        public decimal? Commision { get; set; }
        public decimal? DriverFee { get; set; }
        public decimal? Mtu { get; set; }
        public decimal? Transload { get; set; }
        public decimal? Vat { get; set; }
        public decimal BorderExpense { get; set; }
        public decimal ConductorAllowance { get; set; }
        public decimal DriverAllowance { get; set; }
        public decimal Feeding { get; set; }
        public decimal GasAllowance { get; set; }
        public decimal LoaderCommission { get; set; }
        public decimal Maintenance { get; set; }
        public decimal PettyCash { get; set; }
        public decimal SpareDriverAllowance { get; set; }
        public decimal Transit { get; set; }
        public decimal Union { get; set; }
        public int? JourneyManagementId { get; set; }
        public bool? IsReceived { get; set; }
        public DateTime DateCreated { get; set; }
        public int ManifestId { get; set; }
        public string ManifestNumber { get; set; }
    }
}
