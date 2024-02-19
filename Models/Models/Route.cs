namespace Models.Models
{
    public partial class Route
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
        public int Type { get; set; }
        public decimal DispatchFee { get; set; }
        public decimal DriverFee { get; set; }
        public decimal LoaderFee { get; set; }
        public bool AvailableAtTerminal { get; set; }
        public bool AvailableOnline { get; set; }
        public int? ParentRouteId { get; set; }
        public string? ParentRoute { get; set; }
        public int DepartureTerminalId { get; set; }
        public int DestinationTerminalId { get; set; }
    }
}
