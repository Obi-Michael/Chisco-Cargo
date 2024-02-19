namespace Models.Models
{
    public partial class CutomerInvoice
    {
        public int Int { get; set; }
        public string InvoiceNo { get; set; } = null!;
        public decimal Amount { get; set; }
        public string? PaymentMethod { get; set; }
        public int PaymentStatus { get; set; }
        public DateTime DatePaid { get; set; }
        public string? WaybillNumber { get; set; }
        public DateTime? DateDue { get; set; }
        public int LocationId { get; set; }
        public bool ShipmentCollected { get; set; }
        public string? PaymentRef { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
