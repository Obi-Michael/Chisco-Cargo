namespace Models.Models
{
    public partial class Warehouse
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? WarehouseCode { get; set; }
        public string? WarehouseName { get; set; }
        public string? WarehouseAddress1 { get; set; }
        public string? WarehouseAddress2 { get; set; }
        public string? WarehouseCity { get; set; }
        public string? WarehouseState { get; set; }
        public string? WarehouseZip { get; set; }
        public string? WarehousePhone { get; set; }
        public string? WarehouseFax { get; set; }
        public string? WarehouseEmail { get; set; }
        public string? StockControlAccount { get; set; }
        public string? SalesControlAccount { get; set; }
        public string? CoscontrolAccount { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}
