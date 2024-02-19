namespace Models.Models
{
    public partial class Driver
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Code { get; set; }
        public string? HandoverCode { get; set; }
        public string? VehicleRegistrationNumber { get; set; }
        public int DriverStatus { get; set; }
        public int DriverType { get; set; }
        public string? Name { get; set; }
        public string? Phone1 { get; set; }
        public string? Phone2 { get; set; }
        public string? Designation { get; set; }
        public DateTime AssignedDate { get; set; }
        public string? ResidentialAddress { get; set; }
        public string? NextOfKin { get; set; }
        public DateTime? DateOfEmployment { get; set; }
        public string? Picture { get; set; }
        public bool Active { get; set; }
        public string? NextOfKinNumber { get; set; }
        public string? BankName { get; set; }
        public string? BankAccount { get; set; }
        public string? DeactivationReason { get; set; }
        public string? ActivationStatusChangedByEmail { get; set; }
        public int WalletId { get; set; }
        public int? MaintenanceWalletId { get; set; }
        public int NoOfTrips { get; set; }
        public int EnrollmentStatus { get; set; }
        public string? ConfirmationCode { get; set; }
        public DateTime? LicenseDate { get; set; }
        public int? DuePeriod { get; set; }
        public int? UserId { get; set; }
        public string? Email { get; set; }
        public string? Alias { get; set; }
        public DateTime? ExpiringDate { get; set; }
    }
}
