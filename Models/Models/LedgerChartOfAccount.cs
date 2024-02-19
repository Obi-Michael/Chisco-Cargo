namespace Models.Models
{
    public partial class LedgerChartOfAccount
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? CglaccountNumber { get; set; }
        public string? GlaccountName { get; set; }
        public string? GlaccountDescription { get; set; }
        public string? GlaccountUse { get; set; }
        public string? GlaccountType { get; set; }
        public string? GlbalanceType { get; set; }
        public bool GlreportingAccount { get; set; }
        public string? CurrencyId { get; set; }
        public float CurrencyExchangeRate { get; set; }
        public decimal GlaccountBalance { get; set; }
        public decimal GlaccountBeginningBalance { get; set; }
        public string? GlotherNotes { get; set; }
        public string? CashFlowType { get; set; }
    }
}
