namespace Models.Models
{
    public partial class CustomerInformation
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int? CompanyId { get; set; }
        public int? AccountStatus { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress1 { get; set; }
        public string? CustomerAddress2 { get; set; }
        public string? CustomerAddress3 { get; set; }
        public string? CustomerCity { get; set; }
        public string? CustomerState { get; set; }
        public string? CustomerZip { get; set; }
        public string? CustomerCountry { get; set; }
        public string? CustomerPhone { get; set; }
        public string? CustomerFax { get; set; }
        public string? CustomerEmail { get; set; }
        public string? CustomerWebPage { get; set; }
        public string? CustomerFirstName { get; set; }
        public string? CustomerLastName { get; set; }
        public string? CustomerSalutation { get; set; }
        public int? CustomerTypeId { get; set; }
        public string? TaxIdno { get; set; }
        public string? VattaxIdnumber { get; set; }
        public string? VatTaxOtherNumber { get; set; }
        public int? CurrencyId { get; set; }
        public int? GlsalesAccount { get; set; }
        public int? TermsId { get; set; }
        public string? TermsStart { get; set; }
        public int? TaxGroupId { get; set; }
        public string? PriceMatrix { get; set; }
        public string? PriceMatrixCurrent { get; set; }
        public string? CreditRating { get; set; }
        public decimal? CreditLimit { get; set; }
        public string? CreditComments { get; set; }
        public string? PaymentDay { get; set; }
        public DateTime ApprovalDate { get; set; }
        public DateTime CustomerSince { get; set; }
        public string? SendCreditMemos { get; set; }
        public string? SendDebitMemos { get; set; }
        public string? Statements { get; set; }
        public string? StatementCycleCode { get; set; }
        public string? CustomerSpecialInstructions { get; set; }
        public string? CustomerShipToId { get; set; }
        public string? CustomerShipForId { get; set; }
        public string? ShipMethodId { get; set; }
        public int? WarehouseId { get; set; }
        public string? WarehouseGlaccount { get; set; }
        public bool? Approved { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string? ReferedBy { get; set; }
        public DateTime ReferedDate { get; set; }
        public string? ReferalUrl { get; set; }
        public decimal? AccountBalance { get; set; }
        public int? UserId { get; set; }
        public int? WalletId { get; set; }
    }
}
