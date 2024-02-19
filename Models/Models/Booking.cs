using System;
using System.Collections.Generic;

namespace Models.Models
{
    public partial class Booking
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? PosReference { get; set; }
        public string? BookingReferenceCode { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public int Gender { get; set; }
        public string? Email { get; set; }
        public int NoOfTicket { get; set; }
        public string? PhoneNumber { get; set; }
        public int PaymentMethod { get; set; }
        public string? Address { get; set; }
        public string? NextOfKinName { get; set; }
        public string? NextOfKinPhoneNumber { get; set; }
        public int NumberOfTicketsPrinted { get; set; }
        public string? PickupPointImage { get; set; }
        public DateTime BookingDate { get; set; }
        public int PaymentGateway { get; set; }
        public string? SelectedSeats { get; set; }
        public string? PayStackReference { get; set; }
        public string? PayStackResponse { get; set; }
        public string? PayStackWebhookReference { get; set; }
        public string? GtbReference { get; set; }
        public string? FlutterWaveReference { get; set; }
        public string? FlutterWaveResponse { get; set; }
        public string? ApprovedBy { get; set; }
        public string? GlobalPayReference { get; set; }
        public string? GlobalPayResponse { get; set; }
        public string? QuickTellerReference { get; set; }
        public string? QuickTellerResponse { get; set; }
        public int BookingStatus { get; set; }
        public int PassengerType { get; set; }
        public int PickupStatus { get; set; }
        public int? BookingTypeId { get; set; }
        public int TravelStatus { get; set; }
        public DateTime? ExpiredDate { get; set; }
        public bool IsGhanaRoute { get; set; }
        public DateTime? IssuedDate { get; set; }
        public string? PassportId { get; set; }
        public string? PassportType { get; set; }
        public string? PlaceOfIssue { get; set; }
        public string? Nationality { get; set; }
        public string? ChildNames { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
