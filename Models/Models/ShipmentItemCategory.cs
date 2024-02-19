namespace Models.Models
{
    public partial class ShipmentItemCategory
    {
        public int Id { get; set; }
        public string? ItemCategoryCode { get; set; }
        public string? ItemCategoryDescription { get; set; }
        public decimal? PriceValue { get; set; }
        public decimal? MinWeight { get; set; }
        public decimal? MaxWeight { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
    }
}
