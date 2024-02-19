namespace Models.Models
{
    public partial class CustomerType1
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
        public int CustomerTypeId { get; set; }
        public string? CustomerTypeDescription { get; set; }
        public string? SalesControlAccount { get; set; }
        public string? CoscontrolAccount { get; set; }
        public string? DebtorsControlAccount { get; set; }
        public string? CurrencyExchangeGainOrLossAccount { get; set; }
        public string? DiscountsAccount { get; set; }
        public string? DiscountRate { get; set; }
        public string? BillingType { get; set; }
    }
}
