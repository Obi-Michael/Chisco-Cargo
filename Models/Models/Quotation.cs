namespace Models.Models
{
    public partial class Quotation
    {
        public int Id { get; set; }
        public string? PickupAddress { get; set; }
        public string? DestinationAddress { get; set; }
        public DateTime? PickUpDateTime { get; set; }
        public string? PackageDescription { get; set; }
        public int? NumberOfItems { get; set; }
        public double? Weight { get; set; }
        public string? Comment { get; set; }
        public int? VehicleType { get; set; }
        public int? NumberOfVehicles { get; set; }
        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? EmailAddress { get; set; }
        public DateTime? DateCreated { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
    }
}
