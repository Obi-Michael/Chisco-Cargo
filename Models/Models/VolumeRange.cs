namespace Models.Models
{
    public partial class VolumeRange
    {
        public int Id { get; set; }
        public int? LowerRange { get; set; }
        public int? UpperRange { get; set; }
        public string? Description { get; set; }
        public string? Code { get; set; }
        public int? CustomerType { get; set; }
        public decimal? PricePerKm { get; set; }
        public decimal? FixedPrice { get; set; }
        public decimal? WeightPriceLevel1 { get; set; }
        public decimal? WeightPriceLevel2 { get; set; }
        public decimal? WeightPriceLevel3 { get; set; }
        public decimal? WeightPriceHighest { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
