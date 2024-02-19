namespace Models.Models
{
    public partial class MtuReportModel
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? VehicleId { get; set; }
        public string? DriverId { get; set; }
        public int Status { get; set; }
        public string? Email { get; set; }
        public string? FullName { get; set; }
        public string? Notes { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? Picture { get; set; }
    }
}
