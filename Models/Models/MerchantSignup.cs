namespace Models.Models
{
    public partial class MerchantSignup
    {
        public MerchantSignup()
        {
            MerchantWeightRangePrices = new HashSet<MerchantWeightRangePrice>();
        }

        public int Id { get; set; }
        public string? Businessname { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public string? Phone { get; set; }
        public string? Emailladdress { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Businesstype { get; set; }
        public string? Peakperiod { get; set; }
        public string? Offpeak { get; set; }
        public DateTime? Datecreated { get; set; }
        public string? CodeFromReferrer { get; set; }
        public string? ReferralCode { get; set; }
        public string? UserId { get; set; }
        public int? VolumeRangeId { get; set; }
        public int? DiscountRangeId { get; set; }
        public bool? IsApproved { get; set; }
        public decimal? FixedPrice { get; set; }
        public decimal? PricePerKm { get; set; }
        public int? PriceType { get; set; }
        public decimal? FixedPriceInterState { get; set; }
        public decimal? MinimumDropOffCost { get; set; }
        public decimal? MaximumDropOffCost { get; set; }
        public decimal? MinimumPickUpCost { get; set; }
        public decimal? MaximumPickUpCost { get; set; }

        public virtual ICollection<MerchantWeightRangePrice> MerchantWeightRangePrices { get; set; }
    }
}
