namespace Models.Models
{
    public partial class ShipmentRequest
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? PickUpAddress { get; set; }
        public string? DropOffAddress { get; set; }
        public DateTime? PickupDate { get; set; }
        public string? PickupTime { get; set; }
        public string? PickupState { get; set; }
        public string? DropOffState { get; set; }
        public string? PackageType { get; set; }
        public string? PackagePiece { get; set; }
        public string? PackageWeight { get; set; }
        public string? Comments { get; set; }
        public string? TruckNumber { get; set; }
        public string? TruckSize { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
