namespace Models.Models
{
    public partial class InventoryItem
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? ItemCode { get; set; }
        public bool IsActive { get; set; }
        public int ItemTypeId { get; set; }
        public string? ItemName { get; set; }
        public string? ItemDescription { get; set; }
        public int ItemCategoryId { get; set; }
        public int ItemFamilyId { get; set; }
        public string? PictureUrl { get; set; }
        public string? ItemWeight { get; set; }
        public string? ItemUpccode { get; set; }
        public string? ItemColor { get; set; }
        public string? ItemUom { get; set; }
        public string? GlitemSalesAccountName { get; set; }
        public string? GlitemCogsaccountName { get; set; }
        public string? GlitemInventoryAccountName { get; set; }
        public decimal Price { get; set; }
        public string? ItemPricingCode { get; set; }
        public string? VendorId { get; set; }
        public float ReOrderLevel { get; set; }
        public float ReOrderQty { get; set; }
        public decimal Lifo { get; set; }
        public decimal Lifovalue { get; set; }
        public decimal Lifocost { get; set; }
        public decimal Average { get; set; }
        public decimal AverageValue { get; set; }
        public decimal AverageCost { get; set; }
        public decimal Fifofifovalue { get; set; }
        public decimal Fifocost { get; set; }
        public decimal Expected { get; set; }
        public decimal ExpectedValue { get; set; }
        public decimal ExpectedCost { get; set; }
        public bool IsSerialLotItem { get; set; }
        public bool AllowPurchaseTrans { get; set; }
        public bool AllowSalesTrans { get; set; }
        public bool AllowInventoryTrans { get; set; }
        public int CompanyId { get; set; }
        public int ItemDefaultWarehouseBinId { get; set; }
        public string? ItemDefaultWarehouseBinName { get; set; }
        public int ItemDefaultWarehouseId { get; set; }
        public string? ItemDefaultWarehouseName { get; set; }
        public int GlitemCogsaccountId { get; set; }
        public int GlitemInventoryAccountId { get; set; }
        public int GlitemSalesAccountId { get; set; }
        public int BrandTypeId { get; set; }
    }
}
