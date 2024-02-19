namespace Models.Models
{
    public partial class FareCalendar
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
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int FareType { get; set; }
        public decimal FareValue { get; set; }
        public int? RouteId { get; set; }
        public int FareAdjustmentType { get; set; }
        public int? TerminalId { get; set; }
        public int? VehicleModelId { get; set; }
        public int FareParameterType { get; set; }
        public bool? Saturday { get; set; }
        public bool? Sunday { get; set; }
        public bool? Friday { get; set; }
        public bool? Monday { get; set; }
        public bool? Thursday { get; set; }
        public bool? Tuesday { get; set; }
        public bool? Wednesday { get; set; }
        public int? BookingTypes { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
    }
}
