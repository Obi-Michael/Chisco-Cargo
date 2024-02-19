namespace Models.Models
{
    public partial class InventoryRequest
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public DateTime RequestDate { get; set; }
        public int WarehouseId { get; set; }
        public int Quantity { get; set; }
        public int WarehouseBinId { get; set; }
        public DateTime ApprovedDate { get; set; }
        public int CompanyId { get; set; }
        public int DriverId { get; set; }
        public DateTime IssuedDate { get; set; }
        public int ItemId { get; set; }
        public decimal Price { get; set; }
        public string? ReferenceNumber { get; set; }
        public int Unit { get; set; }
        public int VehicleRegId { get; set; }
        public DateTime VerifiedDate { get; set; }
        public int DepartmentId { get; set; }
        public string? RequestedBy { get; set; }
        public bool IsApproved { get; set; }
        public bool IsCaptured { get; set; }
        public bool IsVerified { get; set; }
        public int IssueId { get; set; }
        public bool IsIssued { get; set; }
        public int QuantityInStock { get; set; }
        public int QuantityReleased { get; set; }
        public DateTime CancelledDate { get; set; }
        public bool IsCancelled { get; set; }
        public string? RequesterEmail { get; set; }
        public int? ItemFourId { get; set; }
        public int? ItemThreeId { get; set; }
        public int? ItemTwoId { get; set; }
        public int MechanicId { get; set; }
        public string? CreditType { get; set; }
        public string? Note { get; set; }
        public string? ApprovedBy { get; set; }
        public string? CancelledBy { get; set; }
        public string? IssuedBy { get; set; }
        public string? PaymentApprovalBy { get; set; }
        public string? VerifiedBy { get; set; }
        public int TerminalId { get; set; }
        public int? CompanyTypeId { get; set; }
        public bool DebitCaptured { get; set; }
        public decimal Amount { get; set; }
        public string? CompanyName { get; set; }
    }
}
