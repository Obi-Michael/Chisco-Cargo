namespace Models.Models
{
    public partial class ItemType
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? ItemTypeName { get; set; }
        public string? ItemTypeDescription { get; set; }
        public int CompanyId { get; set; }
    }
}
