namespace Models.Models
{
    public partial class WarehouseBin
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? WarehouseBinIdcode { get; set; }
        public int WarehouseId { get; set; }
        public string? WarehouseBinName { get; set; }
        public string? WarehouseBinNumber { get; set; }
        public string? WarehouseBinZone { get; set; }
        public string? WarehouseBinType { get; set; }
        public string? WarehouseBinLocation { get; set; }
        public string? WarehouseBinLength { get; set; }
        public string? WarehouseBinWidth { get; set; }
        public string? WarehouseBinHeight { get; set; }
        public long WarehouseBinWeight { get; set; }
        public float MinimumQuantity { get; set; }
        public float MaximumQuantity { get; set; }
        public string? OverFlowBin { get; set; }
        public bool IsActive { get; set; }
        public int CompanyId { get; set; }
    }
}
