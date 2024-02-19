namespace Models.Models
{
    public partial class GroupWayBillNumber
    {
        public int GroupId { get; set; }
        public string GroupWaybillCode { get; set; } = null!;
        public bool IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public int? DepartureId { get; set; }
        public int? ArrivalId { get; set; }
        public bool HasManifest { get; set; }
        public bool? SentToHub { get; set; }
        public int? HubId { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
