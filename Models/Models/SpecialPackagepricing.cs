namespace Models.Models
{
    public partial class SpecialPackagepricing
    {
        public int Id { get; set; }
        public decimal Weight { get; set; }
        public decimal Price { get; set; }
        public string? Desription { get; set; }
        public int ZoneId { get; set; }
        public int SpecialPackageId { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public int? ShipmentItemCategoryId { get; set; }
        public bool? IsBikeable { get; set; }
        public bool? IsVanable { get; set; }
        public bool? IsTruckable { get; set; }

        public virtual SpecialPackage SpecialPackage { get; set; } = null!;
        public virtual Zone Zone { get; set; } = null!;
    }
}
