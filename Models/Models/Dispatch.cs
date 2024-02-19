namespace Models.Models
{
    public partial class Dispatch
    {
        public int Id { get; set; }
        public string? VehicleRegnum { get; set; }
        public string ManifestNumber { get; set; } = null!;
        public decimal DispatchFee { get; set; }
        public string? DriverInfo { get; set; }
        public int? DepartureId { get; set; }
        public int? Detinationid { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public string? Dispatchedby { get; set; }
        public string? ReceivedBy { get; set; }

        public virtual Location? Departure { get; set; }
    }
}
