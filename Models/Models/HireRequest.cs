namespace Models.Models
{
    public partial class HireRequest
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public string? Gender { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhoneNumber { get; set; }
        public int NumberOfBuses { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime DepartureDate { get; set; }
        public string? Departure { get; set; }
        public string? Destination { get; set; }
        public string? AdditionalRequest { get; set; }
        public int? VehicleId { get; set; }
    }
}
