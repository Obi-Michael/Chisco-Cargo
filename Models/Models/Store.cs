namespace Models.Models
{
    public partial class Store
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int Type { get; set; }
        public string? Name { get; set; }
        public string? StoreKeeper { get; set; }
        public int TerminalId { get; set; }
    }
}
