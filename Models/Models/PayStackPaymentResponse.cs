namespace Models.Models
{
    public partial class PayStackPaymentResponse
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
    }
}
