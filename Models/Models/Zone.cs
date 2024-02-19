namespace Models.Models
{
    public partial class Zone
    {
        public Zone()
        {
            DeliveryTypePrices = new HashSet<DeliveryTypePrice>();
            SpecialPackagepricings = new HashSet<SpecialPackagepricing>();
            ZoneMappings = new HashSet<ZoneMapping>();
            ZonePricePerKgs = new HashSet<ZonePricePerKg>();
            ZonePrices = new HashSet<ZonePrice>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public string? ZoneDescription { get; set; }
        public decimal? ZonePrice { get; set; }

        public virtual ICollection<DeliveryTypePrice> DeliveryTypePrices { get; set; }
        public virtual ICollection<SpecialPackagepricing> SpecialPackagepricings { get; set; }
        public virtual ICollection<ZoneMapping> ZoneMappings { get; set; }
        public virtual ICollection<ZonePricePerKg> ZonePricePerKgs { get; set; }
        public virtual ICollection<ZonePrice> ZonePrices { get; set; }
    }
}
