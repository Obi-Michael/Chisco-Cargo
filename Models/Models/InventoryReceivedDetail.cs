namespace Models.Models
{
    public partial class InventoryReceivedDetail
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
        public Guid InventoryReceivedId { get; set; }
        public int? ItemId { get; set; }
        public string? Description { get; set; }
        public int RequestedQty { get; set; }
        public int WarehouseId { get; set; }
        public int WarehouseBinId { get; set; }
        public int ToWarehouseId { get; set; }
        public int ToWarehouseBinId { get; set; }
        public int? GlexpenseAccount { get; set; }
        public float ItemValue { get; set; }
        public float ItemCost { get; set; }
        public int? CostMethod { get; set; }
        public int? ProjectId { get; set; }
        public int? GlanalysisType1 { get; set; }
        public int? GlanalysisType2 { get; set; }
        public int? AssetId { get; set; }
        public string? Ponumber { get; set; }
        public string? ItemUpccode { get; set; }
        public float ReceivedQty { get; set; }
        public int? InventoryTransferId { get; set; }
    }
}
