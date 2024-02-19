namespace Models.Models
{
    public partial class VendorInformation
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
        public int? AccountStatus { get; set; }
        public string? VendorName { get; set; }
        public string? VendorAddress1 { get; set; }
        public string? VendorAddress2 { get; set; }
        public string? VendorAddress3 { get; set; }
        public string? VendorCity { get; set; }
        public string? VendorState { get; set; }
        public string? VendorZip { get; set; }
        public string? VendorCountry { get; set; }
        public string? VendorPhone { get; set; }
        public string? VendorFax { get; set; }
        public string? VendorWebPage { get; set; }
        public int VendorTypeId { get; set; }
        public string? AccountNumber { get; set; }
        public string? ContactId { get; set; }
        public int? WarehouseId { get; set; }
        public string? CurrencyId { get; set; }
        public string? TermsId { get; set; }
        public string? TermsStart { get; set; }
        public string? GlpurchaseAccount { get; set; }
        public string? TaxIdno { get; set; }
        public string? VattaxIdnumber { get; set; }
        public string? VatTaxOtherNumber { get; set; }
        public string? TaxGroupId { get; set; }
        public string? CreditLimit { get; set; }
        public string? AvailibleCredit { get; set; }
        public string? CreditComments { get; set; }
        public string? CreditRating { get; set; }
        public int ApprovalDate { get; set; }
        public string? CustomerSince { get; set; }
        public string? FreightPayment { get; set; }
        public string? CustomerSpecialInstructions { get; set; }
        public string? SpecialTerms { get; set; }
        public string? ConvertedFromCustomer { get; set; }
        public string? VendorRegionId { get; set; }
        public string? VendorSourceId { get; set; }
        public string? VendorIndustryId { get; set; }
        public bool Comfirmed { get; set; }
        public string? ReferedBy { get; set; }
        public string? ReferedDate { get; set; }
        public string? ReferalUrl { get; set; }
        public decimal AccountBalance { get; set; }
        public string? Note { get; set; }
    }
}
