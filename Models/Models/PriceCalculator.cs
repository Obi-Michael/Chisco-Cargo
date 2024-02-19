namespace Models.Models
{
    public partial class PriceCalculator
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? State { get; set; }
        public decimal? PriceTdrforBike { get; set; }
        public decimal? PriceTdrforVan { get; set; }
        public decimal? PriceTdrforTruck { get; set; }
        public bool? IsDefaultCost { get; set; }
        public decimal? DefaultBikePrice { get; set; }
        public decimal? DefaultVanPrice { get; set; }
        public decimal? DefaultTruckPrice { get; set; }
        public decimal? CommissionforBike { get; set; }
        public decimal? CommissionforVan { get; set; }
        public decimal? CommissionforTruck { get; set; }
        public decimal? MinimumInKm { get; set; }
        public decimal? MaximumInKm { get; set; }
        public decimal? MinimumInValue { get; set; }
        public decimal? MaximumInValue { get; set; }
        public decimal? MinimumPickUpCost { get; set; }
        public decimal? MaximumPickUpCost { get; set; }
        public decimal? DropOffPriceforBike { get; set; }
        public decimal? DropOffPriceforVan { get; set; }
        public decimal? DropOffPriceforTruck { get; set; }
        public decimal? MinimumPriceTrigger { get; set; }
        public decimal? MaximumPriceTrigger { get; set; }
        public decimal? TerminalPriceForTruck { get; set; }
    }
}
