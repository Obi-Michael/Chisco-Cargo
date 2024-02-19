namespace Models.Models
{
    public partial class Vehicle
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? RegistrationNumber { get; set; }
        public string? ChasisNumber { get; set; }
        public string? EngineNumber { get; set; }
        public string? Imeinumber { get; set; }
        public string? Type { get; set; }
        public int VehicleModelId { get; set; }
        public int? LocationId { get; set; }
        public bool IsOperational { get; set; }
        public int Status { get; set; }
        public int? DriverId { get; set; }
        public int? FranchiseId { get; set; }
        public int? FranchizeId { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? InsuranceDate { get; set; }
        public DateTime? LicenseDate { get; set; }
        public string? Description { get; set; }
        public DateTime? InsuranceExpiration { get; set; }
        public DateTime? LicenseExpiration { get; set; }
        public DateTime? RoadWorthinessDate { get; set; }
        public DateTime? RoadWorthinessExpiration { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
    }
}
