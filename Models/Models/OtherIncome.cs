namespace Models.Models
{
    public partial class OtherIncome
    {
        public Guid Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string? PaymentName { get; set; }
        public string? PaymentDescription { get; set; }
        public string? Issuer { get; set; }
        public double Amount { get; set; }
        public int TerminalId { get; set; }
        public string? TerminalName { get; set; }
    }
}
