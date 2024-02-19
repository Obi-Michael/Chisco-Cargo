namespace Models.Models
{
    public partial class JourneyManagement
    {
        public Guid Id { get; set; }
        public DateTime? ActualTripStartTime { get; set; }
        public DateTime? TripStartTime { get; set; }
        public DateTime? TripEndTime { get; set; }
        public Guid? TransloadedJourneyId { get; set; }
        public DateTime JourneyDate { get; set; }
        public string? ApprovedBy { get; set; }
        public string? ReceivedBy { get; set; }
        public decimal DispatchFee { get; set; }
        public decimal CaptainFee { get; set; }
        public decimal LoaderFee { get; set; }
        public Guid VehicleTripRegistrationId { get; set; }
        public int JourneyStatus { get; set; }
        public string? DenialReason { get; set; }
        public int JourneyType { get; set; }
        public int CaptainTripStatus { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public string? TransloadedBy { get; set; }
        public DateTime TransloadedDate { get; set; }
    }
}
