namespace Models.Models
{
    public partial class InventoryReceivedHeader
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int CompanyId { get; set; }
        public int? AdjustmentTypeId { get; set; }
        public DateTime TransactionDate { get; set; }
        public int? WarehouseId { get; set; }
        public int? WarehouseBinId { get; set; }
        public string? Notes { get; set; }
        public string? Void { get; set; }
        public bool Captured { get; set; }
        public bool Verified { get; set; }
        public string? VerifiedBy { get; set; }
        public DateTime? VerifiedDate { get; set; }
        public bool Issued { get; set; }
        public string? IssuedBy { get; set; }
        public DateTime? IssuedDate { get; set; }
        public bool Approved { get; set; }
        public string? ApprovedBy { get; set; }
        public DateTime ApprovedDate { get; set; }
        public bool Posted { get; set; }
        public string? PostedBy { get; set; }
        public DateTime PostedDate { get; set; }
        public string? BatchControlNumber { get; set; }
        public string? BatchControlTotal { get; set; }
        public string? Signature { get; set; }
        public string? SignaturePassword { get; set; }
        public string? SupervisorSignature { get; set; }
        public string? ManagerSignature { get; set; }
        public int? Currency { get; set; }
        public decimal CurrencyExchangeRate { get; set; }
        public string? Reference { get; set; }
        public int? WarehouseCustomerId { get; set; }
        public string? DeliveryNote { get; set; }
        public string? SiteNumber { get; set; }
        public string? VehicleRegistration { get; set; }
        public int? DriversId { get; set; }
        public int? FromCompanyId { get; set; }
        public int? FromWarehouseId { get; set; }
        public int? FromWarehouseBinId { get; set; }
        public int? InventoryIssueTransferId { get; set; }
    }
}
