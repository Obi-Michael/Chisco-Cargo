namespace Models.Models
{
    public partial class ShipmentPackaging
    {
        public int Id { get; set; }
        public string? ShipmentId { get; set; }
        public int PackageType { get; set; }
        public string? Createdby { get; set; }
        public DateTime CreationTime { get; set; }
        public decimal? PackagingFee { get; set; }
        public int? DepartureLocationId { get; set; }
        public int? DestinationId { get; set; }
    }
}
