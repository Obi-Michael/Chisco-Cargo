namespace Models.Models
{
    public partial class ZoneMapping
    {
        public int Id { get; set; }
        public int DepartuteId { get; set; }
        public int DestinationId { get; set; }
        public int ZoneId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Location Departute { get; set; } = null!;
        public virtual Location Destination { get; set; } = null!;
        public virtual Zone Zone { get; set; } = null!;
    }
}
