namespace Models.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            ShipmentItems = new HashSet<ShipmentItem>();
        }

        public int Id { get; set; }
        public string Waybill { get; set; } = null!;
        public DateTime? DeliveryTime { get; set; }
        public int PaymentStatus { get; set; }
        public int? TypeofCustomer { get; set; }
        public int? CustomerId { get; set; }
        public int? DepartureLocationId { get; set; }
        public int DestinationId { get; set; }
        public string ReceiverName { get; set; } = null!;
        public string? ReceiverPhoneNumber { get; set; }
        public string? ReceiverEmail { get; set; }
        public string? ReceiverAddress { get; set; }
        public int? ReceiverStateId { get; set; }
        public int? DeliveryTypeId { get; set; }
        public string? GroupId { get; set; }
        public int? PickupOptions { get; set; }
        public string? SpecifiedDateofDelivery { get; set; }
        public DateTime? ExpectedDateOfArrival { get; set; }
        public DateTime? ActualArrivalDate { get; set; }
        public decimal? ItemsWeight { get; set; }
        public decimal? GrandTotal { get; set; }
        public bool IsCashOnDelivery { get; set; }
        public decimal? CashOnDeliveryAmount { get; set; }
        public decimal? ExpectedAmountToCollect { get; set; }
        public decimal? ActualAmountCollected { get; set; }
        public string? CreatedBy { get; set; }
        public bool ValueIsDecleared { get; set; }
        public decimal? DeClearedValue { get; set; }
        public decimal? DiscountAmountGiven { get; set; }
        public bool IsInsured { get; set; }
        public decimal? Vat { get; set; }
        public decimal? Packagingfee { get; set; }
        public decimal? InsuranceAmount { get; set; }
        public decimal TotalTopay { get; set; }
        public string PaymentMethod { get; set; } = null!;
        public bool IsCancelled { get; set; }
        public string? Description { get; set; }
        public string? Specialnote { get; set; }
        public string? SenderAddress { get; set; }
        public int SenderStateId { get; set; }
        public bool HasArrived { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
        public string? BookingRefCode { get; set; }
        public decimal? ItemQuantity { get; set; }
        public string? PackagingType { get; set; }
        public bool? IsRefund { get; set; }
        public decimal? RefundAmount { get; set; }
        public bool? IsMissing { get; set; }
        public DateTime? IsMissingDate { get; set; }
        public string? IsMissingStatus { get; set; }
        public bool? IsCredit { get; set; }
        public DateTime? CreditPaymentDate { get; set; }
        public string? PayStackResponse { get; set; }
        public string? PayStackWebhookReference { get; set; }
        public string? PayStackReference { get; set; }
        public string? UserId { get; set; }
        public int? AccountType { get; set; }
        public string? TellerNumber { get; set; }
        public bool? IsVerified { get; set; }
        public decimal? VerifiedAmount { get; set; }
        public string? SenderEmail { get; set; }
        public string? SenderName { get; set; }
        public string? SenderPhoneNumber { get; set; }
        public int? IsCollected { get; set; }
        public float? PickUpLatitude { get; set; }
        public float? PickUpLongitude { get; set; }
        public float? DropOffLatitude { get; set; }
        public float? DropOffLongitude { get; set; }
        public string? SenderActualAddress { get; set; }
        public string? ReceiverActualAddress { get; set; }
        public string? SenderFirstName { get; set; }
        public string? SenderLastName { get; set; }
        public string? ReceiverFirstName { get; set; }
        public string? ReceiverLastName { get; set; }
        public int? ShipmentStatus { get; set; }
        public int? EscalationActionType { get; set; }
        public bool? IsTampered { get; set; }
        public DateTime? IsTamperedDate { get; set; }
        public string? IsTamperedStatus { get; set; }
        public int? PackageQuantity { get; set; }
        public string? Signature { get; set; }
        public string? SignatureName { get; set; }
        public string? SignaturePhoneNumber { get; set; }
        public int? MerchantShipmentId { get; set; }
        public int? CustomerPlatform { get; set; }
        public bool IsHomeDelivery { get; set; }
        public string? PosreferenceNo { get; set; }
        public string? TransferName { get; set; }
        public string? TransferDate { get; set; }
        public int? ServiceType { get; set; }

        public virtual Deliverytype? DeliveryType { get; set; }
        public virtual Location? DepartureLocation { get; set; }
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}
