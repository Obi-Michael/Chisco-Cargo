namespace Models.Models
{
    public partial class ShipmentItem
    {
        public int Id { get; set; }
        public string ItemDescription { get; set; } = null!;
        public int ShipmentType { get; set; }
        public decimal ItemWeight { get; set; }
        public string? ItemNature { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? ShipmentId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public int? ItemCategoryId { get; set; }
        public string? ItemCategoryDescription { get; set; }
        public int? SpecialPackagePricingId { get; set; }

        public virtual Shipment? Shipment { get; set; }
    }
}
