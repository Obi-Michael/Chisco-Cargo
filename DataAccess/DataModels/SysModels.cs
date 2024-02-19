using Models.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
//using Utility.Services;

namespace DataAccess.DataModels
{
    public class SysModels
    {

        private readonly LibDataClass _libDataClass;
        public SysModels(LibDataClass libDataClass)
        {
            _libDataClass = libDataClass;
        }


        #region Enums
        public enum MerchantPriceType
        {
            Fixed = 0,
            Weight = 1,
            FixedandKM = 2,
        }
        public enum EscalationActionType
        {
            Paid = 0,
            Closed = 1,
            Unpaid = 2,
            Investigating = 3,
            Sorted = 4,
            Called = 5
        }
        public enum DiscountTypesEnum
        {
            OnboardingDiscount = 0,
            TransactionalDiscount = 1,
            ReferralDiscount = 2,
            VolumeDiscount = 3
        }
        public enum DeviceType
        {
            DispatchExpress = 0,
            LibmotExpress = 1
        }
        public enum ParcelStatus
        {//ShipmentStatus
            InTransit = 0,
            Pending = 1,
            IsCompleted = 2,
            IsDamaged = 3,
            IsAccidented = 4,
            IsPickedUp = 5,
            AtHub = 6,
            Waiting = 7,
            Delivered = 8,
            IsAccepted = 9,
            IsDroppedOff = 10,
            Arrived = 11,
        }
        public enum AccountType
        {
            Android = 0, IOS = 1, Web = 2
        }
        public enum ItemCostType
        {
            Generic = 0, NonGeneric = 1
        }
        public enum PriceParamater
        {
            Discount = 0,
            Increase = 1,
            FlatTreshhold = 2,
            FlatDiscount = 3 /*however, we should not let the value become negative*/
        }
        public enum ConvertRate
        {
            Percentage = 0, Value = 1
        }

        public enum PlatformType
        {
            android = 0, ios = 1, web = 2
        }
        public enum VehicleType
        {
            Bike = 0,
            Van = 1,
            Truck = 2,
            //Minivan = 3
        }
        public enum DispatchType
        {
            Pickup = 0,
            DropOff = 1,
            PickupAndDropOff = 2
        }
        public enum itemcondition
        {
            Good = 0,
            Damaged = 1,
            PartiallyDamaged = 2
        }
        public enum ismissingstatus
        {
            Found = 0,
            Spoilt = 1,
            Theft = 2,
            Pending = 3
        }
        public enum JourneyStatus
        {
            Pending = 0,
            Approved = 1,
            InTransit = 2,
            Received = 3,
            Transloaded = 4,
            Denied = 5
        }
        public enum TransactionType
        {
            Debit = 0,
            Credit = 1,
            Transfer = 2
        }
        public enum JourneyType
        {
            Loaded = 0,
            Blown = 1,
            Pickup = 2,
            Rescue = 3,
            Transload = 4,
            Hire = 5
        }
        public enum VehicleStatus
        {
            Idle = 0,
            Working = 1,
            InWorkshop = 2,
            SpecialAssignement = 3,
            OnSales = 4,
            MovedToEcobus = 5,
            Disabled = 6,
            SiezedByAuthority = 7,
            Accidented = 8,
            NoCaptain = 9,
            BrokenDown = 10,
            Patrol = 11,
            TerminalUse = 12,
            RepairRequested = 13,
            WorkshopReleased = 14,
            TripDenied = 15,
            PoliceStation = 16,
            InWorkshopAndAssigned = 17,
            Pickup = 18,
            DueForService = 19,
            InTransit = 20,
            All = 21
        }
        public enum UserType
        {
            Employee = 0,
            Merchant = 1,
            Customer = 2,
            Guest = 3
        }
        public enum PaymentStatus
        {
            Pending = 0,
            Approved = 1,
            Cancelled = 2,
            Created = 3,
            Declined = 4,
            Expired = 5,
            Failed = 6,
            OnLock = 7,
            OnPayment = 8,
            Ongoing = 9,
            Abandoned = 10,
            Refunded = 11,
            Reversed = 12,
            TransactionError = 13,
            Unsuccessful = 14,
            GtbCancelled = 15,
            Suspended = 16,
            Paid = 17,
            Coupon = 18
        }
        public enum PaymentMethod
        {
            Cash = 0,
            Pos = 1,
            UnifiedPaymentSolutionsLtd = 2,
            InterswitchLtd = 3,
            Isonhold = 4,
            PayStack = 5,
            BankIt = 6,
            GtbUssd = 7,
            FlutterWave = 8,
            FlutterWaveUssd = 9,
            UnionBank = 10,
            DiamondBank = 11,
            FirstBank = 12,
            CashAndPos = 13,
            ZenithBank = 14,
            GlobalPay = 15,
            QuickTeller = 16,
            GlobalAccelerex = 17,
            SterlingBank = 18,
            EBillsPay = 19,
            Transfer = 20,
            Credit = 21,
            EWallet = 22,
            Coupon = 23
        }
        public enum DurationType // for coupons
        {
            Second = 0,
            Minute = 1,
            Hour = 2,
            Day = 3,
            Month = 4,
            Year = 5,
            Week = 6
        }
        public enum Gender
        {
            Male = 0,
            Female = 1
        }
        public enum CouponType
        {

            Percentage = 0,
            //FixedValue
            Fixed = 1

        }
        public enum CreditStatus
        {
            PaidCredit = 0,
            UnpaidCredit = 1,
            AllCredit = 2
        }
        public enum ShipmentCollectionStatus
        {
            Collected = 1,
            NotCollected = 0
        }
        public enum ShipmentStatus
        {
            Waiting = 0,
            InTransit = 1,
            Delivered = 2,
        }

        public enum TamperedStatus
        {
            Theft = 0,
            Spoiled = 1,
            Damaged = 2,
        }

        public enum TravelType
        {
            IntraState = 0,
            InterState = 1
        }

        public enum MerchantPaymentType
        {
            [Display(Name = "Terminal DropOff | PickUp")]
            TerminalDropOffAndPickUp = 0,
            [Display(Name = "Direct PickUp | Terminal PickUp")]
            DRPickUpTerminalPickup = 1,
            [Display(Name = "Terminal DropOff | Home Delivery")]
            TerminalDropOffHomeDelivery = 2,
            [Display(Name = "DR PickUp | Home Delivery")]
            DRPickUpHomeDelivery = 3
        }

        public enum CompanyInfo
        {
            [Display(Name = "Libmot Express")]
            LimbotExpress = 0,
            [Display(Name = "Libmot (Motors)")]
            Libmot = 1,
        }

        public enum TrackingStatus
        {
            Arrived = 0,
            Departed = 1,
            Delayed = 2,
            Transloaded = 3,
        }

        #endregion      

        public class ShipmentModel
        {

            #region entity properties
            //entity properties
            public int Id { get; set; }
            public string Waybill { get; set; }
            public DateTime? DeliveryTime { get; set; }
            public int PaymentStatus { get; set; }
            public int? typeofCustomer { get; set; }
            public int? customerId { get; set; }
            public int? departureLocationId { get; set; }
            public int destinationId { get; set; }
            public string receiverName { get; set; }
            public string receiverPhoneNumber { get; set; }
            public string receiverEmail { get; set; }
            public string receiverAddress { get; set; }
            public int? receiverStateId { get; set; }
            public int? deliveryTypeId { get; set; }
            public string groupId { get; set; }
            public int? PickupOptions { get; set; }
            public string SpecifiedDateofDelivery { get; set; }
            public DateTime? expectedDateOfArrival { get; set; }
            public DateTime? actualArrivalDate { get; set; }
            public decimal? TotalWeight /*itemsWeight*/ { get; set; }
            public System.Nullable<decimal> TotalPrice /*grandTotal*/ { get; set; }
            public bool isCashOnDelivery { get; set; }
            public System.Nullable<decimal> cashOnDeliveryAmount { get; set; }
            public System.Nullable<decimal> expectedAmountToCollect { get; set; }
            public System.Nullable<decimal> actualAmountCollected { get; set; }
            public string createdBy { get; set; }
            public bool ValueIsDeceleared { get; set; }
            public decimal? deClearedValue { get; set; }
            public System.Nullable<decimal> discountAmountGiven { get; set; }
            public bool isInsured { get; set; }
            public decimal? vat { get; set; }
            public decimal? packagingfee { get; set; }
            public int? packagingQuantity { get; set; }
            public System.Nullable<decimal> insuranceAmount { get; set; }
            public decimal totalTopay { get; set; }
            public string paymentMethod { get; set; }
            public bool isCancelled { get; set; }
            public string description { get; set; }
            public string senderAddress { get; set; }
            public int senderStateId { get; set; }
            public bool HasArrived { get; set; }
            public DateTime DateCreated { get; set; }
            public System.Nullable<System.DateTime> DateModified { get; set; }
            public bool isDeleted { get; set; }
            public string specialnote { get; set; }
            public string BookingRefCode { get; set; }
            public System.Nullable<decimal> ItemQuantity { get; set; }
            public string PackagingType { get; set; }
            public int? PackageQuantity { get; set; }
            public System.Nullable<bool> IsRefund { get; set; }
            public System.Nullable<decimal> RefundAmount { get; set; }
            public System.Nullable<bool> IsMissing { get; set; }
            public string IsMissingStatus { get; set; }
            public System.Nullable<System.DateTime> IsMissingDate { get; set; }
            public System.Nullable<bool> IsCredit { get; set; }
            public System.Nullable<System.DateTime> CreditPaymentDate { get; set; }
            public string PayStackResponse { get; set; }
            public string PayStackWebhookReference { get; set; }
            public string PayStackReference { get; set; }
            public string UserId { get; set; }
            public int? AccountType { get; set; }
            public string TellerNumber { get; set; }
            public bool? IsVerified { get; set; }
            public decimal? VerifiedAmount { get; set; }
            public string senderEmail { get; set; }
            public string senderName { get; set; }
            public string senderphone { get; set; }
            public int? IsCollected { get; set; }
            public float? senderLat /*PickUpLatitude*/ { get; set; }
            public float? senderLng /*PickUpLongitude*/ { get; set; }
            public float? receiverLat /*DropOffLatitude*/ { get; set; }
            public float? receiverLng /*DropOffLongitude*/ { get; set; }
            public string SenderActualAddress { get; set; }
            public string ReceiverActualAddress { get; set; }
            public int? ShipmentStatus { get; set; }
            public string SenderFirstName { get; set; }
            public string SenderLastName { get; set; }
            public string ReceiverFirstName { get; set; }
            public string ReceiverLastName { get; set; }
            public string ReleasedBy { get; set; }
            public DateTime? ReleasedDate { get; set; }
            public System.Nullable<bool> IsTampered { get; set; }
            public System.Nullable<System.DateTime> IsTamperedDate { get; set; }
            public string IsTamperedStatus { get; set; }
            public string Signature { get; set; }
            public string SignatureName { get; set; }
            public string SignaturePhoneNumber { get; set; }
            public int? MerchantShipmentID { get; set; }
            public int? CustomerPlatform { get; set; }
            public bool IsHomeDelivery { get; set; }
            public string POSReferenceNO { get; set; }
            public string TransferName { get; set; }
            public string TransferDate { get; set; }
            public int? ServiceType { get; set; }
            public string PosCash { get; set; }




            #endregion


            #region non-entity properties
            //Other non-entity properties
            public string VehicleRegNumber { get; set; }
            public string CreditReceiversname { get; set; }
            public string CreditReceiversPhoneNo { get; set; }
            public string CreditReleaseby { get; set; }
            public string GroupNumber { get; set; }
            public string ManifestNumber { get; set; }
            public string CustomerType { get; set; }
            public string departureLocation { get; set; }
            public string destinationLocation { get; set; }
            public string destinationState { get; set; }
            public string departureState { get; set; }
            public string DeliveryType { get; set; }
            public string GrandTotalValue { get; set; }
            public bool Released { get; set; }
            public bool Collected { get; set; }
            public string DeclaredValue { get; set; }
            public bool? isReceived { get; set; }
            public DateTime? IsReceivedDate { get; set; }
            public PayStackPaymentResponseModel PayStackPaymentResponse { get; set; }
            public string destinationLocationAddress { get; set; }
            public string departureLocationAddress { get; set; }
            public int? ZoneId { get; set; }
            #endregion

            #region object properties
            //Object Properties
            public List<ShipmentItemModel> Items { get; set; }
            public LocationModel Location { get; set; }

            #endregion

            public static implicit operator ShipmentModel(Shipment p)
            {
                if (p != null)
                {
                    var shipmentStatus = Convert.ToInt32(new LibDataClass().GetShipmentStatus(p.Waybill));
                    ShipmentModel resp = new ShipmentModel
                    {
                        //entity properties
                        Id = p.Id,
                        Waybill = p.Waybill,
                        DeliveryTime = p.DeliveryTime,
                        PaymentStatus = p.PaymentStatus,
                        typeofCustomer = p.TypeofCustomer,
                        customerId = p.CustomerId,
                        departureLocationId = p.DepartureLocationId,
                        destinationId = p.DestinationId,
                        receiverName = p.ReceiverName,
                        receiverPhoneNumber = p.ReceiverPhoneNumber,
                        receiverEmail = p.ReceiverEmail,
                        receiverAddress = p.ReceiverAddress,
                        receiverStateId = p.ReceiverStateId,
                        deliveryTypeId = p.DeliveryTypeId,
                        groupId = p.GroupId,
                        PickupOptions = p.PickupOptions,
                        SpecifiedDateofDelivery = p.SpecifiedDateofDelivery,
                        expectedDateOfArrival = p.ExpectedDateOfArrival,
                        actualArrivalDate = p.ActualArrivalDate,
                        createdBy = p.CreatedBy,
                        ValueIsDeceleared = p.ValueIsDecleared,
                        deClearedValue = p.DeClearedValue,
                        discountAmountGiven = p.DiscountAmountGiven,
                        isInsured = p.IsInsured,
                        vat = p.Vat,
                        packagingfee = p.Packagingfee,
                        packagingQuantity = p.PackageQuantity,
                        insuranceAmount = p.InsuranceAmount,
                        totalTopay = p.TotalTopay,
                        paymentMethod = p.PaymentMethod,
                        isCancelled = p.IsCancelled,
                        description = p.Description,
                        senderAddress = p.SenderAddress,
                        senderStateId = p.SenderStateId,
                        HasArrived = p.HasArrived,
                        DateCreated = p.DateCreated,
                        DateModified = p.DateModified,
                        isDeleted = p.IsDeleted,
                        specialnote = p.Specialnote,
                        BookingRefCode = p.BookingRefCode,
                        ItemQuantity = p.ItemQuantity,
                        PackagingType = p.PackagingType,
                        IsRefund = p.IsRefund,
                        RefundAmount = p.RefundAmount,
                        IsMissing = p.IsMissing,
                        IsMissingStatus = p.IsMissingStatus,
                        IsMissingDate = p.IsMissingDate,
                        IsCredit = p.IsCredit,
                        PayStackResponse = p.PayStackResponse,
                        PayStackWebhookReference = p.PayStackWebhookReference,
                        PayStackReference = p.PayStackReference,
                        UserId = p.UserId,
                        AccountType = p.AccountType,
                        ShipmentStatus = shipmentStatus,
                        TellerNumber = p.TellerNumber,
                        IsVerified = p.IsVerified,
                        VerifiedAmount = p.VerifiedAmount,
                        senderphone = p.SenderPhoneNumber,
                        senderEmail = p.SenderEmail,
                        TotalPrice = p.GrandTotal,
                        actualAmountCollected = p.ActualAmountCollected,
                        cashOnDeliveryAmount = p.CashOnDeliveryAmount,
                        CreditPaymentDate = p.CreditPaymentDate,
                        expectedAmountToCollect = p.ExpectedAmountToCollect,
                        isCashOnDelivery = p.IsCashOnDelivery,
                        TotalWeight = p.ItemsWeight,
                        receiverLat = p.DropOffLatitude,
                        receiverLng = p.DropOffLongitude,
                        IsCollected = p.IsCollected,
                        senderLat = p.PickUpLatitude,
                        senderLng = p.PickUpLongitude,
                        ReceiverActualAddress = p.ReceiverActualAddress,
                        SenderActualAddress = p.SenderActualAddress,
                        senderName = p.SenderName,

                        SenderFirstName = p.SenderFirstName,
                        SenderLastName = p.SenderLastName,
                        ReceiverFirstName = p.ReceiverFirstName,
                        ReceiverLastName = p.ReceiverLastName,

                        IsTampered = p.IsTampered,
                        IsTamperedDate = p.IsTamperedDate,

                        //other properties
                        CreditReceiversname = p.ReceiverName,
                        CreditReceiversPhoneNo = p.ReceiverPhoneNumber,
                        CreditReleaseby = null,
                        VehicleRegNumber = null,



                        //object properties
                        Items = p.ShipmentItems.ToList().ToModelList(),
                        Location = p.Location

                    };
                    return resp;
                }
                return null;
            }
        }
        public class LoginModel
        {
            public string username { get; set; }
            public string password { get; set; }
        }
        public class RegisterModel
        {
            public string Email { get; set; }
            public int Id { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Password { get; set; }
            public string FullName => FirstName + " " + LastName;
            public string MiddleName { get; set; }
            public int Gender { get; set; }
            public string PhoneNumber { get; set; }
            public string Address { get; set; }
            public string CodeFromReferrer { get; set; }
            public string ReferralCode { get; set; }
            public int? AccountType { get; set; }
            public int LocationId { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime? DateofBirth { get; set; }
        }
        public class PasswordResetModel
        {
            public string usernameOrEmail { get; set; }
            public string ActivationCode { get; set; }
            public string NewPassword { get; set; }
            public string OTP { get; set; }
        }
        public class CustomerPlatformTypeModel
        {
            public int ID { get; set; }
            public string PlatformType { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime CreationTime { get; set; }
            public string LastModificationUserId { get; set; }
            public DateTime? LastModificationDate { get; set; }
            public bool IsDeleted { get; set; }
        }

        public class ServiceTypeModel
        {
            public int ID { get; set; }
            public string ServiceType { get; set; }
            public string CreatorUserId { get; set; }
            public DateTime CreationTime { get; set; }
            public string LastModificationUserId { get; set; }
            public DateTime? LastModificationDate { get; set; }
            public bool IsDeleted { get; set; }
        }



        public class ServiceTypePriceModel
        {
            public int id { get; set; }
            public string ServiceType { get; set; }
            public int ServiceTypeId { get; set; }
            public string zonename { get; set; }
            public int zoneid { get; set; }
            public decimal price { get; set; }
            public bool IsActive { get; set; }
            //public int CustomerType { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
            public bool IsDeleted { get; set; }
            // public string customertType { get; set; }
        }
        public class ChangePasswordModel
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }
        }
        public class TokenDTO
        {
            public string Token { get; set; }
            public DateTime Expires { get; set; }
        }
        public class WalletModel
        {
            //entity properties
            public int Id { get; set; }
            public string WalletNumber { get; set; }
            public decimal Balance { get; set; }
            public string UserType { get; set; }
            public string UserId { get; set; }
            public bool IsReset { get; set; }
            public DateTime? LastResetDate { get; set; }
            public bool IsDeleted { get; set; }

            //non-entity properties
        }
        public class WalletTransactionDTO
        {
            //entity properties
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public TransactionType TransactionType { get; set; }
            public decimal TransactionAmount { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal LineBalance { get; set; }
            public int WalletId { get; set; }
            public DateTime CreationTime { get; set; }
            public string TransactedBy { get; set; }
            public string TransactionDescription { get; set; }
            public string Reference { get; set; }
            public bool IsCompleted { get; set; }
            public string WayBill { get; set; }
            public string PayStackResponse { get; set; }
            public string PayStackWebhookReference { get; set; }
            public string PayStackReference { get; set; }
            public string paymentMethod { get; set; }

            //non- entity prop
            public string TransType { get; set; }
        }
        public class ShipmentResponseDTO
        {
            public string Response { get; set; }
            public decimal? Amount { get; set; }
            public string WayBill { get; set; }
            public string WalletTransactionReference { get; set; }
            public string DateCreated { get; set; }
            public string PaymentResponse { get; set; }
            public string departureLocation { get; set; }
            public string destinationLocation { get; set; }
        }
        public class PaystackVerifyResponseDto
        {
            public bool status { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
        }
        public class PaymentResponseDTO
        {
            public bool Status { get; set; }
            public decimal? TransactionAmount { get; set; }
            public decimal? WalletBalance { get; set; }
            public DateTime? CreationDate { get; set; }
            public TransactionType TransactionType { get; set; }
            public string CreatedBy { get; set; }
            public string Response { get; set; }
        }

        public class Data
        {
            public int id { get; set; }
            public string domain { get; set; }
            public string status { get; set; }
            public string reference { get; set; }
            public int amount { get; set; }
            //public object message { get; set; }
            public string gateway_response { get; set; }
            public DateTime? paid_at { get; set; }
            public DateTime? created_at { get; set; }
            public string channel { get; set; }
            public string currency { get; set; }
            //public object ip_address { get; set; }
            //public string metadata { get; set; }
            //public object log { get; set; }
            //public int fees { get; set; }
            //public object fees_split { get; set; }
            public Authorization authorization { get; set; }
            public CustomerModel customer { get; set; }
            //public object plan { get; set; }
            //public object order_id { get; set; }
            public DateTime? paidAt { get; set; }
            public DateTime? createdAt { get; set; }
            public DateTime? transaction_date { get; set; }
            //public PlanObject plan_object { get; set; }
            //public Subaccount subaccount { get; set; }
        }


        public class Authorization
        {
            public string authorization_code { get; set; }
            public string bin { get; set; }
            public string last4 { get; set; }
            public string exp_month { get; set; }
            public string exp_year { get; set; }
            public string channel { get; set; }
            public string card_type { get; set; }
            public string bank { get; set; }
            public string country_code { get; set; }
            public string brand { get; set; }
            public bool reusable { get; set; }
            public string signature { get; set; }
        }

        public class CustomerModel
        {
            public int id { get; set; }

            public string firstName { get; set; }

            public string lastName { get; set; }

            public int gender { get; set; }

            public string email { get; set; }

            public string address { get; set; }

            public string phoneNumber { get; set; }

            public string password { get; set; }

            public DateTime DateCreated { get; set; }

            public DateTime? DateModified { get; set; }

            public bool IsDeleted { get; set; }

            public string CodeFromReferrer { get; set; }

            public string ReferralCode { get; set; }
            public string UserId { get; set; }

            public DateTime? DateofBirth { get; set; }

            public static implicit operator CustomerModel(Customer customer)
            {
                if (customer == null) { return null; }
                CustomerModel resp = new CustomerModel
                {
                    id = customer.Id,
                    firstName = customer.FirstName,
                    lastName = customer.LastName,
                    gender = customer.Gender,
                    email = customer.Email,
                    address = customer.Address,
                    phoneNumber = customer.PhoneNumber,
                    password = customer.Password,
                    DateCreated = customer.DateCreated,
                    DateModified = customer.DateModified,
                    IsDeleted = customer.IsDeleted,
                    CodeFromReferrer = customer.CodeFromReferrer,
                    ReferralCode = customer.ReferralCode,
                    UserId = customer.UserId,
                    DateofBirth = customer.DateofBirth
                };
                return resp;
            }
        }

        public class PayStackWebhookResponseDTO
        {
            [Key]
            public string Reference { get; set; }
            public int ApprovedAmount { get; set; }
            public string AuthorizationCode { get; set; }
            public string CardType { get; set; }
            public string Last4 { get; set; }
            public bool Reusable { get; set; }
            public string Bank { get; set; }
            public string ExpireMonth { get; set; }
            public string ExpireYear { get; set; }
            public DateTime TransactionDate { get; set; }
            public string Channel { get; set; }
            public string Status { get; set; }
        }
        public class PayStackResponseModel
        {
            public string email { get; set; }

            public int amount { get; set; }

            public string wayBill { get; set; }
            public string PayStackReference { get; set; }
            public TransactionType TransactionType { get; set; }
        }
        public class itemConditionModel
        {
            public int ID { get; set; }
            public string Itemcondition { get; set; }
            public bool Isdeleted { get; set; }
        }
        public class StateModel
        {
            public int id { get; set; }
            public string name { get; set; }
            public string regionName { get; set; }
            public System.Nullable<bool> Instate;
        }

        public class EmployeeModel
        {
            public int id { get; set; }
            public string fname { get; set; }
            public string lname { get; set; }
            public string locationName { get; set; }
            public int locationId { get; set; }
            public string RoleId { get; set; }
            public string UserID { get; set; }
            public string UserEmail { get; set; }
            public string UserPhone { get; set; }
            public string eachRole { get; set; }
            //public virtual List<AspNetRole> GetUserRole { get; set; }
            //public List<UserRoleModel> listofRoles { get; set; }
            public int Gender { get; set; }
            public bool IsActiveUser { get; set; }
            public string PhoneNumber { get; set; }
        }
        public class UserProfileModel
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
            public int Gender { get; set; }
            public string Address { get; set; }
            public string DateCreated { get; set; }
            public string ActivationCode { get; set; }
            public UserType UserType { get; set; } = UserType.Merchant;
            public string UserId { get; set; }
            public string ReferralCode { get; set; }
            public string DateofBirth { get; set; }
        }
        public class regionmodel
        {
            public int id { get; set; }
            public string name { get; set; }
        }
        public class SalesPerWeekModel
        {
            public string TotalSalesPerDay { get; set; }
            public DateTime DateofSales { get; set; }
        }

        public partial class SalesIModel
        {
            //[JsonObject("chartm")]
            public Chartm Chartmodel { get; set; }

            [JsonProperty("data")]
            public List<Datum> Data { get; set; }
            public partial class Datum
            {
                [JsonProperty("label")]
                public string Label { get; set; }

                [JsonProperty("value")]
                public string Value { get; set; }
            }
            [JsonProperty("trendlines")]
            public List<Trendline> Trendlines { get; set; }
        }

        public partial class Chartm
        {
            [JsonProperty("caption")]
            public string Caption { get; set; }

            [JsonProperty("subCaption")]
            public string SubCaption { get; set; }

            [JsonProperty("xAxisName")]
            public string XAxisName { get; set; }

            [JsonProperty("yAxisName")]
            public string YAxisName { get; set; }

            [JsonProperty("numberPrefix")]
            public string NumberPrefix { get; set; }

            [JsonProperty("paletteColors")]
            public string PaletteColors { get; set; }

            [JsonProperty("bgColor")]
            public string BgColor { get; set; }

            [JsonProperty("borderAlpha")]
            public long BorderAlpha { get; set; }

            [JsonProperty("canvasBorderAlpha")]
            public long CanvasBorderAlpha { get; set; }

            [JsonProperty("usePlotGradientColor")]
            public long UsePlotGradientColor { get; set; }

            [JsonProperty("plotBorderAlpha")]
            public long PlotBorderAlpha { get; set; }

            [JsonProperty("placevaluesInside")]
            public long PlacevaluesInside { get; set; }

            [JsonProperty("rotatevalues")]
            public long Rotatevalues { get; set; }

            [JsonProperty("valueFontColor")]
            public string ValueFontColor { get; set; }

            [JsonProperty("showXAxisLine")]
            public long ShowXAxisLine { get; set; }

            [JsonProperty("xAxisLineColor")]
            public string XAxisLineColor { get; set; }

            [JsonProperty("divlineColor")]
            public string DivlineColor { get; set; }

            [JsonProperty("divLineIsDashed")]
            public long DivLineIsDashed { get; set; }

            [JsonProperty("showAlternateHGridColor")]
            public long ShowAlternateHGridColor { get; set; }

            [JsonProperty("subcaptionFontBold")]
            public long SubcaptionFontBold { get; set; }

            [JsonProperty("subcaptionFontSize")]
            public long SubcaptionFontSize { get; set; }
        }



        public partial class Trendline
        {
            [JsonProperty("line")]
            public List<Line> Line { get; set; }
        }

        public partial class Line
        {
            [JsonProperty("startvalue")]
            public long Startvalue { get; set; }

            [JsonProperty("color")]
            public string Color { get; set; }

            [JsonProperty("valueOnRight")]
            public long ValueOnRight { get; set; }

            [JsonProperty("displayvalue")]
            public string Displayvalue { get; set; }
        }





        public class SalesPerLocationModel
        {
            public string TotalSalesPerDay { get; set; }
            public string DateofSales { get; set; }
            public string LocationName { get; set; }
            public string GrandTotal { get; set; }
            public string TotalVat { get; set; }
            public string TotalPkFee { get; set; }
            public string TotalCash { get; set; }
            public string TotalPos { get; set; }
            public string TotalCredit { get; set; }
            public string TotalTransfer { get; set; }
            public string TotalCashandPOS { get; set; }
            public int? LocationId { get; set; }
            public string CreatedBy { get; set; }
            public string TotalWeight { get; set; }
            public string EWallet { get; set; }
        }
        public class GetAcessTokenResponseModel
        {

            [JsonProperty("code")]
            public string Code { get; set; }

            [JsonProperty("shortDescription")]
            public string ShortDescription { get; set; }

            [JsonProperty("object")]
            public Tokenobj Object { get; set; }
            public class Tokenobj
            {
                public string token { get; set; }
                public string refreshToken { get; set; }
                public string expires { get; set; }

            }


        }
        public class GenericReportModel
        {
            public string WayBill { get; set; }
            public string DateCreated { get; set; }
            public string TotalPerDay { get; set; }
            public string LocationName { get; set; }
            public string GrandTotal { get; set; }
            public int? LocationId { get; set; }
            public string CreatedBy { get; set; }
            public string PackagingType { get; internal set; }
            public decimal PackagingFees { get; set; }
            public int? PackagingQuantity { get; internal set; }

        }


        public class CancelledShpModel
        {
            public string WayBill { get; set; }
            public string DateCreated { get; set; }
            public string TotalPerDay { get; set; }
            public string LocationName { get; set; }
            public string GrandTotal { get; set; }
            public string CreatedBy { get; set; }
            public int? LocationId { get; set; }
        }

        public class CancelledShipmentsModel
        {
            public string WayBill { get; set; }
            public string DateCreated { get; set; }
            public string TotalPerDay { get; set; }
            public string LocationName { get; set; }
            public string GrandTotal { get; set; }
            public string CreatedBy { get; set; }
            public int? LocationId { get; set; }
        }


        public class SalesPerday
        {
            public DateTime DateTime { get; set; }
            public decimal Sales { get; set; }
        }
        public class CustomerTypeModel
        {
            public int id { get; set; }
            public string Custype { get; set; }
            public bool? IsActive { get; set; }
        }
        public class InsuranceModel
        {
            public int id { get; set; }
            public string name { get; set; }
            public decimal value { get; set; }
            public bool IsActive { get; set; }
        }
        public class PackagePriceModel
        {
            public int id { get; set; }
            public string name { get; set; }
            public decimal price { get; set; }
            public bool isActive { get; set; }
            public DateTime dateCreated { get; set; }
            public DateTime? dateModified { get; set; }
            public bool isDeleted { get; set; }
        }
        public class DriversModel
        {
            public int id { get; set; }
            public string Drivername { get; set; }
            public string Driverphone { get; set; }
            public bool isActive { get; set; }
            public DateTime dateCreated { get; set; }
            public DateTime? dateModified { get; set; }
            public bool? isDeleted { get; set; }
        }

        public class VehicleModelList
        {
            public int MakeId { get; set; }
            public int ModelId { get; set; }
            public string Fullname { get; set; }
            public string MakeName { get; set; }
            public string ModelName { get; set; }
        }

        public class VehicleMakeList
        {
            public int Id { get; set; }
            public string Makename { get; set; }
        }

        public class SpecialPackageModel
        {
            public int id { get; set; }
            public string name { get; set; }
            public decimal ItemWeight { get; set; }
            public bool isActive { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime DateModified { get; set; }
            public bool IsDeleted { get; set; }
        }
        public class SpecialPackagePriceModel
        {
            public int id { get; set; }
            public decimal weight { get; set; }
            public decimal price { get; set; }
            public string desription { get; set; }
            public int zoneId { get; set; }
            public string zonename { get; set; }
            public int specialPackageId { get; set; }
            public string specialPackagename { get; set; }
            public bool isActive { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
            public bool IsDeleted { get; set; }
            public string Name { get; set; }

            //public  ZoneModel Zone { get; set; }
            public System.Nullable<int> ShipmentItemCategoryId;
            public string ShipmentItemCategoryName;

            public System.Nullable<bool> IsBikeable;

            public System.Nullable<bool> IsVanable;

            public System.Nullable<bool> IsTruckable;
        }
        public class UserRoleModel
        {
            public string Id { get; set; }
            public string Rolename { get; set; }
        }
        public class locationModel
        {
            public int id { get; set; }
            public string name { get; set; }
            public int stateId { get; set; }
            public int Locationtype { get; set; }
            public string statename { get; set; }
            public string FormattedName { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
            public bool IsDeleted { get; set; }
            public string Address { get; set; }
            public string Image { get; set; }
            public string Code { get; set; }
            public float? Latitude { get; set; }
            public float? Longitude { get; set; }
            public string ContactPerson { get; set; }
            public string ContactPersonNo { get; set; }
            public bool? IsCommision { get; set; }
        }
        public class SpecialShipmentModel
        {
            public int id { get; set; }
            public string Shipmentitem { get; set; }
            public decimal price { get; set; }
        }




        public class LocationModel
        {
            public int id { get; set; }

            public string locationName { get; set; }

            public int stateId { get; set; }

            public int LocationType { get; set; }

            public System.DateTime DateCreated { get; set; }

            public System.Nullable<System.DateTime> DateModified { get; set; }

            public System.Nullable<bool> IsDeleted { get; set; }

            public string Address { get; set; }

            public string Image { get; set; }

            public string Code { get; set; }

            public System.Nullable<float> Latitude { get; set; }

            public System.Nullable<float> Longitude { get; set; }

            public string ContactPerson { get; set; }

            public string ContactPersonNo { get; set; }

            public System.Nullable<bool> IsCommision { get; set; }

            public static implicit operator LocationModel(Location location)
            {
                if (location == null) { return null; }
                var resp = new LocationModel
                {
                    id = location.Id,
                    locationName = location.LocationName,
                    stateId = location.StateId,
                    LocationType = location.LocationType,
                    DateCreated = location.DateCreated,
                    DateModified = location.DateModified,
                    IsDeleted = location.IsDeleted,
                    Address = location.Address,
                    Image = location.Image,
                    Code = location.Code,
                    Latitude = location.Latitude,
                    Longitude = location.Longitude,
                    ContactPerson = location.ContactPerson,
                    ContactPersonNo = location.ContactPersonNo,
                    IsCommision = location.IsCommision
                };
                return resp;
            }
        }

        public class ZonePerKgModel
        {
            public int id { get; set; }
            public int zoneId { get; set; }
            public decimal pricePerKg { get; set; }
            public bool isDeleted { get; set; }
            public string zoneName { get; set; }
            public DateTime dateCreated { get; set; }
            public DateTime? dateModified { get; set; }
        }
        public class DeliveryTypeModel
        {
            public int id { get; set; }
            public string name { get; set; }
            public bool IsActive { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
            public bool IsDeleted { get; set; }
            public string CustomerType { get; set; }
        }
        public class DeliveryTypePriceModel
        {
            public int id { get; set; }
            public string DeliveryType { get; set; }
            public int DeliveryTypeId { get; set; }
            public string zonename { get; set; }
            public int zoneid { get; set; }
            public decimal price { get; set; }
            public bool IsActive { get; set; }
            public int CustomerType { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
            public bool IsDeleted { get; set; }
            public string customertType { get; set; }
        }

        public class UserList
        {
            public string Name { get; set; }
            public string Id { get; set; }
        }
        public class WaybillsList
        {
            public string Waybilnumber { get; set; }
        }
        public class returnmsg
        {
            public string code { get; set; }
            public string errormsg { get; set; }
            public string successmsg { get; set; }
            public bool completed { get; set; }
        }


        public class GroupWayBillNumberModel
        {
            #region entity properties
            public int GroupId { get; set; }

            public string GroupWaybillCode { get; set; }

            public bool IsActive { get; set; }

            public string createdBy { get; set; }

            public System.Nullable<int> departureId { get; set; }

            public System.Nullable<int> arrivalId { get; set; }

            public bool hasManifest { get; set; }

            public System.Nullable<bool> SentToHub { get; set; }

            public System.Nullable<int> HubId { get; set; }

            public System.Nullable<System.DateTime> DateCreated { get; set; }

            public System.Nullable<System.DateTime> DateModified { get; set; }

            public bool IsDeleted { get; set; }
            #endregion
            #region other properties
            public string waybillNumber { get; set; }
            public string departure { get; set; }

            public string arrival { get; set; }
            #endregion
        }
        public class GroupWaybillNumMappingModel
        {

            public int mappingId { get; set; }
            public string waybillNumber { get; set; }

            public string departureId { get; set; }
            public string destinationId { get; set; }

            #region other properties
            public string departure { get; set; }

            public string arrival { get; set; }
            #endregion


        }

        public partial class ZoneMappingModel
        {
            public int id { get; set; }

            public int departuteId { get; set; }

            public int destinationId { get; set; }

            public string ArrivalTerminalName { get; set; }
            public string DepartureTerminalName { get; set; }
            public int zoneId { get; set; }
            public string ZoneName { get; set; }

            public bool? isActive { get; set; }

            public DateTime DateCreated { get; set; }

            public DateTime? DateModified { get; set; }

            public bool isDeleted { get; set; }
        }
        public partial class ZonePriceModel
        {
            public int id { get; set; }

            public int zoneId { get; set; }
            public string zonename { get; set; }

            public decimal price { get; set; }

            public decimal weight { get; set; }

            public bool isActive { get; set; }

            public bool isDeleted { get; set; }

            public DateTime DateCreated { get; set; }

            public DateTime? DateModified { get; set; }
        }

        public class GroupShipmentListModel
        {
            public int manifestID { get; set; }
            //public List<WaybilNumberList> Waybillnum { get; set; }
            public string Waybillnum { get; set; }
            public string DestinationName { get; set; }
            public string DepartureName { get; set; }
            public DateTime? dateTime { get; set; }
            public string GroupNumber { get; set; }
            public string manifestNumber { get; set; }
            public bool HasArrived { get; set; }
            public bool? sentToHub { get; set; }
            public string HubName { get; set; }
            public string CreatedBy { get; set; }
        }

        public class GroupnumbersListModel
        {
            public string GroupNumber { get; set; }
        }

        public class ManifestModel
        {
            #region entity properties
            public int ManifestId { get; set; }
            public string ManifestNumber { get; set; }
            public int ShipmentId { get; set; }
            public int DepartId { get; set; }
            public decimal? DispatchFee { get; set; }
            public string DispatchedById { get; set; }
            public string ReceiverById { get; set; }
            public bool? IsDispatched { get; set; }
            public bool? IsReceived { get; set; }
            public int? VehicleId { get; set; }
            public int? DriverId { get; set; }
            public System.Nullable<int> ManifestType { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public System.Nullable<System.DateTime> DateModified { get; set; }
            public bool IsDeleted { get; set; }
            public System.Nullable<int> DestinationId { get; set; }
            public System.Nullable<System.DateTime> IsReceivedDate { get; set; }
            public string IsReceivedBy { get; set; }
            public System.Nullable<int> TransloadedVehicle { get; set; }
            public System.Nullable<int> TransloadedDriver { get; set; }
            public int? JourneyManagementId { get; set; }
            public int? TransloadedJourneyId { get; set; }
            #endregion

            #region model properties
            public string departureLocation { get; set; }
            public string dispatchedBy { get; set; }
            public string vehicleNumber { get; set; }
            public string driverName { get; set; }
            public List<Driver> DriverInfo { get; set; }
            public List<Vehicle> VehicleInfo { get; set; }
            public string driverphone { get; set; }
            public string groupnumber { get; set; }
            public string destination { get; set; }
            public string JourneyCode { get; set; }


            #endregion
        }
        public class FleetReportModel
        {
            #region entity properties
            public int ManifestId { get; set; }
            public string ManifestNumber { get; set; }
            public int ShipmentId { get; set; }
            public int DepartId { get; set; }
            public decimal? DispatchFee { get; set; }
            public string DispatchedById { get; set; }
            public string ReceiverById { get; set; }
            public bool? IsDispatched { get; set; }
            public bool? IsReceived { get; set; }
            public int? VehicleId { get; set; }
            public int? DriverId { get; set; }
            public System.Nullable<int> ManifestType { get; set; }
            public string CreatedBy { get; set; }
            public DateTime DateCreated { get; set; }
            public System.Nullable<System.DateTime> DateModified { get; set; }
            public bool IsDeleted { get; set; }
            public System.Nullable<int> DestinationId { get; set; }
            public System.Nullable<System.DateTime> IsReceivedDate { get; set; }
            public string IsReceivedBy { get; set; }
            public System.Nullable<int> TransloadedVehicle { get; set; }
            public System.Nullable<int> TransloadedDriver { get; set; }
            public int? JourneyManagementId { get; set; }
            public int? TransloadedJourneyId { get; set; }
            #endregion
            public string departureLocation { get; set; }
            public string dispatchedBy { get; set; }
            public string vehicleNumber { get; set; }
            public string driverName { get; set; }
            public List<Driver> DriverInfo { get; set; }
            public List<Vehicle> VehicleInfo { get; set; }
            public string driverphone { get; set; }
            public string groupnumber { get; set; }
            public string destination { get; set; }
            public string JourneyCode { get; set; }

            public string TotalSalesPerDay { get; set; }
            public string DateofSales { get; set; }
            public string locationName { get; set; }
            public string grandTotal { get; set; }
            public string totalVat { get; set; }
            public string totalPkFee { get; set; }
            public string totalCash { get; set; }
            public string totalPos { get; set; }
            public string totalCredit { get; set; }
            public string totalTransfer { get; set; }
            public string totalCashandPOS { get; set; }
            public string wayBillNumber { get; set; }
            public string shipmentCreatedBy { get; set; }
            public string description { get; set; }
            public string HasArrived { get; set; }

        }


        public class TrackingModel
        {
            [JsonProperty("Code")]
            public string Code { get; set; }

            [JsonProperty("ShortDescription")]
            public string ShortDescription { get; set; }

            [JsonProperty("Shipmentobj")]
            public TrackShipmentsModel ShipmentObj { get; set; }
            public class TrackShipmentsModel
            {
                public int id { get; set; }
                public string WayBillNumber { get; set; }
                public string ItemDesc { get; set; }
                public string senderFname { get; set; }
                public string senderLname { get; set; }
                public string ShSenderAddress { get; set; }
                public DateTime? TimeofDelivery { get; set; }
                public int ShPaymentstatus { get; set; }
                public string ShpCustomerType { get; set; }
                public string departureLocation { get; set; }
                public string destinationLocation { get; set; }
                public string destinationState { get; set; }
                public string departureState { get; set; }
                public int? destinationStateId { get; set; }
                public int departureStateId { get; set; }
                public string SHReceiverName { get; set; }
                public string ShReceiverAddress { get; set; }
                public string TerminalPickUpAddress { get; set; }
                public DateTime? expectedDeveliveryDate { get; set; }
                public DateTime? actualArrivalDate { get; set; }
                public string Receiverphone { get; set; }
                public string ShDeliveryType { get; set; }
                public string GrandTotalValue { get; set; }
                public decimal? VatValue { get; set; }
                public string CreatedBy { get; set; }
                public string PaymentMode { get; set; }
                public decimal? ShpitemWeight { get; set; }
                public DateTime DateofCreation { get; set; }
                public bool Released { get; set; }
                public bool HasArrived { get; set; }
                public bool Collected { get; set; }
                public DateTime? DepartureDate { get; set; }
                ///public int servicetype { get; set; }
                //public List<ShipmentItemModel> shipmentItem { get; set; }
            }

            [JsonProperty("Trackingobj")]
            //[JsonIgnore]
            public List<ShipmentTrackingModel> Trackingobj { get; set; }
            public class ShipmentTrackingModel
            {
                public string WaybillNumber { get; set; }
                public string CurrentLocation { get; set; }
                public string CurrentStatus { get; set; }
                public string Dateupdated { get; set; }
            }
            [JsonProperty("ShipmentParcelTracking")]
            //[JsonIgnore]
            public List<ShipmentParcelTrackingModel> ShipmentParcelTracking { get; set; }
            public class ShipmentParcelTrackingModel
            {
                public string IsPickUpFrom { get; set; }
                public string ParcelDropTo { get; set; }
                public ParcelStatus ParcelStatus { get; set; }
                public DateTime? IsPickUpDate { get; set; }
                public DateTime? IsDroppedOffDate { get; set; }
            }

        }

        public class NotificationModel
        {
            public int IncomingCount { get; set; }
            public string Description { get; set; }
        }

        public class PayStackPaymentResponseModel
        {
            public string Reference { get; set; }

            public int ApprovedAmount { get; set; }

            public string AuthorizationCode { get; set; }

            public string CardType { get; set; }

            public string Last4 { get; set; }

            public bool Reusable { get; set; }

            public string Bank { get; set; }

            public string ExpireMonth { get; set; }

            public string ExpireYear { get; set; }

            public DateTime TransactionDate { get; set; }

            public string Channel { get; set; }

            public string Status { get; set; }

            public static implicit operator PayStackPaymentResponseModel(PayStackPaymentResponse psr)
            {
                if (psr != null)
                {
                    PayStackPaymentResponseModel payStackPaymentResponseModel = new PayStackPaymentResponseModel
                    {
                        Reference = psr.Reference,
                        ApprovedAmount = psr.ApprovedAmount,
                        AuthorizationCode = psr.AuthorizationCode,
                        CardType = psr.CardType,
                        Last4 = psr.Last4,
                        Reusable = psr.Reusable,
                        Bank = psr.Bank,
                        ExpireMonth = psr.ExpireMonth,
                        ExpireYear = psr.ExpireYear,
                        TransactionDate = psr.TransactionDate,
                        Channel = psr.Channel,
                        Status = psr.Status
                    };
                    return payStackPaymentResponseModel;
                }
                return null;
            }
        }
        public class ShipmentOrderModel
        {
            //public string Name { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string PickupAdd { get; set; }
            public string DropOffAdd { get; set; }
            public DateTime PickupDate { get; set; }
            public string PickupTime { get; set; }
            public string PickupState { get; set; }
            public string DropoffState { get; set; }
            public string PickupType { get; set; }
            public string PackagePiece { get; set; }
            public string ItemWeight { get; set; }
            public string Comment { get; set; }
            //public string TruckNum { get; set; }
            //public string TruckSize { get; set; }
            public string Email { get; set; }
            public string ReceiverFirstName { get; set; }
            public string ReceiverLastName { get; set; }
            public string ReceiverEmail { get; set; }
            public string ReceiverPhone { get; set; }

        }
        public class MerchantsModel
        {
            public int ID { get; set; }
            public string BizName { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Phone { get; set; }
            public string State { get; set; }
            public string emailAdd { get; set; }
            public string City { get; set; }
            public string BizType { get; set; }
            public string PeakPeriod { get; set; }
            public string OffPeak { get; set; }
            public DateTime? DateCreated { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string PhoneNumber { get; set; }

            public UserType UserType { get; set; } = UserType.Merchant;
            public string ReferralCode { get; set; }
            public string CodeFromReferrer { get; set; }
            public System.Nullable<int> VolumeRangeId { get; set; }
            public string UserId { get; set; }
            public System.Nullable<bool> IsApproved { get; set; }
            #region model prop
            public int? AccountType { get; set; }
            public System.Nullable<int> PriceType { get; set; }
            public System.Nullable<decimal> FixedPrice { get; set; }
            public decimal? FixedPriceInterState { get; set; }
            public decimal? WeightPriceLevel1 { get; set; }
            public decimal? WeightPriceLevel2 { get; set; }
            public decimal? WeightPriceLevel3 { get; set; }
            public decimal? WeightPriceHighest { get; set; }
            public decimal? MinimumDropOffCost { get; set; }
            public decimal? MaximumDropOffCost { get; set; }
            public decimal? MinimumPickUpCost { get; set; }
            public decimal? MaximumPickUpCost { get; set; }
            //public System.Nullable<decimal> PricePerKm { get; set; }

            public decimal? PricePerKm { get; set; }
            public List<MerchantWeightRangePriceModel> WeightRangePrice { get; set; }
            #endregion
            public static implicit operator MerchantsModel(MerchantSignup s)
            {
                if (s == null) { return null; }
                MerchantsModel model = new MerchantsModel
                {
                    ID = s.Id,
                    BizName = s.Businessname,
                    BizType = s.Businesstype,
                    FirstName = s.Firstname,
                    LastName = s.Lastname,
                    Phone = s.Phone,
                    State = s.State,
                    emailAdd = s.Emailladdress,
                    City = s.City,
                    PeakPeriod = s.Peakperiod,
                    OffPeak = s.Offpeak,
                    DateCreated = s.Datecreated,
                    Email = s.Emailladdress,
                    PhoneNumber = s.Phone,
                    UserType = UserType.Merchant,
                    ReferralCode = s.ReferralCode,
                    CodeFromReferrer = s.CodeFromReferrer,
                    VolumeRangeId = s.VolumeRangeId,
                    UserId = s.UserId,
                    IsApproved = s.IsApproved,
                    PriceType = s.PriceType,
                    FixedPrice = s.FixedPrice,
                    FixedPriceInterState = s.FixedPriceInterState,
                    PricePerKm = s.PricePerKm,
                    MinimumDropOffCost = s.MinimumDropOffCost,
                    MaximumDropOffCost = s.MaximumDropOffCost,
                    MinimumPickUpCost = s.MinimumPickUpCost,
                    MaximumPickUpCost = s.MaximumPickUpCost,

                    //WeightPriceLevel1 = s.WeightPriceLevel1,
                    //WeightPriceLevel2 = s.WeightPriceLevel2,
                    //WeightPriceLevel3 = s.WeightPriceLevel3,
                    //WeightPriceHighest = s.WeightPriceHighest,

                    WeightRangePrice = s.MerchantWeightRangePrices.ToList()
                };
                return model;
            }
        }

        public class ShipmentCollectionModel
        {
            public string waybillNumber { set; get; }
            public string Receivername { set; get; }
            public string ReceiverPhone { set; get; }
            public string ReceiverEmail { set; get; }
            public string ReceiverAddress { set; get; }
            public string MeansOfId { set; get; }
            public string Releasedby { set; get; }
            public DateTime? DateReleased { set; get; }
            public DateTime Datemodified { set; get; }
            public bool IsDeleted { set; get; }
        }
        public class JourneyManagementModel
        {
            public int Id { get; set; }
            public DateTime? ActualTripStartTime { get; set; }
            public DateTime? TripStartTime { get; set; }
            public DateTime? TripEndTime { get; set; }
            public int? TransloadedJourneyId { get; set; }
            public DateTime JourneyDate { get; set; }
            public DateTime DateCreated { get; set; }
            public string ApprovedBy { get; set; }
            public string CreatedBy { get; set; }
            public string ReceivedBy { get; set; }
            public decimal DispatchFee { get; set; }
            public decimal DriverFee { get; set; }
            public decimal LoaderFee { get; set; }
            public string ManifestNumber { get; set; }
            public int ManifestId { get; set; }
            public int? VehicleId { get; set; }
            public int DriverId { get; set; }
            public string departure { get; set; }
            public string vehicleName { get; set; }
            public string driverName { get; set; }
            public string JourneyType { get; set; }
            public string JourneyStatus { get; set; }
            public string JourneyCode { get; set; }
        }
        public class ManifestInformation
        {
            public string drivername { get; set; }
            public string driverphone { get; set; }
            public string manifestdate { get; set; }
            public string Vehicleinfo { get; set; }
            public string JourneyCode { get; set; }
        }
        public class GroupManifestModel
        {
            public string manifestnum { get; set; }
            public string GroupWayBillNumber { get; set; }
            public List<Shipment> shipmentItem { get; set; }
            public List<ShipmentItem> shipmentItemList { get; set; }
            public List<Manifest> manifestinfo { get; set; }
            public DateTime manifestdate { get; set; }
            public string Driver { get; set; }
            public string DriverPhone { get; set; }
            public string Vehiclename { get; set; }
            public string destination { get; set; }

        }

        public class ShipmentPackagingModel
        {

            public int ID { get; set; }
            public string ShipmentId { get; set; }
            public string Createdby { get; set; }
            public int PackageType { get; set; }
            public DateTime CreationTime { get; set; }
            public decimal? PackagingFee { get; set; }

            public int? DepartureLocationId { get; set; }
            public int? DestinationId { get; set; }


        }

        public class ShipmentItemModel
        {
            public string wayBillNum { get; set; }
            public int Id { get; set; }
            public string ItemDesc { get; set; }
            public string Shpdestination { get; set; }
            public string Shpdeparture { get; set; }
            public string ItemType { get; set; }
            public string ItemName { get; set; }
            public decimal ItemWeight { get; set; }
            public string shItemCondition { get; set; }
            public decimal Price { get; set; }
            public int Quantity { get; set; }
            public int ShipID { get; set; }
            public DateTime? DateCreated { get; set; }
            public bool HasArrived { get; set; }
            public string senderphone { get; set; }
            public string senderName { get; set; }
            public string receiverPhone { get; set; }
            public string receiverName { get; set; }
            public string Groupnumber { get; set; }
            public decimal TotalToPay { get; set; }
            public bool? IsCredit { get; set; }
            public DateTime? CreditPaymentDate { get; set; }
            public decimal? GrandTotal { get; set; }
            public decimal? DeclearedValue { get; set; }
            public int ShipmentType { get; set; }
            public int ShipmentId { get; set; }
            public string PaymentMethod { get; set; }
            public System.Nullable<int> ItemCategoryId { get; set; }
            public string ItemCategoryDescription { get; set; }
            public System.Nullable<int> SpecialPackagePricingId { get; set; }
            public string ItemGuid { get; set; }
        }

        public class ComplaintModel
        {
            public int Id { get; set; }

            public string FullName { get; set; }

            public string Email { get; set; }

            public int ComplaintType { get; set; }

            public int PriorityLevel { get; set; }

            public string WayBillNumber { get; set; }

            public string Message { get; set; }

            public System.DateTime CreationTime { get; set; }

            public System.Nullable<long> CreatorUserId { get; set; }

            public System.Nullable<long> DeleterUserId { get; set; }

            public System.Nullable<System.DateTime> DeletionTime { get; set; }

            public bool IsDeleted { get; set; }

            public System.Nullable<System.DateTime> LastModificationTime { get; set; }

            public System.Nullable<long> LastModifierUserId { get; set; }

            public System.DateTime TransDate { get; set; }

            public string RepliedMessage { get; set; }

            public bool Responded { get; set; }
        }
        public class CouponModel
        {
            public System.Guid Id { get; set; }
            public System.DateTime CreationTime { get; set; }
            public string CreatorUserId { get; set; }
            public System.Nullable<System.DateTime> LastModificationTime { get; set; }
            public string LastModifierUserId { get; set; }
            public string CouponCode { get; set; }
            public decimal CouponValue { get; set; }
            public int CouponType { get; set; }
            public bool Validity { get; set; }
            public int DurationType { get; set; }
            public int Duration { get; set; }
            public string PhoneNumber { get; set; }
            public System.Nullable<System.DateTime> DateUsed { get; set; }
            public bool IsUsed { get; set; }
            public string VoucherNote { get; set; }
            public System.Nullable<int> VoucherType { get; set; }
            public System.Nullable<System.Decimal> CouponValueLimit { get; set; }
            public System.Nullable<System.DateTime> StartDate { get; set; }
            public System.Nullable<System.DateTime> EndDate { get; set; }
            public System.Nullable<int> TotalUsage { get; set; }
        }


        public class CouponManagementModel
        {
            public int ID { get; set; }

            public string CouponUserId { get; set; }

            public string CouponCode { get; set; }

            public System.Nullable<System.DateTime> UsedDate { get; set; }

            public PlatformType PlatformType { get; set; } = PlatformType.web;

            public string WayBill { get; set; }

            public bool IsDeleted { get; set; }

        }
        public class ReferralsModel
        {
            public long Id { get; set; }
            public System.DateTime CreationTime { get; set; }
            public System.Nullable<long> CreatorUserId { get; set; }
            public System.Nullable<System.DateTime> LastModificationTime { get; set; }

            public System.Nullable<long> LastModifierUserId { get; set; }

            public bool IsDeleted { get; set; }

            public System.Nullable<long> DeleterUserId { get; set; }

            public System.Nullable<System.DateTime> DeletionTime { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public int UserType { get; set; }

            public string ReferralCode { get; set; }
        }
        public class CustomerGoodsPickupResponse
        {
            public decimal Price { get; set; }//the cost for coming to pick goods from the customer's place
        }
        public class CustomerGoodsPickupRequest
        {
            public double PickupDistance { get; set; }
            public double DropOffDistance { get; set; }
            public string PickupStateName { get; set; }
            public string DropOffStateName { get; set; }
            public VehicleType VehicleType { get; set; }
            public DispatchType DispatchType { get; set; }
        }
        public class MerchantGoodsPickupResponse
        {
            public decimal Price { get; set; }//the cost for coming to pick goods from the merchant's place
        }
        public class MerchantGoodsPickupRequest
        {
            public string MerchantUserName { get; set; }
            public double PickupDistance { get; set; }
            public double DropOffDistance { get; set; }
            public string PickupStateName { get; set; }
            public string DropOffStateName { get; set; }
            public MerchantPriceType PriceType { get; set; }
            public int? WeightRangeId { get; set; }
            public DispatchType DispatchType { get; set; }
        }
        public class QuotationModel
        {
            public int Id { get; set; }
            public string PickupAddress { get; set; }
            public string DestinationAddress { get; set; }
            public DateTime? PickUpDateTime { get; set; }
            public string PackageDescription { get; set; }
            public int? NumberOfItems { get; set; }
            public double? Weight { get; set; }
            public string Comment { get; set; }
            public VehicleType? VehicleType { get; set; }
            public int? NumberOfVehicles { get; set; }
            public string Name { get; set; }
            public string PhoneNumber { get; set; }
            public string EmailAddress { get; set; }
            public DateTime? DateCreated { get; set; }
            public string CreatedBy { get; set; }
            public bool IsDeleted { get; set; }


            public static implicit operator QuotationModel(Quotation Quotation)
            {
                if (Quotation == null) { return null; }
                var data = new QuotationModel
                {
                    Comment = Quotation.Comment,
                    CreatedBy = Quotation.CreatedBy,
                    DateCreated = Quotation.DateCreated,
                    DestinationAddress = Quotation.DestinationAddress,
                    EmailAddress = Quotation.EmailAddress,
                    Id = Quotation.Id,
                    IsDeleted = Quotation.IsDeleted,
                    Name = Quotation.Name,
                    NumberOfItems = Quotation.NumberOfItems,
                    NumberOfVehicles = Quotation.NumberOfVehicles,
                    PackageDescription = Quotation.PackageDescription,
                    PhoneNumber = Quotation.PhoneNumber,
                    PickupAddress = Quotation.PickupAddress,
                    PickUpDateTime = Quotation.PickUpDateTime,
                    VehicleType = (VehicleType?)Quotation.VehicleType,
                    Weight = Quotation.Weight
                };
                return data;
            }


        }
        public class ZoneModel
        {
            public int id { get; set; }

            public string name { get; set; }

            public bool? isActive { get; set; }

            public System.DateTime DateCreated { get; set; }

            public System.Nullable<System.DateTime> DateModified { get; set; }
            public string ZoneDescription { get; set; }

            public System.Nullable<decimal> ZonePrice { get; set; }

            public bool IsDeleted { get; set; }

            public static implicit operator ZoneModel(Zone model)
            {
                if (model == null) { return null; }
                var data = new ZoneModel
                {
                    DateCreated = model.DateCreated,
                    DateModified = model.DateModified,
                    id = model.Id,
                    isActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    name = model.Name,
                    ZoneDescription = model.ZoneDescription,
                    ZonePrice = model.ZonePrice
                };
                return data;
            }
        }

        public class ShipmentParcelModel
        {
            #region entity properties
            public int Id { get; set; }

            public string CreatedBy { get; set; }

            public System.Nullable<System.DateTime> CreatedDate { get; set; }

            public string Waybill { get; set; }

            public System.Nullable<int> AccountType { get; set; }

            public System.Nullable<bool> IsPickedUp { get; set; }

            public string IsPickUpBy { get; set; }

            public System.Nullable<System.DateTime> IsPickedDate { get; set; }

            public string IsPickedUpFrom { get; set; }

            public System.Nullable<bool> IsAssigned { get; set; }

            public string IsAssignedBy { get; set; }

            public string ParcelAssignedTo { get; set; }

            public string ParcelStatus { get; set; }

            public System.Nullable<bool> IsDroppedOff { get; set; }

            public string IsDroppedOffBy { get; set; }

            public System.Nullable<System.DateTime> IsDroppedOffDate { get; set; }

            public string ParcelDropTo { get; set; }
            public DateTime? AssignedDate { get; set; }
            public string Vehicle { get; set; }
            public System.Nullable<bool> IsAccepted { get; set; }
            public string AcceptedBy { get; set; }
            public System.Nullable<System.DateTime> AcceptedDate { get; set; }

            public System.Nullable<decimal> DispatchRiderCommission { get; set; }

            public System.Nullable<float> PickUpLatitude { get; set; }

            public System.Nullable<float> PickUpLongitude { get; set; }

            public System.Nullable<float> DropOffLatitude { get; set; }

            public System.Nullable<float> DropOffLongitude { get; set; }

            public System.Nullable<bool> IsInterstate { get; set; }
            public string SenderEmail { get; set; }
            public string ParcelDropOffTerminal { get; set; }
            public int PaymentStatus { get; set; }
            public string SenderActualAddress { get; set; }
            public string ReceiverActualAddress { get; set; }
            public string SenderPhoneNumber { get; set; }
            public string ReceiverPhoneNumber { get; set; }
            public System.Nullable<decimal> TotalPay { get; set; }

            public bool IsHomeDelivery { get; set; }

            #endregion

            #region model properties

            #endregion

            public static implicit operator ShipmentParcelModel(ShipmentParcel model)
            {
                if (model == null) { return null; }
                var parcel = new ShipmentParcelModel()
                {
                    Waybill = model.Waybill,
                    AccountType = model.AccountType,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    Id = model.Id,
                    IsAssigned = model.IsAssigned,
                    IsAssignedBy = model.IsAssignedBy,
                    IsDroppedOff = model.IsDroppedOff,
                    IsDroppedOffBy = model.IsDroppedOffBy,
                    IsDroppedOffDate = model.IsDroppedOffDate,
                    IsPickedDate = model.IsPickedDate,
                    IsPickedUp = model.IsPickedUp,
                    IsPickedUpFrom = model.IsPickedUpFrom,
                    IsPickUpBy = model.IsPickUpBy,
                    ParcelAssignedTo = model.ParcelAssignedTo,
                    ParcelDropTo = model.ParcelDropTo,
                    ParcelStatus = model.ParcelStatus,
                    AssignedDate = model.AssignedDate,
                    Vehicle = model.Vehicle,
                    IsAccepted = model.IsAccepted,
                    AcceptedBy = model.AcceptedBy,
                    AcceptedDate = model.AcceptedDate,//22
                    DispatchRiderCommission = model.DispatchRiderCommission,
                    DropOffLatitude = model.DropOffLatitude,
                    DropOffLongitude = model.DropOffLongitude,
                    IsInterstate = model.IsInterstate,
                    PickUpLatitude = model.PickUpLatitude,
                    PickUpLongitude = model.PickUpLongitude,
                    SenderEmail = model.SenderEmail,
                    ParcelDropOffTerminal = model.ParcelDropOffTerminal,

                };
                return parcel;
            }
        }
        public class VehiclesListModel
        {
            #region entity properties
            public int id { get; set; }

            public string regNumber { get; set; }

            public string EngineNumber { get; set; }

            public bool isActive { get; set; }

            public int modelId { get; set; }

            public System.DateTime DateCreated { get; set; }

            public System.DateTime DateModified { get; set; }

            public bool IsDeleted { get; set; }

            public string VehicleStatus { get; set; }

            public int CompanyInfoId { get; set; }
            #endregion

            #region model properies
            public string ModelName { get; set; }
            #endregion
            public static implicit operator VehiclesListModel(Vehicle model)
            {
                if (model == null) { return null; }
                VehiclesListModel data = new VehiclesListModel
                {
                    DateCreated = model.DateCreated,
                    DateModified = model.DateModified,
                    EngineNumber = model.EngineNumber,
                    id = model.Id,
                    isActive = model.IsActive,
                    IsDeleted = model.IsDeleted,
                    modelId = model.ModelId,
                    regNumber = model.RegNumber,
                    VehicleStatus = model.VehicleStatus,
                    CompanyInfoId = (int)model.CompanyInfoId,
                    ModelName = null,
                };
                return data;
            }
        }
        public class PriceCalculatorModel
        {
            #region entity properties 
            public int Id { get; set; }

            public string CreatedBy { get; set; }

            public System.Nullable<System.DateTime> CreatedDate { get; set; }

            public string State { get; set; }

            public System.Nullable<decimal> PriceTDRforBike { get; set; }

            public System.Nullable<decimal> PriceTDRforVan { get; set; }

            public System.Nullable<decimal> PriceTDRforTruck { get; set; }

            public System.Nullable<bool> IsDefaultCost { get; set; }

            public System.Nullable<decimal> DefaultBikePrice { get; set; }

            public System.Nullable<decimal> DefaultVanPrice { get; set; }

            public System.Nullable<decimal> DefaultTruckPrice { get; set; }
            public System.Nullable<decimal> CommissionforBike { get; set; }

            public System.Nullable<decimal> CommissionforVan { get; set; }

            public System.Nullable<decimal> CommissionforTruck { get; set; }
            public System.Nullable<decimal> MinimumInKm { get; set; }

            public System.Nullable<decimal> MaximumInKm { get; set; }

            public System.Nullable<decimal> MinimumInValue { get; set; }

            public System.Nullable<decimal> MaximumInValue { get; set; }

            public System.Nullable<decimal> MinimumPickUpCost { get; set; }

            public System.Nullable<decimal> MaximumPickUpCost { get; set; }
            //public System.Nullable<decimal> TerminalPriceForBike { get; set; }
            //public System.Nullable<decimal> TerminalPriceForVan { get; set; }
            public System.Nullable<decimal> TerminalPriceForTruck { get; set; }
            //public System.Nullable<decimal> TerminalPriceForBikeIntra { get; set; }
            //public System.Nullable<decimal> TerminalPriceForVanIntra { get; set; }
            //public System.Nullable<decimal> TerminalPriceForTruckIntra { get; set; }
            #endregion

            #region model properties

            #endregion

            public static implicit operator PriceCalculatorModel(PriceCalculator model)
            {
                if (model == null) { return null; }
                PriceCalculatorModel response = new PriceCalculatorModel
                {
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    DefaultBikePrice = model.DefaultBikePrice,
                    DefaultTruckPrice = model.DefaultTruckPrice,
                    DefaultVanPrice = model.DefaultVanPrice,
                    Id = model.Id,
                    IsDefaultCost = model.IsDefaultCost,
                    PriceTDRforBike = model.PriceTDRforBike,
                    PriceTDRforTruck = model.PriceTDRforTruck,
                    PriceTDRforVan = model.PriceTDRforVan,
                    State = model.State,
                    CommissionforBike = model.CommissionforBike,
                    CommissionforTruck = model.CommissionforTruck,
                    CommissionforVan = model.CommissionforVan,
                    MaximumInKm = model.MaximumInKm,
                    MaximumInValue = model.MaximumInValue,
                    MaximumPickUpCost = model.MaximumPickUpCost,
                    MinimumInKm = model.MinimumInKm,
                    MinimumInValue = model.MinimumInValue,
                    MinimumPickUpCost = model.MinimumPickUpCost,
                    //TerminalPriceForBike = model.TerminalPriceForBike,
                    //TerminalPriceForVan = model.TerminalPriceForVan,
                    //TerminalPriceForTruck = model.TerminalPriceForTruck,
                    //TerminalPriceForBikeIntra =model.TerminalPriceForBikeIntra,
                    //TerminalPriceForVanIntra = model.TerminalPriceForVanIntra,
                    //TerminalPriceForTruckIntra = model.TerminalPriceForTruckIntra

                };
                return response;
            }
        }

        public class FareCalendarModel
        {
            #region entity properties
            public int Id { get; set; }

            public string CreatedBy { get; set; }

            public System.Nullable<System.DateTime> CreatedDate { get; set; }

            public System.Nullable<int> AccountType { get; set; }

            public System.Nullable<int> ItemCostType { get; set; }

            public System.Nullable<int> PriceParameterType { get; set; }

            public System.Nullable<int> ConvertRate { get; set; }

            public System.Nullable<bool> IsZone { get; set; }

            public string ZoneDescription { get; set; }

            public string ParcelValue { get; set; }

            public System.Nullable<bool> IsActive { get; set; }

            public string DiscountReason { get; set; }

            public System.Nullable<System.DateTime> DateFrom { get; set; }

            public System.Nullable<System.DateTime> DateTo { get; set; }

            public System.Nullable<bool> Monday { get; set; }

            public System.Nullable<bool> Tuesday { get; set; }

            public System.Nullable<bool> Wednesday { get; set; }

            public System.Nullable<bool> Thursday { get; set; }

            public System.Nullable<bool> Friday { get; set; }

            public System.Nullable<bool> Saturday { get; set; }

            public System.Nullable<bool> Sunday { get; set; }
            #endregion

            #region model properties
            //public string AccountTypeName { get; set; }
            //public string ConvertRateName { get; set; }
            public System.Nullable<bool> IsBikeable;

            public System.Nullable<bool> IsVanable;

            public System.Nullable<bool> IsTruckable;
            #endregion
            public static implicit operator FareCalendarModel(FareCalendar model)
            {
                if (model == null) { return null; }
                var resp = new FareCalendarModel
                {
                    AccountType = model.AccountType,
                    ConvertRate = model.ConvertRate,
                    CreatedBy = model.CreatedBy,
                    CreatedDate = model.CreatedDate,
                    DateFrom = model.DateFrom,
                    DateTo = model.DateTo,
                    DiscountReason = model.DiscountReason,
                    Friday = model.Friday,
                    Id = model.Id,
                    IsActive = model.IsActive,
                    IsZone = model.IsZone,
                    ItemCostType = model.ItemCostType,
                    Monday = model.Monday,
                    ParcelValue = model.ParcelValue,
                    PriceParameterType = model.PriceParameterType,
                    Saturday = model.Saturday,
                    Sunday = model.Sunday,
                    Thursday = model.Thursday,
                    Tuesday = model.Tuesday,
                    Wednesday = model.Wednesday,
                    ZoneDescription = model.ZoneDescription,
                    IsBikeable = model.IsBikeable,
                    IsVanable = model.IsVanable,
                    IsTruckable = model.IsTruckable,

                };
                return resp;
            }
        }

        public class Constant
        {
            public const string AUTHORIZATION_ERROR = "Unauthorized";
            public const string USER_OR_PASSWORD_ERROR = "Invalid Username or Password";
            #region Dispatch Rider Configuration For Firebase
            public const string FCM_Express_Dispatch_Server_Key = "AAAA5WN-8eA:APA91bHK1LKIibbKsoELnzn5cXZqCI94sX92fBhIHYcZuxKEuFgzuU8FJiqo6JoEoD5MKgy3q35DuckXJwCtkOEtYUPXBJMBSpadRIv0Uav9-yXoLKyv43ZEWWfuLBvhqGjLabK6X488";
            public const string FCM_Express_Dispatch_Sender_Id = "985216774624";
            public const string FCM_Logistics_Express_Server_Key = "AAAAMGmxDgM:APA91bH2vshdjYA50rCnQxlkcy7xi0BbFoNbpuvccMrjuefj1rw0RtBMqt9CTf2NYCZFu4MJf9f7I4hUdD-UQhc18IB-lrvRmdZBxmfqDiTTcaDboNszslX8ml5MGB-s0aDO-MtM_01-";
            public const string FCM_Logistics_Express_Sender_Id = "207931641347";
            #endregion
            // public const string GeocodingAPI = "AIzaSyAjN-B802v815bzoSkjhwHFimBFvZh6FxA";
            public const string GeocodingAPI = "AIzaSyCp8GGOCVFqHGWoqVEPdXXrpZ1lVYXIJ8M";
        }
        public class ShipmentItemCategoryModel
        {
            public int Id { get; set; }

            public string ItemCategoryCode { get; set; }

            public string ItemCategoryDescription { get; set; }
            public decimal? PriceValue { get; set; }
            public System.Nullable<decimal> MinWeight { get; set; }

            public System.Nullable<decimal> MaxWeight { get; set; }

            public System.Nullable<decimal> MinValue { get; set; }

            public System.Nullable<decimal> MaxValue { get; set; }
        }
        public class FirebaseCloudMessagingModel
        {
            public string DeviceId { get; set; }
            public DeviceType DeviceType { get; set; }
        }
        public class FCMPushNotificationResponseModel
        {
            public bool Successful
            {
                get;
                set;
            }
            public string Response
            {
                get;
                set;
            }
            public Exception Error
            {
                get;
                set;
            }
            public HttpStatusCode StatusCode { get; set; }
        }
        public class FCMPushNotificationRequestModel
        {
            public string Title { get; set; }
            public string Message { get; set; }
            public string DeviceId { get; set; }
            public string ServerKey { get; set; }
            public string SenderId { get; set; }
            //additions

        }
        public class DiscountTypeModel
        {
            public int Id { get; set; }

            public string Description { get; set; }

            public string Code { get; set; }

            public string Name { get; set; }
        }
        public class DiscountModel
        {
            #region entity model
            public int Id { get; set; }

            public System.Nullable<int> DiscountType { get; set; }

            public System.Nullable<bool> Bikeable { get; set; }

            public System.Nullable<bool> Vanable { get; set; }

            public System.Nullable<bool> Truckable { get; set; }

            public System.Nullable<bool> IsActive { get; set; }

            public System.Nullable<System.DateTime> DateFrom { get; set; }

            public System.Nullable<System.DateTime> DateTo { get; set; }

            public System.Nullable<int> UserType { get; set; }

            public System.Nullable<int> ReoccurCount { get; set; }

            public System.Nullable<int> AccountType { get; set; }

            public System.Nullable<int> DiscountValueType { get; set; }

            public System.Nullable<decimal> ValueAmount { get; set; }

            public System.Nullable<int> ReferralCodePerHeadCount { get; set; }
            #endregion

            #region 
            public string Name { get; set; }
            #endregion
        }
        public class VolumeRangeModel
        {
            public int Id { get; set; }

            public System.Nullable<int> LowerRange { get; set; }

            public System.Nullable<int> UpperRange { get; set; }

            public string Description { get; set; }
            public string FormattedName { get; set; }

            public string Code { get; set; }

            public System.Nullable<int> CustomerType { get; set; }
            public DateTime? DateCreated { get; set; }
            public System.Nullable<System.DateTime> DateModified { get; set; }
            public bool? isDeleted { get; set; }
            public bool? isActive { get; set; }

        }

        public class WeightRangeModel
        {
            public int Id { get; set; }

            public System.Nullable<decimal> LowerRange { get; set; }

            public System.Nullable<decimal> UpperRange { get; set; }
            public string FormattedName { get; set; }

            public string Code { get; set; }

            public DateTime? DateCreated { get; set; }
            public System.Nullable<System.DateTime> DateModified { get; set; }
            public bool? isDeleted { get; set; }
            public bool? isActive { get; set; }

        }
        public class EscalationReportModel
        {
            public string WaybillNumber { get; set; }
            public DateTime? DateCreated { get; set; }
            public DateTime? DateDeparted { get; set; }
            public DateTime? DateArrived { get; set; }
            public string DestinationLocation { get; set; }
            public int? DateDiscripacy { get; set; }
            public string WaybillStatus { get; set; }
            public string EscalationActionType { get; set; }
            public string ReceiverNumber { get; set; }
            public string CollectionStatus { get; set; }
            public int? Demurrage { get; set; }
            public decimal? DemurrageAmount { get; set; }
        }
        public class AspNetUserModel
        {
            public string Id { get; set; }

            public string Email { get; set; }

            public bool EmailConfirmed { get; set; }

            public string PasswordHash { get; set; }

            public string SecurityStamp { get; set; }

            public string PhoneNumber { get; set; }

            public bool PhoneNumberConfirmed { get; set; }

            public bool TwoFactorEnabled { get; set; }

            public System.Nullable<System.DateTime> LockoutEndDateUtc { get; set; }

            public bool LockoutEnabled { get; set; }

            public int AccessFailedCount { get; set; }

            public string UserName { get; set; }

            public System.Nullable<int> AccountType { get; set; }

            public System.Nullable<int> UserType { get; set; }

            public string DispatchExpressDeviceId { get; set; }

            public string LogisticsExpressDeviceId { get; set; }

            public System.Nullable<bool> IsOnboardingDiscountUsed { get; set; }

            public System.Nullable<int> TransactionalDiscount { get; set; }

            public System.Nullable<int> ReferralPoint { get; set; }
            public static implicit operator AspNetUserModel(AspNetUser model)
            {
                if (model == null) { return null; }
                AspNetUserModel resp = new AspNetUserModel
                {
                    AccessFailedCount = model.AccessFailedCount,
                    AccountType = model.AccountType,
                    DispatchExpressDeviceId = model.DispatchExpressDeviceId,
                    Email = model.Email,
                    EmailConfirmed = model.EmailConfirmed,
                    Id = model.Id,
                    IsOnboardingDiscountUsed = model.IsOnboardingDiscountUsed,
                    LockoutEnabled = model.LockoutEnabled,
                    LockoutEndDateUtc = model.LockoutEndDateUtc,
                    LogisticsExpressDeviceId = model.LogisticsExpressDeviceId,
                    PasswordHash = model.PasswordHash,
                    PhoneNumber = model.PhoneNumber,
                    PhoneNumberConfirmed = model.PhoneNumberConfirmed,
                    ReferralPoint = model.ReferralPoint,
                    SecurityStamp = model.SecurityStamp,
                    TransactionalDiscount = model.TransactionalDiscount,
                    TwoFactorEnabled = model.TwoFactorEnabled,
                    UserName = model.UserName,
                    UserType = model.UserType,

                };
                return resp;
            }
        }
        public class LibmotExpressConstants
        {
            public class ShipmentTrackingStatus
            {
                public const string ShipmentProcessing = "Shipment Processing";
                public const string InTransit = "In Transit";
                public const string AvailableForPickup = "Available for pickup";
                public const string AssignedToDispatchRider = "Assigned to Dispatch Rider";
                public const string Collected = "Collected";
            }

            public class KonnectAPIDetails
            {
                public const string AccountId = "CFQENebSSmp49mTsqO6feg==";
                public const string AuthKey = "bl3_gOt+bR_bw2qM288gW8wXS8Nw3+6JFz6T_EvCcso=";
                public const string SenderMask = "LIBMOEXPRES";
                public const string CountryCode = "234";

            }
        }
        public class SpecialPackagepricingModel
        {
            public int id { get; set; }

            public decimal weight { get; set; }

            public decimal price { get; set; }

            public string desription { get; set; }

            public int zoneId { get; set; }

            public int specialPackageId { get; set; }

            public bool isActive { get; set; }

            public System.Nullable<System.DateTime> DateCreated { get; set; }

            public System.Nullable<System.DateTime> DateModified { get; set; }

            public bool IsDeleted { get; set; }

            public System.Nullable<int> ShipmentItemCategoryId { get; set; }

            public System.Nullable<bool> IsBikeable { get; set; }

            public System.Nullable<bool> IsVanable { get; set; }

            public System.Nullable<bool> IsTruckable { get; set; }
        }

        public class MerchantShipmentDTO
        {

            public int MerchantID { get; set; }
            public decimal TotalGrandTotal { get; set; }
            public decimal TotalVat { get; set; }
            public decimal TotalInsured { get; set; }
            public List<ShipmentDTO> Shipments { get; set; }

        }

        public class ShipmentDTO
        {
            #region entity properties
            //entity properties
            public int Id { get; set; }
            public string Waybill { get; set; }
            public DateTime? DeliveryTime { get; set; }
            public int PaymentStatus { get; set; }
            public int? typeofCustomer { get; set; }
            public int? customerId { get; set; }
            public int? departureLocationId { get; set; }
            public int destinationId { get; set; }
            public string receiverName { get; set; }
            public string receiverPhoneNumber { get; set; }
            public string receiverEmail { get; set; }
            public string receiverAddress { get; set; }
            public int? receiverStateId { get; set; }
            public int? deliveryTypeId { get; set; }
            public string groupId { get; set; }
            public int? PickupOptions { get; set; }
            public string SpecifiedDateofDelivery { get; set; }
            public DateTime? expectedDateOfArrival { get; set; }
            public DateTime? actualArrivalDate { get; set; }
            public decimal? TotalWeight /*itemsWeight*/ { get; set; }
            public System.Nullable<decimal> TotalPrice /*grandTotal*/ { get; set; }
            public bool isCashOnDelivery { get; set; }
            public System.Nullable<decimal> cashOnDeliveryAmount { get; set; }
            public System.Nullable<decimal> expectedAmountToCollect { get; set; }
            public System.Nullable<decimal> actualAmountCollected { get; set; }
            public string createdBy { get; set; }
            public bool ValueIsDeceleared { get; set; }
            public decimal? deClearedValue { get; set; }
            public System.Nullable<decimal> discountAmountGiven { get; set; }
            public bool isInsured { get; set; }
            public decimal? vat { get; set; }
            public decimal? packagingfee { get; set; }
            public int? packagingQuantity { get; set; }
            public System.Nullable<decimal> insuranceAmount { get; set; }
            public decimal totalTopay { get; set; }
            public string paymentMethod { get; set; }
            public bool isCancelled { get; set; }
            public string description { get; set; }
            public string senderAddress { get; set; }
            public int senderStateId { get; set; }
            public bool HasArrived { get; set; }
            public DateTime DateCreated { get; set; }
            public System.Nullable<System.DateTime> DateModified { get; set; }
            public bool isDeleted { get; set; }
            public string specialnote { get; set; }
            public string BookingRefCode { get; set; }
            public System.Nullable<decimal> ItemQuantity { get; set; }
            public string PackagingType { get; set; }
            public int? PackageQuantity { get; set; }
            public System.Nullable<bool> IsRefund { get; set; }
            public System.Nullable<decimal> RefundAmount { get; set; }
            public System.Nullable<bool> IsMissing { get; set; }
            public string IsMissingStatus { get; set; }
            public System.Nullable<System.DateTime> IsMissingDate { get; set; }
            public System.Nullable<bool> IsCredit { get; set; }
            public System.Nullable<System.DateTime> CreditPaymentDate { get; set; }
            public string PayStackResponse { get; set; }
            public string PayStackWebhookReference { get; set; }
            public string PayStackReference { get; set; }
            public string UserId { get; set; }
            public int? AccountType { get; set; }
            public string TellerNumber { get; set; }
            public bool? IsVerified { get; set; }
            public decimal? VerifiedAmount { get; set; }
            public string senderEmail { get; set; }
            public string senderName { get; set; }
            public string senderphone { get; set; }
            public int? IsCollected { get; set; }
            public float? senderLat /*PickUpLatitude*/ { get; set; }
            public float? senderLng /*PickUpLongitude*/ { get; set; }
            public float? receiverLat /*DropOffLatitude*/ { get; set; }
            public float? receiverLng /*DropOffLongitude*/ { get; set; }
            public string SenderActualAddress { get; set; }
            public string ReceiverActualAddress { get; set; }
            public int? ShipmentStatus { get; set; }
            public string SenderFirstName { get; set; }
            public string SenderLastName { get; set; }
            public string ReceiverFirstName { get; set; }
            public string ReceiverLastName { get; set; }
            public string ReleasedBy { get; set; }
            public DateTime? ReleasedDate { get; set; }
            public System.Nullable<bool> IsTampered { get; set; }
            public System.Nullable<System.DateTime> IsTamperedDate { get; set; }
            public string IsTamperedStatus { get; set; }
            public string Signature { get; set; }
            public string SignatureName { get; set; }
            public string SignaturePhoneNumber { get; set; }

            #endregion


            #region non-entity properties
            //Other non-entity properties
            public string VehicleRegNumber { get; set; }
            public string CreditReceiversname { get; set; }
            public string CreditReceiversPhoneNo { get; set; }
            public string CreditReleaseby { get; set; }
            public string GroupNumber { get; set; }
            public string ManifestNumber { get; set; }
            public string CustomerType { get; set; }
            public string departureLocation { get; set; }
            public string destinationLocation { get; set; }
            public string destinationState { get; set; }
            public string departureState { get; set; }
            public string DeliveryType { get; set; }
            public string GrandTotalValue { get; set; }
            public bool Released { get; set; }
            public bool Collected { get; set; }
            public string DeclaredValue { get; set; }
            public bool? isReceived { get; set; }
            public DateTime? IsReceivedDate { get; set; }
            public PayStackPaymentResponseModel PayStackPaymentResponse { get; set; }
            public string destinationLocationAddress { get; set; }
            public string departureLocationAddress { get; set; }
            public int? ZoneId { get; set; }
            public List<ShipmentItemModel> Items { get; set; }

            #endregion
        }
        public class MerchantShipmentModel
        {
            public int ID { get; set; }
            public int MerchantID { get; set; }
            public decimal TotalGrandTotal { get; set; }
            public decimal TotalVat { get; set; }
            public decimal TotalInsured { get; set; }
            public DateTime DateCreated { get; set; }
            public bool IsDeleted { get; set; }



        }



        public class MerchantWeightRangePriceModel
        {
            public int Id { get; set; }
            public int MercahntId { get; set; }
            public int WeightRangeId { get; set; }
            public decimal? WeightPrice { get; set; }
            public DateTime DateCreated { get; set; }
        }

        public class MarketingCouponDTO
        {
            public Guid Id { get; set; }
            public string CouponCode { get; set; }
            public decimal CouponValue { get; set; }
            public int? TotalUsage { get; set; }
            public int? TotalLimit { get; set; }
        }
        public class MarketingCouponResponse
        {
            [JsonProperty("Code")]
            public string Code { get; set; }

            [JsonProperty("Coupon")]
            public MarketingCoupon Coupon { get; set; }

            public class MarketingCoupon
            {
                public string Code { get; set; }
                public Guid Id { get; set; }
                public string CouponCode { get; set; }
                public decimal CouponValue { get; set; }
                public int? TotalUsage { get; set; }
                public int? TotalLimit { get; set; }

            }
        }

        public class UpdateMarketingCouponDTO
        {
            public string CouponUserId { get; set; }
            public string CouponCode { get; set; }
            public AccountType PlatformType { get; set; }
            public string Waybill { get; set; }
        }

        public class WalletTerminalModel
        {
            public int id { get; set; }
            public string Name { get; set; }
            public decimal Amount { get; set; }
            public string PaymentMethod { get; set; }
            public DateTime DateCreated { get; set; }
        }

        public class PaymentModel
        {
            public bool Status { get; set; }
            public decimal? TransactionAmount { get; set; }
            public decimal? WalletBalance { get; set; }
            public DateTime? CreationDate { get; set; }
            public TransactionType TransactionType { get; set; }
            public string CreatedBy { get; set; }
            public string Response { get; set; }
        }

        public class WalletTransactionModel
        {
            //entity properties
            public Guid Id { get; set; }
            public string UserId { get; set; }
            public TransactionType TransactionType { get; set; }
            public decimal TransactionAmount { get; set; }
            public DateTime TransactionDate { get; set; }
            public decimal LineBalance { get; set; }
            public int WalletId { get; set; }
            public DateTime CreationTime { get; set; }
            public string TransactedBy { get; set; }
            public string TransactionDescription { get; set; }
            public string Reference { get; set; }
            public bool IsCompleted { get; set; }
            public string WayBill { get; set; }
            public string PayStackResponse { get; set; }
            public string PayStackWebhookReference { get; set; }
            public string PayStackReference { get; set; }
            public string paymentMethod { get; set; }

            //non- entity prop
            public string TransType { get; set; }
        }


        public class CustomerPhoneNumbersAndWayBill
        {
            public string SenderPhoneNumber { get; set; }
            public string ReceiverPhoneNumber { get; set; }
            public string WayBillNumber { get; set; }
        }

    }
}
