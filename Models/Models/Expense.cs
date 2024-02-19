namespace Models.Models
{
    public partial class Expense
    {
        public Guid Id { get; set; }
        public string? Description { get; set; }
        public string? Receiver { get; set; }
        public string? Issuer { get; set; }
        public double Amount { get; set; }
        public int TerminalId { get; set; }
        public int AccountingStatus { get; set; }
        public string? AuthorisedBy { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
