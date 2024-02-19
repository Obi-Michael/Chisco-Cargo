namespace Models.Models
{
    public partial class EmployeeRoute
    {
        public long Id { get; set; }
        public long EmployeeRouteId { get; set; }
        public bool IsActive { get; set; }
        public int? TerminalId { get; set; }
        public int? EmployeeId { get; set; }
        public int? RouteId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
