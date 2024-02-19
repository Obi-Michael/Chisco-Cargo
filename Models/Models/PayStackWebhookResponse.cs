namespace Models.Models
{
    public partial class PayStackWebhookResponse
    {
        public string Reference { get; set; } = null!;
        public int ApprovedAmount { get; set; }
        public string? AuthorizationCode { get; set; }
        public string? CardType { get; set; }
        public string? Last4 { get; set; }
        public bool Reusable { get; set; }
        public string? Bank { get; set; }
        public string? ExpireMonth { get; set; }
        public string? ExpireYear { get; set; }
        public DateTime TransactionDate { get; set; }
        public string? Channel { get; set; }
        public string? Status { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
