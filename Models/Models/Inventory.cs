namespace Models.Models
{
    public partial class Inventory
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Code { get; set; }
        public int CompanyId { get; set; }
        public DateTime CaptureDate { get; set; }
        public int ItemId { get; set; }
        public int WarehouseId { get; set; }
        public int WarehouseBinId { get; set; }
        public int VendorId { get; set; }
        public int Quantity { get; set; }
        public int Unit { get; set; }
        public decimal Price { get; set; }
        public int QuantityInStock { get; set; }
        public int QuantityReleased { get; set; }
        public decimal Vat { get; set; }
    }
}
