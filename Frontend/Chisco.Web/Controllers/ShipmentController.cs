using DataAccess.DataModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using static DataAccess.DataModels.SysModels;
using System.Collections.Specialized;

namespace Chisco.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : Controller
    {
        private readonly LibDataClass _libDataClass;
       // private readonly liblogisticsDataContext db = new liblogisticsDataContext();
        public ShipmentController()
        {
            //_libDataClass = new LibDataClass();
        }
        [HttpGet, HttpPost, AllowAnonymous, Route("LoadItemPricing")]
        public IActionResult LoadItemPricing(List<SpecialPackagepricingModel> entities)
        {
            //return Ok();
            foreach (var e in entities)
            {
                var d = (from spp in db.SpecialPackagepricings where spp.id == e.id select spp).FirstOrDefault();
                if (d != null)
                {
                    d.desription = e.desription;
                    d.price = e.price;
                    d.weight = e.weight;
                    d.zoneId = e.zoneId;
                    d.isActive = e.isActive;
                    d.ShipmentItemCategoryId = e.ShipmentItemCategoryId;
                    d.IsBikeable = e.IsBikeable;
                    d.IsVanable = e.IsVanable;
                    d.IsTruckable = e.IsTruckable;

                }
            }
            db.SubmitChanges();
            return Ok("zone A cleared");
        }
        #region Shipment APIs
        [HttpGet, Route("ItemConditions")]
        public IActionResult GetItemConditions()
        {//IList<itemConditionModel>
            return Ok(_libDataClass.GetItemConditions());
        }
        [HttpGet, Route("ShipmentItemCategory")]
        public IActionResult GetShipmentItemCategory()
        {
            var resp = (from sic in db.ShipmentItemCategories
                        select new ShipmentItemCategoryModel
                        {
                            Id = sic.Id,
                            ItemCategoryCode = sic.ItemCategoryCode,
                            ItemCategoryDescription = sic.ItemCategoryDescription,
                            PriceValue = sic.PriceValue,
                            MaxValue = sic.MaxValue,
                            MaxWeight = sic.MaxWeight,
                            MinValue = sic.MinValue,
                            MinWeight = sic.MinWeight,

                        }).ToList();
            return Ok(resp);
        }


        [HttpPost]
        [Route("GetOnboardingDiscount/{userId}")]
        public IActionResult GetDiscounts([FromBody] ShipmentModel value, string userId)
        {
            return Ok(_libDataClass.OnboardingDiscount(value, userId));
        }


        [HttpGet]
        [Route("GetRequiredFareCalendarForMobile/{accountType}")]
        [Route("GetRequiredFareCalendarForMobile/{accountType}/{vehicleType}")]
        public IActionResult GetRequiredFareCalendarForMobile(int accountType, int? vehicleType = null)
        {
            return Ok(_libDataClass.GetRequiredFareCalendarForMobile((AccountType)accountType, (VehicleType?)vehicleType));
        }
        [HttpGet, Route("GetVat")]
        public IActionResult GetVat()
        {
            return Ok(_libDataClass.GetVat());
        }
        [HttpGet, Route("GetInsuranceAmt")]
        public IActionResult GetInsuranceAmt()
        {
            return Ok(_libDataClass.GetInsuranceAmt());
        }
        [HttpPost, Route("CreateShipment")]
        public async Task<List<returnmsg>> CreateShipment([FromBody] ShipmentModel value)//create shipment
        {
            value.Waybill = _libDataClass.RandomDigits(10);

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.FindByName(User.Identity.Name) ?? userManager.FindByEmail(User.Identity.Name);

            value.UserId = user.Id;
            value.createdBy = User.Identity.Name;
            value.AccountType = value.AccountType == null ? 0 : value.AccountType;//default to android!
            var shipment = _libDataClass.createShipment(value);
            var shipmentid = _libDataClass.GetShipmentId(value.Waybill);
            foreach (var item in value.Items)
            {
                if (item == null) { continue; }
                item.ShipmentId = shipmentid;
                _libDataClass.CreateShipmentItem(item);
            }
            shipment.FirstOrDefault().code = value.Waybill;



            #region Send Dispatch Officer Email
            string dataFile = HostingEnvironment.ApplicationPhysicalPath + "messaging\\dispatch-email.html";
            var replacement = new StringDictionary
            {
                ["WayBillNumber"] = shipment.FirstOrDefault().code,
                ["RecieverName"] = value.receiverName,
                ["RecieverAddress"] = value.receiverAddress,
                ["RecieverPhone"] = value.receiverPhoneNumber,
            };

            string emailsubject = $"Libmot Express Waybill Dispatch.";

            // string myEmail = "igwubor@gmail.com";
            var mail = new System.Net.Mail(System.Configuration.ConfigurationManager.AppSettings["UserName"], emailsubject, System.Configuration.ConfigurationManager.AppSettings["DispatchOfficerEmail"])
            {
                BodyIsFile = true,
                BodyPath = dataFile
            };

            //var mail = new Mail(System.Configuration.ConfigurationManager.AppSettings["UserName"], emailsubject, myEmail)
            //{
            //    BodyIsFile = true,
            //    BodyPath = dataFile
            //};



            await SmtpEmailService.SendMailAsync(mail, replacement);
            #endregion
            return shipment;
        }

        [HttpPost, Route("CreateMerchantShipment")]
        public async Task<List<returnmsg>> CreateMerchantShipment([FromBody] MerchantShipmentDTO value)//create merchant shipment
        {

            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.FindByName(User.Identity.Name) ?? userManager.FindByEmail(User.Identity.Name);

            var merchantShipment = _libDataClass.createMerchantShipment(value);
            var merchantShipmentID = _libDataClass.GetMerchantShipmentId(value.MerchantID);

            foreach (var sh in value.Shipments)
            {
                sh.Waybill = _libDataClass.RandomDigits(10);
                sh.UserId = user.Id;
                sh.createdBy = User.Identity.Name;
                sh.AccountType = sh.AccountType == null ? 0 : sh.AccountType;//default to android!


                var spdata = new ShipmentModel
                {
                    Waybill = sh.Waybill,
                    senderphone = sh.senderphone,
                    PaymentStatus = sh.PaymentStatus,
                    typeofCustomer = sh.typeofCustomer,
                    customerId = sh.customerId,
                    departureLocationId = sh.departureLocationId,
                    destinationId = sh.destinationId,
                    receiverStateId = sh.receiverStateId,
                    receiverName = sh.receiverName,
                    receiverPhoneNumber = sh.receiverPhoneNumber,
                    receiverEmail = sh.receiverEmail,
                    receiverAddress = sh.receiverAddress,
                    deliveryTypeId = sh.deliveryTypeId,
                    PickupOptions = sh.PickupOptions,
                    SpecifiedDateofDelivery = sh.SpecifiedDateofDelivery,
                    TotalWeight = sh.TotalWeight,
                    TotalPrice = sh.TotalPrice,
                    isCashOnDelivery = sh.isCashOnDelivery,
                    createdBy = sh.createdBy,
                    ValueIsDeceleared = sh.ValueIsDeceleared,
                    deClearedValue = sh.deClearedValue,
                    isInsured = sh.isInsured,
                    vat = sh.vat,
                    insuranceAmount = sh.insuranceAmount,
                    packagingfee = sh.packagingfee,
                    totalTopay = sh.totalTopay,
                    paymentMethod = sh.paymentMethod,
                    description = sh.description,
                    senderAddress = sh.senderAddress,
                    senderStateId = sh.senderStateId,
                    DateCreated = sh.DateCreated,
                    specialnote = sh.specialnote,
                    BookingRefCode = sh.BookingRefCode,
                    PackagingType = sh.PackagingType,
                    packagingQuantity = sh.packagingQuantity,
                    UserId = sh.UserId,
                    AccountType = sh.AccountType,
                    senderLat = sh.senderLat,
                    senderLng = sh.senderLng,
                    receiverLat = sh.receiverLat,
                    receiverLng = sh.receiverLng,
                    senderEmail = sh.senderEmail,
                    SenderActualAddress = sh.SenderActualAddress,
                    ReceiverActualAddress = sh.ReceiverActualAddress,
                    senderName = sh.senderName,
                    SenderFirstName = sh.SenderFirstName,
                    SenderLastName = sh.SenderLastName,
                    ReceiverFirstName = sh.ReceiverFirstName,
                    ReceiverLastName = sh.ReceiverLastName,
                    MerchantShipmentID = merchantShipmentID
                };

                var shipment = _libDataClass.createShipment(spdata);
                var shipmentid = _libDataClass.GetShipmentId(sh.Waybill);
                foreach (var item in sh.Items)
                {
                    if (item == null) { continue; }
                    item.ShipmentId = shipmentid;
                    _libDataClass.CreateShipmentItem(item);
                }
                merchantShipment.FirstOrDefault().code = sh.Waybill.ToString();
            }


            return merchantShipment;
        }

        [HttpGet, Route("VehicleTypes")]
        public async Task<List<VehicleType>> GetVehicleTypes()
        {
            return _ = Enum.GetValues(typeof(VehicleType)).Cast<VehicleType>().ToList();
        }
        [HttpGet, Route("Itemconditions")]
        public async Task<List<itemcondition>> GetItemconditions()
        {
            return _ = Enum.GetValues(typeof(itemcondition)).Cast<itemcondition>().ToList();
        }
        [HttpGet, Route("GetSpecialShipmentPrices")]
        public async Task<IList<SpecialShipmentModel>> GetSpecialShipmentPrices()// Bulky,Dead Weight,FOOD STUFF,PASSENGER SHIPMENT,HEAVY ITEMS, etc.
        {
            return await Task.FromResult(_libDataClass.GetSpecialShipmentPrices());
        }
        [HttpGet, Route("ShipmentsByWayBill/{waybillnumber}")]
        public async Task<List<ShipmentModel>> GetShipmentsByWayBill(string waybillnumber)//Track Your Shipment (Get Shipment details / Shipment Item detail)
        //async Task<List<ShipmentsModel>>
        {
            return await Task.FromResult(_libDataClass.GetShipmentsByWayBill(waybillnumber));
        }
        [HttpGet, Route("GetShipmentHistory")]
        public async Task<List<ShipmentModel>> GetShipmentHistory(string userName)// per month
        {
            return await _libDataClass.GetShipmentHistory(userName);
        }
        //[HttpPost, Route("GetCustomerGoodsPickupDetails/{distance}/{stateName}/{vehicleType}")]
        [HttpPost, Route("GetCustomerGoodsPickupDetails")]
        public IActionResult GetCustomerGoodsPickupDetails(CustomerGoodsPickupRequest model)//CustomerGoodsPickupResponse
        //(double distance, string stateName, VehicleType vehicleType, DispatchType dispatchType = DispatchType.Pickup)
        {
            try
            {
                var resp = _libDataClass.GetDispatchPriceDetails(model);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet, Route("GetSpecialPackagePricesByZone")]
        public IActionResult GetSpecialPackagePricesByZone(double departureLatitude, double departureLongitude, double destinationLatitude, double destinationLongitude)
        {
            var resp = _libDataClass.GetSpecialPackagePricesByZone(departureLatitude, departureLongitude, destinationLatitude, destinationLongitude);
            return Ok(resp);
        }

        [HttpGet, Route("GetMobilePriceSettings/{stateName}")]
        public IActionResult GetMobilePriceSettings(string stateName)
        {
            var resp = _libDataClass.GetMobilePriceSetting(stateName);
            return Ok(resp);
        }

        [HttpPost, Route("AddQuotation")]
        public async Task<IActionResult> AddQuotation(QuotationModel model)
        {
            model.CreatedBy = User.Identity.Name;
            return Ok(_libDataClass.AddQuotation(model));
        }
        [HttpGet, Route("GetMerchantGoodsPickupDetails")]
        public IActionResult GetMerchantGoodsPickupDetails(MerchantGoodsPickupRequest model)
        {
            try
            {
                var resp = _libDataClass.GetMerchantDispatchPriceDetails(model);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion
    }
}
