namespace Models.Models
{
    public partial class InventoryTracker
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
        public int IncreasedQuantity { get; set; }
        public int InventoryId { get; set; }
        public string? IncreasedBy { get; set; }
        public decimal NewPrice { get; set; }
    }
}
