namespace Models.Models
{
    public partial class VendorType
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int CompanyId { get; set; }
        public string? VendorTypeDescription { get; set; }
        public string? CreditorsControlAccount { get; set; }
        public string? CurrencyExchangeGainOrLossAccount { get; set; }
        public string? DiscountsAccount { get; set; }
        public string? DiscountRate { get; set; }
        public string? Name { get; set; }
    }
}
