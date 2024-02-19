namespace Models.Models
{
    public partial class ShipmentTracking
    {
        public int TrackingId { get; set; }
        public string Waybill { get; set; } = null!;
        public string? Location { get; set; }
        public string? Status { get; set; }
        public DateTime? DateTime { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
