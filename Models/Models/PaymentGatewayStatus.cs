namespace Models.Models
{
    public partial class PaymentGatewayStatus
    {
        public int Id { get; set; }
        public string? Gateway { get; set; }
        public bool Status { get; set; }
    }
}
