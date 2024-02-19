namespace Models.Models
{
    public partial class NextNumber
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? NextNumberName { get; set; }
        public string? NextNumberValue { get; set; }
        public string? NextNumberPrefix { get; set; }
        public string? NextNumberSeparator { get; set; }
        public int CompanyId { get; set; }
    }
}
