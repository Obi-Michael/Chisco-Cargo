namespace Models.Models
{
    public partial class GeneralTransaction
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int TransactionType { get; set; }
        public int PayTypeDiscription { get; set; }
        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? TransactedBy { get; set; }
        public bool IsActive { get; set; }
        public string? TransDescription { get; set; }
    }
}
