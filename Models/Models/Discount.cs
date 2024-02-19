namespace Models.Models
{
    public partial class Discount
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public int BookingType { get; set; }
        public decimal AdultDiscount { get; set; }
        public decimal MinorDiscount { get; set; }
        public decimal MemberDiscount { get; set; }
        public decimal ReturnDiscount { get; set; }
        public decimal AppDiscountIos { get; set; }
        public decimal AppDiscountAndroid { get; set; }
        public decimal AppDiscountWeb { get; set; }
        public decimal AppReturnDiscountIos { get; set; }
        public decimal AppReturnDiscountAndroid { get; set; }
        public decimal AppReturnDiscountWeb { get; set; }
        public decimal PromoDiscount { get; set; }
        public bool Active { get; set; }
        public decimal CustomerDiscount { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
    }
}
