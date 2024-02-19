namespace Models.Models
{
    public partial class ShipmentParcel
    {
        public int Id { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Waybill { get; set; }
        public int? AccountType { get; set; }
        public bool? IsPickedUp { get; set; }
        public string? IsPickUpBy { get; set; }
        public DateTime? IsPickedDate { get; set; }
        public string? IsPickedUpFrom { get; set; }
        public bool? IsAssigned { get; set; }
        public string? IsAssignedBy { get; set; }
        public string? ParcelAssignedTo { get; set; }
        public string? ParcelStatus { get; set; }
        public bool? IsDroppedOff { get; set; }
        public string? IsDroppedOffBy { get; set; }
        public DateTime? IsDroppedOffDate { get; set; }
        public string? ParcelDropTo { get; set; }
        public DateTime? AssignedDate { get; set; }
        public string? Vehicle { get; set; }
        public bool? IsAccepted { get; set; }
        public string? AcceptedBy { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public decimal? DispatchRiderCommission { get; set; }
        public float? PickUpLatitude { get; set; }
        public float? PickUpLongitude { get; set; }
        public float? DropOffLatitude { get; set; }
        public float? DropOffLongitude { get; set; }
        public bool? IsInterstate { get; set; }
        public string? SenderEmail { get; set; }
        public string? ParcelDropOffTerminal { get; set; }
    }
}
