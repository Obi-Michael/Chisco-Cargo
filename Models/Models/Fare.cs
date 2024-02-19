namespace Models.Models
{
    public partial class Fare
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Name { get; set; }
        public decimal Amount { get; set; }
        public float? ChildrenDiscountPercentage { get; set; }
        public int RouteId { get; set; }
        public int VehicleModelId { get; set; }
        public decimal NonIdAmount { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
    }
}
