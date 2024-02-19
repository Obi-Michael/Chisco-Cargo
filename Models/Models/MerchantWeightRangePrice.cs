namespace Models.Models
{
    public partial class MerchantWeightRangePrice
    {
        public int Id { get; set; }
        public int MerchantId { get; set; }
        public int WeightRangeId { get; set; }
        public decimal WeightPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public decimal WeightPriceInterState { get; set; }

        public virtual MerchantSignup Merchant { get; set; } = null!;
        public virtual WeightRange WeightRange { get; set; } = null!;
    }
}
