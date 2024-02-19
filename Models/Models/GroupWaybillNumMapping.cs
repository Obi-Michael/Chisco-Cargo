namespace Models.Models
{
    public partial class GroupWaybillNumMapping
    {
        public int MappingId { get; set; }
        public bool IsActive { get; set; }
        public string? GroupWaybillNumber { get; set; }
        public string WaybillNumber { get; set; } = null!;
        public int DepartureId { get; set; }
        public int DestinationId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Location Departure { get; set; } = null!;
        public virtual Location Destination { get; set; } = null!;
    }
}
