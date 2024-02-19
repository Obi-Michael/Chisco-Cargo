namespace Models.Models
{
    public partial class ZonePricePerKg
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public decimal PricePerKg { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Zone Zone { get; set; } = null!;
    }
}
