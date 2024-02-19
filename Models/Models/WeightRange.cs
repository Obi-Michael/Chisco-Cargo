namespace Models.Models
{
    public partial class WeightRange
    {
        public WeightRange()
        {
            MerchantWeightRangePrices = new HashSet<MerchantWeightRangePrice>();
        }

        public int Id { get; set; }
        public string? Code { get; set; }
        public decimal? LowerRange { get; set; }
        public decimal? UpperRange { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }

        public virtual ICollection<MerchantWeightRangePrice> MerchantWeightRangePrices { get; set; }
    }
}
