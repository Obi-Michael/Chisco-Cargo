namespace Models.Models
{
    public partial class SpecialShipmentPrice
    {
        public int Id { get; set; }
        public string ShipmentItem { get; set; } = null!;
        public decimal PricePerKg { get; set; }
    }
}
