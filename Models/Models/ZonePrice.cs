namespace Models.Models
{
    public partial class ZonePrice
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public decimal Price { get; set; }
        public decimal Weight { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual Zone Zone { get; set; } = null!;
    }
}
