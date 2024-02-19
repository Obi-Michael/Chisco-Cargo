namespace Models.Models
{
    public partial class VehicleAllocationDetail
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? DriverId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int VehicleId { get; set; }
        public string? VehicleName { get; set; }
        public int? DestinationTerminal { get; set; }
        public string? UserEmail { get; set; }
    }
}
