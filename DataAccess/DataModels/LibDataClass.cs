using DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataAccess.DataModels.SysModels;

namespace DataAccess.DataModels
{
    public class LibDataClass
    {
        //  CultureInfo culture = new CultureInfo("en-US");

        private readonly AppDbContext _context;

        public LibDataClass(AppDbContext context)
        {
            _context = context;
        }


        Guid GenerateComb()
        {
            byte[] destinationArray = Guid.NewGuid().ToByteArray();
            DateTime time = new DateTime(0x76c, 1, 1);
            DateTime now = DateTime.Now;
            TimeSpan span = new TimeSpan(now.Ticks - time.Ticks);
            TimeSpan timeOfDay = now.TimeOfDay;
            byte[] bytes = BitConverter.GetBytes(span.Days);
            byte[] array = BitConverter.GetBytes((long)(timeOfDay.TotalMilliseconds / 3.333333));
            Array.Reverse(bytes);
            Array.Reverse(array);
            Array.Copy(bytes, bytes.Length - 2, destinationArray, destinationArray.Length - 6, 2);
            Array.Copy(array, array.Length - 4, destinationArray, destinationArray.Length - 4, 4);
            return new Guid(destinationArray);
        }
        //Dashboard services
        public decimal SalesbyLocation(int locationId)
        {

            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var statdata = (from s in _context.Shipments.ToList()
                            where s.DepartureLocationId == locationId && s.DateCreated >= startDateTime && s.DateCreated <= endDateTime && s.IsCancelled == false
                            select s.GrandTotal).ToList();
            var result = statdata.Sum();
            return result.Value;
        }

        #region Getting Total sales in a day
        public decimal AllTerminalSales()
        {

            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var cdata = (from s in _context.Shipments.ToList()
                         where s.DateCreated >= startDateTime && s.DateCreated <= endDateTime && s.IsCancelled == false
                         select s.GrandTotal).ToList();
            var result = cdata.Sum();
            return result.Value;
        }
        #endregion


        #region Get yesterday's sales
        public decimal AllTerminalSalesForYesterday()
        {
            DateTime today = DateTime.Today;//Today at 00:00:00
            DateTime yesterday = today.AddDays(-1);//Today at 23:59:59
            var cdata = (from s in _context.Shipments.ToList()
                         where s.DateCreated >= yesterday && s.DateCreated < today && s.IsCancelled == false
                         select s.GrandTotal).ToList();
            var result = cdata.Sum();
            return result.Value;
        }

        #endregion

        public int GetshipmentCountByLocationId(int locationId)
        {

            List<ShipmentItem> item = new List<ShipmentItem>();
            var dtcx = (from l in _context.Shipments.ToList()
                        where l.DestinationId == locationId && l.GroupId == null
                        select l.DepartureLocationId).Count();
            return dtcx;
        }

        #region DailyShipmentCount
        public int DailyShipmentCount()
        {
            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var cdata = (from s in _context.Shipments.ToList()
                         where s.DateCreated >= startDateTime && s.DateCreated <= endDateTime && s.IsCancelled == false
                         select s.Id).Count();
            return cdata;
        }

        #endregion

        #region Daily truck shipment activity
        public int GetTotalTruckDailyCount()
        {
            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var dtcx = (from mc in _context.Manifests.ToList()
                        join jc in _context.JourneyManagements.ToList() on mc.JourneyManagementId equals jc.Id
                        where jc.JourneyDate >= startDateTime && jc.JourneyDate <= endDateTime
                        select mc.ManifestId).ToList();
            return dtcx.Count();
        }
        #endregion

        #region Get all missing items
        public int GetMissingItemsDailyCount()
        {
            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var dtcx = (from mc in _context.Shipments.ToList()
                        where mc.IsMissingStatus == "Pending" && mc.IsMissing == true && mc.DateCreated >= startDateTime && mc.DateCreated <= endDateTime
                        select mc.Id).ToList();
            return dtcx.Count();
        }
        #endregion

        #region IncomingShipments
        public int GetDailyIncomingShipments(int locationId)
        {

            List<ShipmentItem> item = new List<ShipmentItem>();
            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var dtcx = (from l in _context.Shipments.ToList()
                        join dpId in _context.Locations.ToList() on l.DepartureLocationId equals dpId.Id
                        join dlType in _context.Deliverytypes.ToList() on l.DeliveryTypeId equals dlType.Id
                        join arrId in _context.Locations.ToList() on l.DestinationId equals arrId.Id
                        where l.DestinationId == locationId && l.DateCreated >= startDateTime && l.DateCreated <= endDateTime
                        select l.DepartureLocationId).Count();
            return dtcx;
        }

        #region GetTotalNumberOfMerchants
        public Int32 GetMerchantCount()
        {
            List<ShipmentItem> item = new List<ShipmentItem>();
            var dtcx = (from l in _context.MerchantSignups.ToList()
                        where l.IsApproved == true
                        select l.Id).ToList();
            return dtcx.Count();
        }

        #endregion

        public int GetDailyOutgoingShipments(int locationId)
        {
            List<ShipmentItem> item = new List<ShipmentItem>();
            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(1).AddTicks(-1);//Today at 23:59:59
            var dtcx = (from l in _context.Shipments.ToList()
                        join dpId in _context.Locations.ToList() on l.DepartureLocationId equals dpId.Id
                        join dlType in _context.Deliverytypes.ToList() on l.DeliveryTypeId equals dlType.Id
                        join arrId in _context.Locations.ToList() on l.DestinationId equals arrId.Id
                        join depstate in _context.States.ToList() on l.SenderStateId equals depstate.Id
                        join arrState in _context.States.ToList() on l.ReceiverStateId equals arrState.Id
                        where l.SenderStateId != 8 && l.DateCreated >= startDateTime && l.DateCreated <= endDateTime
                        select l.DepartureLocationId).Count();
            return dtcx;
        }
        #endregion


        public int GetDailyFleetCount()
        {
            //using (var db = new liblogisticsDataContext())
            //{
            //    DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            //    DateTime endDateTime = DateTime.Today.AddDays(3).AddTicks(-1);//Today at 23:59:59

            //    var fleetCount = (from manifest in db.Manifests
            //                      join vehicle in db.Vehicles on manifest.VehicleId equals vehicle.id
            //                      join driver in db.Drivers on manifest.DriverId equals driver.Id
            //                      join journeyManagement in db.JourneyManagements on manifest.JourneyManagementId equals journeyManagement.Id
            //                      join manifestMapping in db.ManifestMappings on manifest.ManifestId equals manifestMapping.ManifestId
            //                      join shipment in db.shipments on manifestMapping.GroupWaybillNumber equals shipment.groupId
            //                      join ldept in db.Locations on shipment.departureLocationId equals ldept.id
            //                      join larr in db.Locations on shipment.destinationId equals larr.id
            //                      where manifest.DateCreated >= startDateTime && manifest.DateCreated <= endDateTime select manifest.DriverId).Distinct();
            //    return fleetCount.Count();
            //}
            return 1;

        }


        public Int32 GetTodayShipmentCountByLocationId(int locationId)
        {

            List<ShipmentItem> item = new List<ShipmentItem>();
            var dtcx = (from l in _context.Shipments.ToList()
                        join dpId in _context.Locations.ToList() on l.DepartureLocationId equals dpId.Id
                        join arrId in _context.Locations.ToList() on l.DestinationId equals arrId.Id
                        where l.DestinationId == locationId && l.GroupId != null
                        && l.DateCreated == DateTime.Now
                        select l.DepartureLocationId).ToList();
            return dtcx.Count();

        }


        public int GetMissingItemCount()
        {
            //List<ShipmentItem> item = new List<ShipmentItem>();
            var dtcx = (from mc in _context.Shipments.ToList()
                        where mc.IsMissingStatus == "Pending" && mc.IsMissing == true
                        select mc.Id).ToList();
            return dtcx.Count();
        }

        public int GetItemNotCollectedCount()
        {

            //where date >= DateTime.Today.AddDays(-7) &&
            // date < DateTime.Today
            DateTime startDateTime = DateTime.Today.AddDays(-2);//Today at 00:00:
            DateTime endDateTime = DateTime.Today;//Today at 23:59:59
            var dtcx = (from nc in _context.Shipments.ToList()
                        where nc.IsCollected == 0 && nc.DateCreated >= startDateTime && nc.DateCreated < endDateTime
                        select nc.Id).ToList();
            return dtcx.Count();
        }

        public int GetManifestNotRecievedCount()
        {
            DateTime startDateTime = DateTime.Today;//Today at 00:00:00
            DateTime endDateTime = DateTime.Today.AddDays(4).AddTicks(-1);//Today at 23:59:59
            var dtcx = (from nr in _context.Manifests.ToList()
                        where nr.IsReceived == false && nr.DateCreated >= startDateTime && nr.DateCreated <= endDateTime
                        select nr.ManifestId).ToList();
            return dtcx.Count();
        }

        public int GetTotalTruckWeeklyCount()
        {
            //List<ShipmentItem> item = new List<ShipmentItem>();
            var dtcx = (from mc in _context.Manifests.ToList()
                        join
                        jc in _context.JourneyManagements.ToList()
                        on mc.JourneyManagementId equals jc.Id
                        where jc.JourneyDate >= DateTime.Now.AddDays(-7) && jc.JourneyDate < DateTime.Now
                        select mc.ManifestId).ToList();
            return dtcx.Count();
        }

        //End Dashboard services
        //public void MakeGridViewPrinterFriendly(GridView gridView)
        //{
        //    if (gridView.Rows.Count > 0)
        //    {
        //        gridView.UseAccessibleHeader = true;
        //        gridView.HeaderRow.TableSection = TableRowSection.TableHeader;
        //    }
        //}

        public string ConvertAmount(decimal amount)
        {
            var new_amount = $"{amount:N}";
            return new_amount;
        }
        public string ConvertAmountDouble(double amount)
        {
            var new_amount = $"{amount:N}";
            return new_amount;
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }
        public string RandomDigits(int length)
        {
            var random = new Random();
            string s = string.Empty;
            for (int i = 0; i < length; i++)
                s = String.Concat(s, random.Next(10).ToString());
            return s;
        }

        public string GenerateWaywillNumber(string locationName, string destLocationName)
        {

            var lcode = (from l in _context.Locations.ToList()
                         where l.LocationName == locationName
                         select l.Code).FirstOrDefault();
            var destlcode = (from l in _context.Locations.ToList()
                             where l.LocationName == destLocationName
                             select l.Code).FirstOrDefault();

            return lcode.ToUpper() + RandomDigits(6) + destlcode.ToUpper();

        }

        public bool ValidateWaybillNumber(string waybillNumber)
        {
            var statdata = (from s in _context.Shipments.ToList()
                            where s.Waybill == waybillNumber
                            select s.Waybill).ToList();
            if (statdata.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public bool ValidateGroupWaybillNumber(string waybillNumber)
        {
            var statdata = (from s in _context.GroupWayBillNumbers.ToList()
                            where s.GroupWaybillCode == waybillNumber
                            select s.GroupWaybillCode).ToList();
            if (statdata.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        //public bool ValidateManifestNumber(string ManifestNumber)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from s in db.Manifests
        //                        where s.ManifestNumber == ManifestNumber
        //                        select s.ManifestNumber).ToList();
        //        if (statdata.Count > 0)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}



        //public string GetGroupWayBillNumberByDesId(int destId)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from s in db.GroupWayBillNumbers
        //                        where s.departureId == destId
        //                        select s.GroupWaybillCode).FirstOrDefault();
        //        return statdata.ToString();
        //    }
        //}

        //public string GetGroupWayBillsByDesId(int destId)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from s in db.GroupWayBillNumbers
        //                        where s.arrivalId == destId
        //                        select s.GroupWaybillCode).FirstOrDefault();
        //        return statdata.ToString();
        //    }
        //}




        public string GetUserLocationName(string UserId)
        {

            var statdata = (from s in _context.Employees.ToList()
                            join m in _context.Locations.ToList() on s.LocationId equals m.Id
                            where s.UserId == UserId
                            select m.LocationName).FirstOrDefault();
            return statdata;
        }

        public string GetLocationName(int? LocationId)
        {
            var statdata = (from m in _context.Locations.ToList()
                            where m.Id == LocationId
                            select m.LocationName).FirstOrDefault();
            return statdata;

        }

        public string GetLocationCode(int? LocationId)
        {
            var statdata = (from m in _context.Locations.ToList()
                            where m.Id == LocationId
                            select m.Code).FirstOrDefault();
            return statdata;

        }

        public int GetUserLocationId(string UserId)
        {

            var statdata = (from s in _context.Employees.ToList()
                            where s.UserId == UserId
                            select s.LocationId).FirstOrDefault();
            return statdata;
        }

        public int GetUserStateId(string UserId)
        {

            var stateId = (from e in _context.Employees.ToList()
                           join l in _context.Locations.ToList() on e.LocationId equals l.Id
                           where e.UserId == UserId
                           select l.StateId).FirstOrDefault();
            return stateId;

        }

        public int GetUserStateId(int LocationId)
        {

            var stateId = (from l in _context.Locations.ToList()
                           where l.Id == LocationId
                           select l.StateId).FirstOrDefault();

            return stateId;

        }
        public string GetUserStateName(int stateId)
        {

            string stateName = (from s in _context.States.ToList()
                                where s.Instate == true
                                && s.Id == stateId
                                select s.Name).FirstOrDefault();

            return stateName;

        }

        //public PriceCalculatorModel GetPriceCalculatorIntraState(string name)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var libata = (from p in db.PriceCalculators
        //                      where p.State == name
        //                      select new PriceCalculatorModel
        //                      {
        //                          Id = p.Id,
        //                          CreatedDate = p.CreatedDate,
        //                          State = p.State,
        //                          PriceTDRforBike = p.PriceTDRforBike,
        //                          PriceTDRforVan = p.PriceTDRforVan,
        //                          PriceTDRforTruck = p.PriceTDRforTruck,
        //                          IsDefaultCost = p.IsDefaultCost,
        //                          DefaultBikePrice = p.DefaultBikePrice,
        //                          DefaultVanPrice = p.DefaultVanPrice,
        //                          DefaultTruckPrice = p.DefaultTruckPrice,
        //                          CommissionforBike = p.CommissionforBike,
        //                          CommissionforVan = p.CommissionforVan,
        //                          CommissionforTruck = p.CommissionforTruck,
        //                          MinimumInKm = p.MinimumInKm,
        //                          MaximumInKm = p.MaximumInKm,
        //                          MinimumInValue = p.MinimumInValue,
        //                          MaximumInValue = p.MaximumInValue,
        //                          MinimumPickUpCost = p.MinimumPickUpCost,
        //                          TerminalPriceForBike = p.TerminalPriceForBike,
        //                          TerminalPriceForVan = p.TerminalPriceForVan,
        //                          TerminalPriceForTruck = p.TerminalPriceForTruck,
        //                          TerminalPriceForBikeIntra = p.TerminalPriceForBikeIntra,
        //                          TerminalPriceForVanIntra = p.TerminalPriceForVanIntra,
        //                          TerminalPriceForTruckIntra = p.TerminalPriceForTruckIntra,
        //                      }).FirstOrDefault();


        //        return libata;



        //    }
        //}

        public PriceCalculatorModel GetPriceCalculatorForTerminals()
        {

            var libata = (from p in _context.PriceCalculators.ToList()
                          where p.State == "Lagos"
                          select new PriceCalculatorModel
                          {
                              Id = p.Id,
                              CreatedDate = p.CreatedDate,
                              State = p.State,
                              PriceTDRforBike = p.PriceTdrforBike,
                              PriceTDRforVan = p.PriceTdrforVan,
                              PriceTDRforTruck = p.PriceTdrforTruck,
                              IsDefaultCost = p.IsDefaultCost,
                              DefaultBikePrice = p.DefaultBikePrice,
                              DefaultVanPrice = p.DefaultVanPrice,
                              DefaultTruckPrice = p.DefaultTruckPrice,
                              CommissionforBike = p.CommissionforBike,
                              CommissionforVan = p.CommissionforVan,
                              CommissionforTruck = p.CommissionforTruck,
                              MinimumInKm = p.MinimumInKm,
                              MaximumInKm = p.MaximumInKm,
                              MinimumInValue = p.MinimumInValue,
                              MaximumInValue = p.MaximumInValue,
                              MinimumPickUpCost = p.MinimumPickUpCost,
                              TerminalPriceForTruck = p.TerminalPriceForTruck,
                              //TerminalPriceForBike = p.TerminalPriceForBike,
                              //TerminalPriceForVan = p.TerminalPriceForVan,
                              //TerminalPriceForBikeIntra = p.TerminalPriceForBikeIntra,
                              //TerminalPriceForVanIntra = p.TerminalPriceForVanIntra,
                              //TerminalPriceForTruckIntra = p.TerminalPriceForTruckIntra,
                          }).FirstOrDefault();


            return libata;

        }



        public PriceCalculatorModel GetPriceCalculator()
        {
            var libata = (from p in _context.PriceCalculators.ToList()
                          where p.IsDefaultCost != false
                          select new PriceCalculatorModel
                          {
                              Id = p.Id,
                              CreatedDate = p.CreatedDate,
                              State = p.State,
                              PriceTDRforBike = p.PriceTdrforBike,
                              PriceTDRforVan = p.PriceTdrforVan,
                              PriceTDRforTruck = p.PriceTdrforTruck,
                              IsDefaultCost = p.IsDefaultCost,
                              DefaultBikePrice = p.DefaultBikePrice,
                              DefaultVanPrice = p.DefaultVanPrice,
                              DefaultTruckPrice = p.DefaultTruckPrice,
                              CommissionforBike = p.CommissionforBike,
                              CommissionforVan = p.CommissionforVan,
                              CommissionforTruck = p.CommissionforTruck,
                              MinimumInKm = p.MinimumInKm,
                              MaximumInKm = p.MaximumInKm,
                              MinimumInValue = p.MinimumInValue,
                              MaximumInValue = p.MaximumInValue,
                              MinimumPickUpCost = p.MinimumPickUpCost,
                              MaximumPickUpCost = p.MaximumPickUpCost
                          }).FirstOrDefault();


            return libata;
        }
        //public int GetUserStateId(int LocationId)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from l in db.Locations
        //                        join s in db.States on l.stateId equals s.id
        //                        where l.id == LocationId
        //                        select s.id).FirstOrDefault();
        //        return statdata;
        //    }
        //}
        //public int GetUserHubId(int LocationId)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from l in db.LocationHubs
        //                        where l.LocationId == LocationId
        //                        select l.HubId).FirstOrDefault();
        //        return statdata;
        //    }
        //}
        public int GetUserHubIdByLocation(int LocationId)
        {
            var statdata = (from l in _context.LocationHubs.ToList()
                            where l.LocationId == LocationId
                            select l.HubId).FirstOrDefault();
            return statdata;
        }
        public string GetLocationAddress(int LocationId)
        {
            var locationAddress = (from l in _context.Locations.ToList()
                                   where l.Id == LocationId
                                   select l.Address).FirstOrDefault();
            return locationAddress;
        }

        public dynamic GetLocationCoordinates(int LocationId)
        {

            var locationCoordinates = (from l in _context.Locations.ToList()
                                       where l.Id == LocationId
                                       select new
                                       {
                                           Longitude = l.Longitude,
                                           Lattitude = l.Latitude
                                       }

                                   ).FirstOrDefault();
            return locationCoordinates;
        }

        public string GetUserLocationAddress(string UserId)
        {

            var address = (from s in _context.Employees.ToList()
                           join m in _context.Locations.ToList() on s.LocationId equals m.Id
                           where s.UserId == UserId
                           select m.Address).FirstOrDefault();
            return address;
        }

        public int GetDestinationId(int locationId)
        {

            var destinationId = (from l in _context.Locations.ToList()
                                 where l.Id == locationId
                                 select l.Id).FirstOrDefault();
            return destinationId;
        }




        public IList<EmployeeModel> GetEmployees()
        {

            var statdata = (from s in _context.Employees
                            join z in _context.AspNetUsers on s.UserId equals z.Id
                            join sp in _context.AspNetUserRoles on s.UserId equals sp.UserId
                            join ur in _context.AspNetRoles on sp.RoleId equals ur.Id
                            join ul in _context.Locations on s.LocationId equals ul.Id
                            select new EmployeeModel
                            {
                                id = s.id,
                                fname = s.FirstName,
                                lname = s.LastName,
                                UserEmail = z.Email,
                                IsActiveUser = s.ActiveStatus,
                                locationName = ul.locationName,
                                UserPhone = z.PhoneNumber,
                                UserID = z.Id,
                                locationId = ul.id,
                                RoleId = sp.RoleId,
                                Gender = s.Gender.Value
                            });

            // Remove Distinct() and use GroupBy directly
            var uniqueEmployees = statdata.GroupBy(x => x.UserID).Select(g => g.FirstOrDefault()).ToList();

            return uniqueEmployees;

        }




        public IList<EmployeeModel> GetEmployeesById(string CUserId)
        {
            List<EmployeeModel> umodel = new List<EmployeeModel>();
            var statdata = (from s in _context.Employees
                            join z in _context.AspNetUsers on s.UserId equals z.Id
                            join sp in _context.AspNetUserRoles on s.UserId equals sp.UserId
                            join ur in _context.AspNetRoles on sp.RoleId equals ur.Id
                            join ul in _context.Locations on s.LocationId equals ul.Id
                            where z.Id == CUserId
                            let roles = _context.AspNetRoles.Where(p => p.Name == ur.Name).ToList()
                            select new EmployeeModel
                            {
                                id = s.id,
                                fname = s.FirstName,
                                lname = s.LastName,
                                UserEmail = z.Email,
                                IsActiveUser = s.ActiveStatus,
                                locationName = ul.locationName,
                                UserPhone = z.PhoneNumber,
                                UserID = z.Id,
                                Gender = s.Gender.Value,
                                //GetUserRole = roles,
                                locationId = ul.id,
                                RoleId = sp.RoleId
                            }).ToList();
            return statdata;


        }


        public IList<EmployeeModel> GetEmployeesByIdSingle(string CUserId)
        {
            List<EmployeeModel> umodel = new List<EmployeeModel>();

            var statdata = (from s in _context.Employees
                            join z in _context.AspNetUsers on s.UserId equals z.Id
                            //join sp in db.AspNetUserRoles on s.UserId equals sp.UserId
                            //join ur in db.AspNetRoles on sp.RoleId equals ur.Id
                            join ul in _context.Locations on s.LocationId equals ul.Id
                            where z.Id == CUserId
                            //let roles = db.AspNetRoles.Where(p => p.Name == ur.Name).ToList()
                            select new EmployeeModel
                            {
                                id = s.id,
                                fname = s.FirstName,
                                lname = s.LastName,
                                UserEmail = z.Email,
                                IsActiveUser = s.ActiveStatus,
                                locationName = ul.locationName,
                                UserPhone = z.PhoneNumber,
                                UserID = z.Id,
                                Gender = s.Gender.Value,
                                //GetUserRole = roles,
                                locationId = ul.id,
                                RoleId = ""
                            }).ToList();
            return statdata;

        }

        public IList<StateModel> GetStates()
        {

            var statdata = (from s in _context.States
                            select new StateModel
                            {
                                id = s.Id,
                                name = s.Name,
                                Instate = s.Instate
                            }).ToList();
            return statdata;

        }
        public IList<regionmodel> GetRegions()
        {
            var itemdata = (from s in _context.Regions
                            select new regionmodel
                            {
                                id = s.Id,
                                name = s.Name
                            }).ToList();
            return itemdata;

        }
        public IList<CustomerModel> GetCustomers()
        {

            var itemdata = (from s in _context.Customers
                            select new CustomerModel
                            {
                                id = s.Id,
                                address = s.Address,
                                email = s.Email,
                                firstName = s.FirstName,
                                gender = s.Gender,
                                lastName = s.LastName,
                                phoneNumber = s.PhoneNumber
                            }).ToList();
            return itemdata;

        }



        public IList<MerchantsModel> GetMerchantsSignups()
        {

            var itemdata = (from s in _context.MerchantSignups
                            select new MerchantsModel
                            {
                                ID = s.Id,
                                BizName = s.Businessname,
                                BizType = s.Businesstype,
                                FirstName = s.Firstname,
                                LastName = s.Lastname,
                                Phone = s.Phone,
                                State = s.State,
                                Email = s.Emailladdress,
                                PriceType = s.PriceType,
                                FixedPrice = s.FixedPrice,
                                FixedPriceInterState = s.FixedPriceInterState,
                                PricePerKm = s.PricePerKm,
                                IsApproved = s.IsApproved,
                                City = s.City,
                                PeakPeriod = s.Peakperiod,
                                OffPeak = s.Offpeak,
                                DateCreated = s.Datecreated
                            }).ToList();
            return itemdata;

        }

        public bool UpdateIsApprove(MerchantSignup model)
        {
            try
            {
 
                var merchant = (from m in _context.MerchantSignups where m.Id == model.Id select m).FirstOrDefault();
                if (merchant != null)
                {
                    merchant.IsApproved = model.IsApproved;
                }
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public MerchantsModel GetMerchantsModel(string userName)
        {

                MerchantsModel resp = (from s in _context.MerchantSignups where s.Emailladdress == userName select s).FirstOrDefault();
                return resp;
            
        }

        public IList<CustomerTypeModel> GetCustomerTypes()
        {

                var ctdata = (from s in _context.CustomerTypes
                              where s.IsDeleted == false
                              select new CustomerTypeModel
                              {
                                  id = s.Id,
                                  Custype = s.CusType,
                                  IsActive = s.IsActive
                              }).ToList();
                return ctdata;

        }

        public IList<CustomerTypeModel> GetActiveCustomerTypes()
        {

                var ctdata = (from s in _context.CustomerTypes
                              where s.IsDeleted == false
                              && s.IsActive == true
                              select new CustomerTypeModel
                              {
                                  id = s.Id,
                                  Custype = s.CusType,
                                  IsActive = s.IsActive
                              }).ToList();
                return ctdata;
            

        }
        public IList<InsuranceModel> GetInsurance()
        {

                var ctdata = (from s in _context.insurances
                              where s.IsDeleted == false
                              select new InsuranceModel
                              {
                                  id = s.Id,
                                  name = s.Name,
                                  value = s.Value
                              }).ToList();
                return ctdata;
    
        }

        public decimal GetInsuranceAmt()
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.insurances
                              where s.isDeleted == false
                              select s.value).FirstOrDefault();
                return ctdata;
            }

        }


        public decimal GetInsuranceFee()
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.insurances
                              where s.isDeleted == false
                              select s.value).FirstOrDefault();
                return ctdata;
            }

        }
        public IList<VehicleModelList> GetVehicleModelList()
        {

                var ctdata = (from s in _context.VehicleModels
                              join mname in _context.VehicleMakes on s.VehicleMakeId equals mname.Id
                              select new VehicleModelList
                              {
                                  ModelId = s.id,
                                  MakeId = mname.id,
                                  Fullname = $"{mname.name} ==> {s.name}",
                                  MakeName = mname.name,
                                  ModelName = s.name
                              }).Distinct().ToList();
                return ctdata;
            

        }

        public IList<VehiclesListModel> GetVehicleList()
        {
            //using (var db = new liblogisticsDataContext())
            //{
                var ctdata = (from s in _context.Vehicles
                              join mname in _context.VehicleModels on s.VehicleModelId equals mname.Id
                              join vmake in _context.VehicleMakes on mname.VehicleMakeId equals vmake.Id
                              where s.IsDeleted == false
                              select new VehiclesListModel
                              {
                                  regNumber = s.regNumber,
                                  EngineNumber = s.EngineNumber,
                                  DateCreated = s.DateCreated,
                                  id = s.id,
                                  isActive = s.isActive,
                                  IsDeleted = s.IsDeleted,
                                  modelId = s.modelId,
                                  ModelName = $"{vmake.name} ==> {mname.name}"
                              }).ToList();
                return ctdata;
        //    }

        }

        public IList<VehicleMakeList> GetVehicleMakeList()
        {

                var ctdata = (from s in _context.VehicleMakes
                              select new VehicleMakeList
                              {
                                  Id = s.Id,
                                  Makename = s.Name
                              }).ToList();
                return ctdata;
           

        }
        public IList<DriversModel> GetDriverList()
        {

                var ctdata = (from s in _context.Drivers
                              where s.IsDeleted == false
                              select new DriversModel
                              {
                                  Drivername = s.Name,
                                  dateCreated = s.CreationTime,
                                  id = s.Id,
                                  Driverphone = s.Phone1,
                                  isActive = s.Active,
                                  isDeleted = s.IsDeleted
                              }).ToList();
                return ctdata;
            
        }


        public IList<DriversModel> GetActiveDriverList()
        {
       
                var ctdata = (from s in _context.Drivers
                              where s.IsDeleted == false && s.Active == true
                              select new DriversModel
                              {
                                  Drivername = s.DriverName,
                                  dateCreated = s.DateCreated,
                                  id = s.Id,
                                  Driverphone = s.DriverPhone,
                                  isDeleted = s.Isdeleted
                              }).ToList();
                return ctdata;
            
        }

        public IList<PackagePriceModel> GetPackagingPrice()
        {


                var ctdata = (from s in _context.PackagingPricings
                              where s.IsDeleted == false
                              select new PackagePriceModel
                              {
                                  id = s.Id,
                                  name = s.Name,
                                  price = s.Price,
                                  isActive = s.IsActive
                              }).ToList();
                return ctdata;
            

        }
        public decimal GetVat()
        {

                var cdata = (from s in _context.Vats
                             where s.IsDeleted == false
                             select s.VatValue).FirstOrDefault();
                return cdata;
            
        }

        public int GetZoneID(int deptId, int ArivId)
        {


                var cdata = (from s in _context.ZoneMappings
                             where s.isDeleted == false && (s.departuteId == deptId && s.destinationId == ArivId) || (s.departuteId == ArivId && s.destinationId == deptId) /*since the prices are the same in both direction*/
                             select s.zoneId).FirstOrDefault();
                /*If it does not exist create it*/
                if (cdata < 1)
                {
                    var zId = (from z in _context.Zones where z.name == "Zone C" select z).FirstOrDefault().id;//.Skip(2).Take(1).FirstOrDefault().id;/*defaulted zone to Zone C, subject to change!*/
                    try
                    {
                        var zm = new ZoneMapping
                        {
                            DateCreated = DateTime.Now,
                            DateModified = DateTime.Now,
                            departuteId = deptId,
                            destinationId = ArivId,
                            isActive = true,
                            isDeleted = false,
                            zoneId = zId
                        };
                        db.ZoneMappings.InsertOnSubmit(zm);
                        db.SubmitChanges();
                        //cdata = GetZoneID(deptId, ArivId);
                    }
                    catch (Exception)
                    {

                    }
                    cdata = zId;
                }
                return cdata;
            
        }

        /// <summary>
        /// This method gets price by Zone ID
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="itemWeight"></param>
        /// <returns></returns>
        public decimal GetRegPricebyZoneID(int znId, decimal itemWeight)
        {

            using (var db = new liblogisticsDataContext())
            {
                var cdata = (from s in db.ZonePrices
                             where s.isDeleted == false && s.zoneId == znId && s.weight == itemWeight
                             && s.isActive == true
                             select s.price).FirstOrDefault();
                return cdata;
            }
        }

        /// <summary>
        /// This method gets Special price by ID, it doesn't work with zone Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public decimal GetSpecialpricingById(int Id)
        {

            using (var db = new liblogisticsDataContext())
            {
                var cdata = (from s in db.SpecialShipmentPrices
                             where s.id == Id
                             select s.PricePerKG).FirstOrDefault();
                return cdata;
            }
        }


        /// <summary>
        /// This method gets price of Generic shipments by zone ID
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="specialpackageId"></param>
        /// <returns></returns>
        public decimal GetGenericPricebyZoneIDspkId(int znId, int spkId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var cdata = (from s in db.SpecialPackagepricings
                             where s.IsDeleted == false && s.zoneId == znId && s.specialPackageId == spkId
                             select s.price).FirstOrDefault();
                return cdata;
            }
        }
        public IList<SpecialPackageModel> GetSpecialPackageList()
        {
            //
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.SpecialPackages
                              where s.IsDeleted == false
                              select new SpecialPackageModel
                              {
                                  id = s.id,
                                  name = s.name,
                                  ItemWeight = s.itemWeight,
                                  isActive = s.isActive
                              }).ToList();
                return ctdata;
            }

        }
        public IEnumerable<SpecialPackagePriceModel> GetSpecialPackagePriceList()
        {

            GenerateOmittedGenericItem();
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.SpecialPackagepricings
                              where s.IsDeleted == false
                              select new SpecialPackagePriceModel
                              {
                                  id = s.id,
                                  isActive = s.isActive,
                                  DateCreated = s.DateCreated,
                                  DateModified = s.DateModified,
                                  desription = s.desription,
                                  IsDeleted = s.IsDeleted,
                                  price = s.price,
                                  specialPackageId = s.specialPackageId,
                                  specialPackagename = s.SpecialPackage != null ? s.SpecialPackage.name : null,
                                  weight = s.SpecialPackage != null ? s.SpecialPackage.itemWeight : 0,
                                  //Zone = s.Zone,
                                  zoneId = s.zoneId,
                                  zonename = s.Zone != null ? s.Zone.name : null,
                                  IsBikeable = s.IsBikeable,
                                  IsTruckable = s.IsTruckable,
                                  IsVanable = s.IsVanable,
                                  ShipmentItemCategoryId = s.ShipmentItemCategoryId
                              }).ToList();
                return ctdata;
            }

        }

        public IList<SpecialPackagePriceModel> GetSpecialPackagePricesByZone(double departureLatitude, double departureLongitude, double destinationLatitude, double destinationLongitude)
        {
            try
            {

                var locations = from location in _context.Locations select location;
                double departureLeastDistance = double.MaxValue;
                int departureLocationId = 0;

                foreach (var l in locations)
                {
                    var displacementInKM = ComputeDistanceBetweenCoordinates(departureLatitude, departureLongitude, l.Latitude ?? 0, l.Longitude ?? 0);
                    if (displacementInKM < departureLeastDistance)
                    {
                        departureLeastDistance = displacementInKM;
                        departureLocationId = l.Id;
                    }
                }

                double destinationLeastDistance = double.MaxValue;
                int destinationLocationId = 0;

                foreach (var l in locations)
                {
                    var displacementInKM = ComputeDistanceBetweenCoordinates(destinationLatitude, destinationLongitude, l.Latitude ?? 0, l.Longitude ?? 0);
                    if (displacementInKM < destinationLeastDistance)
                    {
                        destinationLeastDistance = displacementInKM;
                        destinationLocationId = l.Id;
                    }
                }

                var zoneId = GetZoneID(departureLocationId, destinationLocationId);
                //if(zoneId == 0)
                //{
                //    //switched the order
                //    zoneId = GetZoneID(destinationLocationId, departureLocationId);
                //    if(zoneId == 0)
                //    {
                //        //if zoneId is still zero it is assumed that the combination does not exist in the zone mapping table so...
                //        //default to interstate... would be adjusted by the managers
                //        var zId = (from z in db.Zones where z.name == "Zone C" select z).FirstOrDefault().id;//.Skip(2).Take(1).FirstOrDefault().id;
                //        var zm = new ZoneMapping
                //        {
                //            DateCreated = DateTime.Now,
                //            DateModified = DateTime.Now,
                //            departuteId = departureLocationId,
                //            destinationId = destinationLocationId,
                //            isActive = true,
                //            isDeleted = false,
                //            zoneId = zId
                //        };
                //        db.ZoneMappings.InsertOnSubmit(zm);
                //        db.SubmitChanges();
                //        zoneId = GetZoneID(departureLocationId, destinationLocationId);
                //    }

                //}

                var resp = GetSpecialPackagePriceList().Where(x => x.zoneId == zoneId);
                return resp.ToList();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public PriceCalculatorModel GetMobilePriceSetting(string stateName)
        {
            try
            {
                var priceCalculators = (from pc in db.PriceCalculators
                                        where (pc.State == stateName)
                                        && pc.IsDefaultCost == true
                                        select pc).ToList();

                PriceCalculatorModel resp = priceCalculators.FirstOrDefault();
                return resp;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void GenerateOmittedGenericItem()
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            //get all valid generic items in database
            var SpecialPackages = (from specialp in db.SpecialPackages where specialp.IsDeleted == false select specialp).ToList();

            //foreach valid generic items in database
            foreach (var SpecialPackage in SpecialPackages)
            {
                //go to the specialPackagePricings table and search for the rows with that special package id (that generic item Id)
                var SpecialPackagePricings = (from specialPackagePricings in db.SpecialPackagepricings where specialPackagePricings.IsDeleted == false && specialPackagePricings.specialPackageId == SpecialPackage.id select specialPackagePricings).ToList();

                //does the list include pricing for zone A? if not add it!
                if (!SpecialPackagePricings.Any(a => a.zoneId == 1))
                {
                    var SPPdata = new SpecialPackagepricing
                    {
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        desription = $"Generic Item Pricing for {SpecialPackage.name} Within the state",
                        isActive = true,
                        IsDeleted = false,
                        price = 1_000.00m,//HARD CODE ALERT!!!
                        specialPackageId = SpecialPackage.id,
                        weight = 0.50m,//HARD CODE ALERT!!!
                        zoneId = 1
                    };
                    db.SpecialPackagepricings.InsertOnSubmit(SPPdata);
                    db.SubmitChanges();
                }

                //does the list include pricing for zone B? if not add it!
                if (!SpecialPackagePricings.Any(a => a.zoneId == 2))
                {
                    var SPPdata = new SpecialPackagepricing
                    {
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        desription = $"Generic Item Pricing for {SpecialPackage.name} within the same geographic location",
                        isActive = true,
                        IsDeleted = false,
                        price = 1_000.00m,//HARD CODE ALERT!!!
                        specialPackageId = SpecialPackage.id,
                        weight = 0.50m,//HARD CODE ALERT!!!
                        zoneId = 2
                    };
                    db.SpecialPackagepricings.InsertOnSubmit(SPPdata);
                    db.SubmitChanges();
                }

                //does the list include pricing for zone C? if not add it!
                if (!SpecialPackagePricings.Any(a => a.zoneId == 3))
                {
                    var SPPdata = new SpecialPackagepricing
                    {
                        DateCreated = DateTime.Now,
                        DateModified = DateTime.Now,
                        desription = $"Generic Item Pricing for {SpecialPackage.name} for inter-state like locations",
                        isActive = true,
                        IsDeleted = false,
                        price = 1_000.00m,//HARD CODE ALERT!!!
                        specialPackageId = SpecialPackage.id,
                        weight = 0.50m,//HARD CODE ALERT!!!
                        zoneId = 3
                    };
                    db.SpecialPackagepricings.InsertOnSubmit(SPPdata);
                    db.SubmitChanges();
                }

                //checking for other zones would be quite redundant given that we only use Zone A, B & C!! 
            }
        }

        /// <summary>
        /// Gets Packaging price for the selected package
        /// </summary>
        /// <param name="pkid"></param>
        /// <returns></returns>
        public decimal GetpkFee(int pkid)
        {

            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.PackagingPricings
                              where s.isDeleted == false && s.isActive == true && s.id == pkid
                              select s.price).FirstOrDefault();
                return ctdata;
            }

        }

        public string GetpkName(int pkid)
        {

            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.PackagingPricings
                              where s.isDeleted == false && s.isActive == true && s.id == pkid
                              select s.name).FirstOrDefault();
                return ctdata;
            }
        }

        public decimal GetDeliveryFee(int dtypeId, int zoneId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.DeliveryTypePrices
                              where s.IsDeleted == false && s.deliveryTypeId == dtypeId && s.zoneId == zoneId
                              select s.price).FirstOrDefault();
                return ctdata;
            }
            //The delivery-type-price table needs  
        }

        public decimal GetServiceFee(int srtypeId, int zoneId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.ServiceTypePrices
                              where s.IsDeleted == false && s.ServiceTypeId == srtypeId && s.ZoneId == zoneId
                              select s.Price).FirstOrDefault();
                return ctdata;
            }
            //The service-type-price table needs  
        }




        public IList<SpecialPackagePriceModel> GetGenericPackagePrice()
        {

            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.SpecialPackagepricings
                              join z in db.Zones on s.zoneId equals z.id
                              join sp in db.SpecialPackages on s.specialPackageId equals sp.id
                              where s.IsDeleted == false
                              let shipmentItemCategoryValue = (from sic in db.ShipmentItemCategories where sic.Id == s.ShipmentItemCategoryId select sic.ItemCategoryCode).FirstOrDefault()
                              select new SpecialPackagePriceModel
                              {
                                  id = s.id,
                                  desription = sp.name,
                                  weight = s.weight,
                                  zoneId = s.zoneId,
                                  zonename = z.name,
                                  price = s.price,
                                  specialPackagename = sp.name,
                                  specialPackageId = sp.id,
                                  isActive = s.isActive,
                                  IsBikeable = s.IsBikeable,
                                  IsVanable = s.IsVanable,
                                  IsTruckable = s.IsTruckable,
                                  ShipmentItemCategoryId = s.ShipmentItemCategoryId,
                                  ShipmentItemCategoryName = shipmentItemCategoryValue
                              }).ToList();
                return ctdata;
            }

        }

        //public IList<CustomerTypeModel> GetCustomerTypesById(int itmId)
        //{
        //    
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var ctdata = (from s in db.CustomerTypes
        //                      where s.isDeleted == true && s.id == itmId
        //                      select new CustomerTypeModel
        //                      {
        //                          id = s.id,
        //                          Custype = s.CusType,
        //                          IsActive = s.isActive
        //                      }).ToList();
        //        return ctdata;
        //    }

        //}
        public IList<UserRoleModel> GetUserRoles()
        {

            using (var db = new liblogisticsDataContext())
            {
                var RoleData = (from r in db.AspNetRoles
                                select new UserRoleModel
                                {
                                    Id = r.Id,
                                    Rolename = r.Name
                                }).OrderBy(x => x.Rolename).ToList();
                return RoleData;
            }

        }


        public IList<locationModel> Getlocations()
        {

            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from l in db.Locations
                                join s in db.States on l.stateId equals s.id
                                where l.IsDeleted == false
                                select new locationModel
                                {
                                    id = l.id,
                                    name = l.locationName,
                                    statename = s.name,
                                    Locationtype = l.LocationType,
                                    FormattedName = s.name + " ==> " + l.locationName,
                                    stateId = l.stateId,
                                    Address = l.Address,
                                    Code = l.Code,
                                    Image = l.Image,
                                    Latitude = l.Latitude,
                                    Longitude = l.Longitude,
                                    ContactPerson = l.ContactPerson,
                                    ContactPersonNo = l.ContactPersonNo,
                                    IsCommision = l.IsCommision

                                }).OrderBy(x => x.FormattedName).ToList();
                return statdata;
            }

        }

        public string GetlocationName(int locationId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var locationdata = (from l in db.Locations
                                    join s in db.States on l.stateId equals s.id
                                    where l.id == locationId && l.IsDeleted == false
                                    select new locationModel
                                    {
                                        id = l.id,
                                        name = l.locationName,
                                        statename = s.name,
                                        Locationtype = l.LocationType,
                                        FormattedName = s.name + " ==> " + l.locationName,
                                        stateId = l.stateId,
                                        Address = l.Address,
                                        Code = l.Code,
                                        Image = l.Image,
                                        Latitude = l.Latitude,
                                        Longitude = l.Longitude,
                                        ContactPerson = l.ContactPerson,
                                        ContactPersonNo = l.ContactPersonNo,
                                        IsCommision = l.IsCommision

                                    }).OrderBy(x => x.FormattedName).FirstOrDefault();
                return locationdata.FormattedName;
            }

        }


        public IList<locationModel> Getlocations(int locationId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from l in db.Locations
                                join s in db.States on l.stateId equals s.id
                                where l.IsDeleted == false && l.stateId == locationId
                                select new locationModel
                                {
                                    id = l.id,
                                    name = l.locationName,
                                    statename = s.name,
                                    Locationtype = l.LocationType,
                                    FormattedName = s.name + " ==> " + l.locationName,
                                    stateId = l.stateId,
                                    Address = l.Address,
                                    Code = l.Code,
                                    Image = l.Image,
                                    Latitude = l.Latitude,
                                    Longitude = l.Longitude,
                                    ContactPerson = l.ContactPerson,
                                    ContactPersonNo = l.ContactPersonNo,
                                    IsCommision = l.IsCommision

                                }).OrderBy(x => x.FormattedName).ToList();
                return statdata;
            }

        }


        //public IList<locationModel> GetStateLocations(int UserLocationId)
        //{
        //    
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from l in db.LocationHubs
        //                        join t in db.Locations on l.LocationId equals t.id
        //                        join s in db.States on t.stateId equals s.id
        //                        where t.IsDeleted == false && l.HubId == UserLocationId
        //                        select new locationModel
        //                        {
        //                            id = t.id,
        //                            name = t.locationName,
        //                            statename = s.name,
        //                            Locationtype = t.LocationType,
        //                            FormattedName = s.name + " ==> " + t.locationName
        //                        }).OrderBy(x => x.FormattedName).ToList();
        //        return statdata;
        //    }

        //}
        public IList<locationModel> GetHubs()
        {

            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from l in db.Locations
                                join s in db.States on l.stateId equals s.id
                                where l.IsDeleted == false &&
                                l.LocationType == 1
                                select new locationModel
                                {
                                    id = l.id,
                                    name = l.locationName,
                                    statename = s.name,
                                    Locationtype = l.LocationType,
                                    FormattedName = s.name + " ==> " + l.locationName
                                }).OrderBy(x => x.FormattedName).ToList();
                return statdata;
            }

        }
        public bool CheckForHubs(int locationId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from l in db.Locations
                                join s in db.States on l.stateId equals s.id
                                where l.IsDeleted == false &&
                                l.LocationType == 1 && l.id == locationId
                                select l.State).ToList();
                if (statdata.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

        }
        public IList<itemConditionModel> GetItemConditions()
        {
            using (var db = new liblogisticsDataContext())
            {
                var itemdata = (from l in db.ItemConditions
                                where l.isDeleted == false
                                select new itemConditionModel
                                {
                                    ID = l.id,
                                    Itemcondition = l.ItemCondition1
                                }
                               ).ToList();
                return itemdata;
            }
        }

        public IList<ZoneModel> GetZones()
        {
            using (var db = new liblogisticsDataContext())
            {
                var zonedata = (from l in db.Zones
                                where l.IsDeleted == false
                                select new ZoneModel
                                {
                                    id = l.id,
                                    name = l.name,
                                    isActive = l.isActive,
                                    ZonePrice = l.ZonePrice,
                                    ZoneDescription = l.ZoneDescription,
                                    DateCreated = l.DateCreated,
                                    DateModified = l.DateModified,
                                    IsDeleted = l.IsDeleted
                                }).ToList();
                return zonedata;
            }

        }

        public IList<SpecialShipmentModel> GetSpecialShipmentPrices()
        {
            using (var db = new liblogisticsDataContext())
            {
                var shp = (from l in db.SpecialShipmentPrices
                           select new SpecialShipmentModel
                           {
                               id = l.id,
                               Shipmentitem = l.ShipmentItem,
                               price = l.PricePerKG
                           }).ToList();
                return shp;
            }

        }

        public decimal GetPricePerKGbyZoneID(int zoneId, decimal itemWeight)
        {
            using (var db = new liblogisticsDataContext())
            {
                var zonedata = (from l in db.ZonePricePerKgs
                                where l.isDeleted == false && l.zoneId == zoneId
                                select l.pricePerKg
                                ).FirstOrDefault();
                decimal pricedata = (zonedata * itemWeight);
                return pricedata;
            }
        }
        public IList<ZonePerKgModel> GetPricePerKGZone()
        {

            using (var db = new liblogisticsDataContext())
            {
                var zonedata = (from l in db.ZonePricePerKgs
                                join z in db.Zones on l.zoneId equals z.id
                                where l.isDeleted == false
                                select new ZonePerKgModel
                                {
                                    id = l.id,
                                    pricePerKg = l.pricePerKg,
                                    zoneName = z.name,
                                    zoneId = l.zoneId

                                }).ToList();
                return zonedata;
            }

        }

        public IList<ServiceTypeModel> GetServiceType()
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from s in db.ServiceTypes
                               where s.IsDeleted == false
                               select new ServiceTypeModel
                               {
                                   ID = s.ID,
                                   ServiceType = s.ServiceType1,
                                   IsDeleted = s.IsDeleted
                               }).ToList();
                return libdata;
            }

        }

        public IList<DeliveryTypeModel> GetDeliveryType()
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.Deliverytypes
                               join m in db.CustomerTypes on l.CustomerType equals m.id
                               where l.IsDeleted == false
                               select new DeliveryTypeModel
                               {
                                   id = l.id,
                                   name = l.name,
                                   CustomerType = m.CusType,
                                   IsActive = l.isActive
                               }).ToList();
                return libdata;
            }

        }
        public IList<DeliveryTypeModel> GetDeliveryTypeActive()
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.Deliverytypes
                               join m in db.CustomerTypes on l.CustomerType equals m.id
                               where l.IsDeleted == false && l.isActive == true
                               select new DeliveryTypeModel
                               {
                                   id = l.id,
                                   name = l.name,
                                   CustomerType = m.CusType,
                               }).ToList();
                return libdata;
            }

        }
        public IList<DeliveryTypePriceModel> GetDeliveryTypePricing()
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.DeliveryTypePrices
                               join c in db.Deliverytypes on l.deliveryTypeId equals c.id
                               join m in db.Zones on l.zoneId equals m.id
                               where l.IsDeleted == false
                               select new DeliveryTypePriceModel
                               {
                                   id = l.id,
                                   DeliveryType = c.name,
                                   DeliveryTypeId = c.id,
                                   price = l.price,
                                   zonename = m.name,
                                   zoneid = m.id,
                                   IsActive = l.isActive
                               }).ToList();
                return libdata;
            }

        }

        public IList<ServiceTypePriceModel> GetServiceTypePricing()
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.ServiceTypePrices
                               join c in db.ServiceTypes on l.ServiceTypeId equals c.ID
                               join m in db.Zones on l.ZoneId equals m.id
                               where l.IsDeleted == false
                               select new ServiceTypePriceModel
                               {

                                   id = l.ID,
                                   ServiceType = c.ServiceType1,
                                   ServiceTypeId = c.ID,
                                   price = l.Price,
                                   zonename = m.name,
                                   zoneid = m.id,
                                   IsActive = l.IsActive
                                   //IsDeleted = 
                               }).ToList();
                return libdata;
            }

        }

        public IList<ZoneMappingModel> GetZoneMapping()
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.ZoneMappings
                               join c in db.Locations on l.departuteId equals c.id
                               join d in db.Locations on l.destinationId equals d.id
                               join m in db.Zones on l.zoneId equals m.id
                               where l.isDeleted == false
                               select new ZoneMappingModel
                               {
                                   id = l.id,
                                   departuteId = c.id,
                                   destinationId = d.id,
                                   zoneId = l.zoneId,
                                   DepartureTerminalName = c.locationName,
                                   ArrivalTerminalName = d.locationName,
                                   ZoneName = m.name,
                                   isActive = l.isActive
                               }).ToList();
                return libdata;
            }

        }

        public bool GetZoneMappingExisting(int dpId, int destId, int zoneId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.ZoneMappings
                               join c in db.Locations on l.departuteId equals c.id
                               join d in db.Locations on l.destinationId equals d.id
                               join m in db.Zones on l.zoneId equals m.id
                               where l.isDeleted == false && c.id == dpId && d.id == destId && m.id == zoneId
                               select l.id).FirstOrDefault();
                if (libdata.ToString() == "0")
                {
                    return false;
                }
                else
                {
                    return true;
                }


            }

        }

        public IList<ZonePriceModel> GetZonePricing()
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.ZonePrices
                               join d in db.Zones on l.zoneId equals d.id
                               where l.isDeleted == false
                               select new ZonePriceModel
                               {
                                   id = l.id,
                                   price = l.price,
                                   weight = l.weight,
                                   zoneId = d.id,
                                   zonename = d.name,
                                   isActive = l.isActive
                               }).ToList();
                return libdata;
            }

        }
        public bool GetDeliveryTypeStatus(int itemId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.Deliverytypes
                               where l.id == itemId && l.IsDeleted == false
                               select l.isActive).FirstOrDefault();
                return libdata;
            }
        }
        public bool GetDeliveryTypePriceStatus(int itemId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.DeliveryTypePrices
                               where l.id == itemId && l.IsDeleted == false
                               select l.isActive).FirstOrDefault();
                return libdata;
            }
        }

        public bool GetCustyomerTypeStatus(int itemId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from l in db.CustomerTypes
                               where l.id == itemId && l.isDeleted == false
                               select l.isActive).FirstOrDefault();
                return libdata;
            }
        }
        //public IList<StateModel> GetStateIdbyname(string statename)
        //{
        //    
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from s in db.States
        //                        where s.name == statename
        //                        select new StateModel
        //                        {
        //                            id = s.id,
        //                            name = s.name
        //                        }).ToList();
        //        return statdata;
        //    }

        //}

        public IList<VolumeRangeModel> GetVolumeRange()
        {
            liblogisticsDataContext dataContext = new liblogisticsDataContext();
            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from v in db.VolumeRanges
                               join m in db.CustomerTypes on v.CustomerType equals m.id
                               where v.isDeleted == null
                               select new VolumeRangeModel
                               {
                                   Id = v.Id,
                                   LowerRange = v.LowerRange,
                                   UpperRange = v.UpperRange,
                                   FormattedName = v.LowerRange + " to " + v.UpperRange,
                                   Description = v.Description,
                                   Code = v.Code,
                                   CustomerType = v.CustomerType,
                                   isActive = v.isActive
                               }).ToList();
                return libdata;
            }

        }

        public IList<WeightRangeModel> GetWeightRange()
        {
            liblogisticsDataContext dataContext = new liblogisticsDataContext();
            using (var db = new liblogisticsDataContext())
            {
                var libdata = (from w in db.WeightRanges
                               where w.IsDeleted == null
                               select new WeightRangeModel
                               {
                                   Id = w.Id,
                                   LowerRange = w.LowerRange,
                                   UpperRange = w.UpperRange,
                                   FormattedName = w.LowerRange + " to " + w.UpperRange,
                                   Code = w.Code,
                                   isActive = w.IsActive
                               }).ToList();
                return libdata;
            }

        }


        public int GetStateIdbynameNew(string statename)
        {

            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from s in db.States
                                where s.name == statename
                                select s.id).SingleOrDefault();

                return statdata;
            }

        }

        public int GetCustomerTypeByselectedDeliveryType(string dataname)
        {

            using (var db = new liblogisticsDataContext())
            {
                var resultData = (from s in db.Deliverytypes
                                  where s.name == dataname
                                  select s.CustomerType).SingleOrDefault();

                return resultData;
            }

        }
        public int GetSelectedDeliveryOption(int dataId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var resultData = (from s in db.DeliveryTypePrices
                                  where s.id == dataId
                                  select s.zoneId).SingleOrDefault();

                return resultData;
            }

        }

        public string GetlocationsbyID(int locationId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from l in db.Locations
                                join s in db.States on l.stateId equals s.id
                                where l.id == locationId
                                select l.locationName);
                return statdata.ToList().FirstOrDefault();
            }

        }
        public string GetStatebyID(int stateId)
        {
            try
            {

                using (var db = new liblogisticsDataContext())
                {
                    var statdata = (from s in db.States
                                    where s.id == stateId
                                    select s.name).ToList().FirstOrDefault();
                    return statdata;
                }
            }
            catch (Exception)
            {
                return null;
            }


        }
        //public string GetStatebyLocationId(int locationid)
        //{
        //    
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var statdata = (from s in db.States
        //                        join l in db.Locations on s.id equals l.stateId
        //                        where l.id == locationid
        //                        select s.name);
        //        return statdata.ToString();
        //    }

        //}
        public List<returnmsg> AddLocation(locationModel model)
        {
            return AddLocation(model.name, model.stateId, model.Locationtype, model.Address, model.Image, model.Code, model.Latitude, model.Longitude, model.ContactPerson, model.ContactPersonNo, model.IsCommision);
        }
        public List<returnmsg> AddLocation(string locationname, int stateid, int ltype, string address = null, string image = null, string code = null, float? latitude = null, float? longitude = null, string contactpersonname = null, string contactpersonphone = null, bool? isCommissioned = null)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var locations = from p in db.Locations
                                where p.locationName == locationname
                                select p;
                if (locations.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Location already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    Location location = new Location();
                    location.locationName = locationname;
                    location.stateId = stateid;
                    location.DateCreated = DateTime.Now;
                    location.IsDeleted = false;
                    location.DateModified = DateTime.Now;
                    location.LocationType = ltype;

                    location.Address = address;
                    location.Image = image;
                    location.Code = code;
                    location.Latitude = latitude;
                    location.Longitude = longitude;
                    location.ContactPerson = contactpersonname;
                    location.ContactPersonNo = contactpersonphone;
                    location.IsCommision = isCommissioned;

                    db.Locations.InsertOnSubmit(location);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Location successfully Added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> AddDeliveryType(string dataname, int custId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var ctx = from p in db.Deliverytypes
                          where p.name == dataname && p.CustomerType == custId
                          select p;
                if (ctx.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Data already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    Deliverytype dtc = new Deliverytype();
                    dtc.name = dataname;
                    dtc.CustomerType = custId;
                    dtc.isActive = true;
                    dtc.DateCreated = DateTime.Now;
                    db.Deliverytypes.InsertOnSubmit(dtc);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> AddDeliveryTypePricing(int zoneId, int deliveryTypeId, decimal price)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                DeliveryTypePrice dtc = new DeliveryTypePrice();
                dtc.zoneId = zoneId;
                dtc.price = price;
                dtc.deliveryTypeId = deliveryTypeId;
                dtc.isActive = true;
                dtc.DateCreated = DateTime.Now;
                db.DeliveryTypePrices.InsertOnSubmit(dtc);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }


        public List<returnmsg> AddServiceTypePricing(int zoneId, int serviceTypeId, decimal price)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                ServiceTypePrice dtc = new ServiceTypePrice();
                dtc.ZoneId = zoneId;
                dtc.Price = price;
                dtc.ServiceTypeId = serviceTypeId;
                dtc.IsActive = true;
                dtc.DateCreated = DateTime.Now;
                dtc.DateModified = DateTime.Now;
                db.ServiceTypePrices.InsertOnSubmit(dtc);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> AddZoneMapping(int deptId, int arrId, int zoneId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                ZoneMapping dtc = new ZoneMapping();
                dtc.zoneId = zoneId;
                dtc.departuteId = deptId;
                dtc.destinationId = arrId;
                dtc.isActive = true;
                dtc.DateCreated = DateTime.Now;
                db.ZoneMappings.InsertOnSubmit(dtc);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> AddZonePricing(int zoneId, decimal price, decimal weight)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                ZonePrice dtc = new ZonePrice();
                dtc.zoneId = zoneId;
                dtc.weight = weight;
                dtc.price = price;
                dtc.isActive = true;
                dtc.DateCreated = DateTime.Now;
                db.ZonePrices.InsertOnSubmit(dtc);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> AddZonePerKG(int zoneId, decimal pricePerKG)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                ZonePricePerKg dtc = new ZonePricePerKg();
                dtc.zoneId = zoneId;
                dtc.pricePerKg = pricePerKG;
                dtc.dateCreated = DateTime.Now;
                db.ZonePricePerKgs.InsertOnSubmit(dtc);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> AddVehicelModel(int MakeId, string ModelName)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                Vehiclemodel dtc = new Vehiclemodel();
                dtc.makeId = MakeId;
                dtc.name = ModelName;
                db.Vehiclemodels.InsertOnSubmit(dtc);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> AddEmployee(string UserId, int LocationId, string UserRoleId, string fname, string lname, int gender, string creatorUserId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                Employee employee = new Employee();
                employee.UserId = UserId;
                employee.LocationId = LocationId;
                employee.FirstName = fname;
                employee.LastName = lname;
                employee.Gender = gender;
                employee.ActiveStatus = true;
                employee.CreatorUserId = creatorUserId;
                employee.DateCreated = DateTime.Now;
                db.Employees.InsertOnSubmit(employee);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Employee successfully Added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<returnmsg> AddZone(string zonename, string description = null)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                //liblogisticsDataContext db = new liblogisticsDataContext();
                var locations = from p in db.Zones
                                where p.name == zonename && p.IsDeleted == true
                                select p;
                if (locations.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Zone already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    Zone location = new Zone();
                    location.name = zonename;
                    location.DateCreated = DateTime.Now;
                    location.DateModified = DateTime.Now;
                    location.isActive = true;
                    location.ZoneDescription = description;
                    location.ZonePrice = 0;
                    location.IsDeleted = false;
                    db.Zones.InsertOnSubmit(location);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Zone successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public void CreateItemPriceForActiveZones(string zoneName)
        {

        }
        public List<returnmsg> AddSpecialShipment(string shpname, decimal price)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var locations = from p in db.SpecialShipmentPrices
                                where p.ShipmentItem == shpname
                                select p;
                if (locations.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Shipment already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    SpecialShipmentPrice special = new SpecialShipmentPrice();
                    special.ShipmentItem = shpname;
                    special.PricePerKG = price;
                    db.SpecialShipmentPrices.InsertOnSubmit(special);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Data Successfully Added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }




        public List<returnmsg> addCustomerType(string itemname)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var datadetails = from p in db.CustomerTypes
                                  where p.CusType == itemname
                                  select p;
                if (datadetails.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Entry already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    CustomerType datamodel = new CustomerType();
                    datamodel.CusType = itemname;
                    datamodel.isActive = true;
                    datamodel.DateCreated = DateTime.Now;
                    db.CustomerTypes.InsertOnSubmit(datamodel);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> addInsurance(string itemname, int Invalue)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var datadetails = from p in db.insurances
                                  where p.name == itemname
                                  select p;
                if (datadetails.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Entry already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    insurance datamodel = new insurance();
                    datamodel.name = itemname;
                    datamodel.value = Invalue;
                    datamodel.dateCreated = DateTime.Now;
                    db.insurances.InsertOnSubmit(datamodel);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
                throw;
            }
        }

        public List<returnmsg> addPackagingPrice(string itemname, decimal price)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var datadetails = from p in db.PackagingPricings
                                  where p.name == itemname
                                  select p;
                if (datadetails.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Entry already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    PackagingPricing datamodel = new PackagingPricing();
                    datamodel.name = itemname;
                    datamodel.price = price;
                    datamodel.isActive = true;
                    datamodel.dateCreated = DateTime.Now;
                    datamodel.isDeleted = false;
                    db.PackagingPricings.InsertOnSubmit(datamodel);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;

            }
        }

        public List<returnmsg> AddVehicle(string regNum, string engineNum, int modelId, int companyInfoId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                Vehicle datamodel = new Vehicle();
                datamodel.regNumber = regNum;
                datamodel.EngineNumber = engineNum;
                datamodel.modelId = modelId;
                datamodel.isActive = true;
                datamodel.DateCreated = DateTime.Now;
                datamodel.DateModified = DateTime.Now;
                datamodel.IsDeleted = false;
                datamodel.CompanyInfoId = companyInfoId;
                db.Vehicles.InsertOnSubmit(datamodel);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;

            }
        }
        public List<returnmsg> AddVehicle(VehiclesListModel model)
        {
            return _ = AddVehicle(model.regNumber, model.EngineNumber, model.modelId, model.CompanyInfoId);
        }
        public List<returnmsg> AddDriver(string drivername, string phone, int companyInfoId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                Driver datamodel = new Driver();
                datamodel.DriverName = drivername;
                datamodel.DriverPhone = phone;
                datamodel.IsActive = true;
                datamodel.DateCreated = DateTime.Now;
                datamodel.Isdeleted = false;
                datamodel.CompanyInfoId = companyInfoId;
                db.Drivers.InsertOnSubmit(datamodel);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;

            }
        }


        public List<returnmsg> AddGenericPackaging(string itemname, decimal weight, decimal itemPrice, int shipmentItemCategoryId, bool isbikeable, bool isvanable, bool istruckable)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                var datadetails = from p in db.SpecialPackages
                                  where p.name == itemname
                                  select p;
                if (datadetails.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Entry already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    SpecialPackage datamodel = new SpecialPackage();
                    datamodel.name = itemname;
                    datamodel.itemWeight = weight;
                    datamodel.isActive = true;
                    datamodel.DateCreated = DateTime.Now;

                    db.SpecialPackages.InsertOnSubmit(datamodel);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    //create the pricing for all the zones
                    var zones = (from z in db.Zones where z.isActive == true select z).ToList();
                    var specialPackage = (from sp in db.SpecialPackages where sp.name == itemname select sp).FirstOrDefault();
                    Parallel.ForEach(zones, zone =>
                    {
                        AddGenericPackagPrice(1, itemPrice, zone.id, specialPackage.id, shipmentItemCategoryId, isbikeable, isvanable, istruckable);
                    });
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
                throw;
            }
        }

        public List<returnmsg> AddGenericPackagPrice(decimal weight, decimal price, int zoneId, int spkId, int shipmentItemCategoryId, bool isbikeable, bool isvanable, bool istruckable)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                SpecialPackagepricing datamodel = new SpecialPackagepricing();
                datamodel.weight = weight;
                datamodel.price = price;
                datamodel.zoneId = zoneId;
                datamodel.specialPackageId = spkId;
                datamodel.isActive = true;
                datamodel.DateCreated = DateTime.Now;
                datamodel.ShipmentItemCategoryId = shipmentItemCategoryId;
                datamodel.IsBikeable = isbikeable;
                datamodel.IsVanable = isvanable;
                datamodel.IsTruckable = istruckable;
                db.SpecialPackagepricings.InsertOnSubmit(datamodel);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
                throw;
            }
        }

        public List<returnmsg> AddVolumeRange(string code, int custId, int? lowerRange, int? upperRange, string description)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dataContext = new liblogisticsDataContext();
                var ctx = from v in dataContext.VolumeRanges
                          where v.Code == code && v.CustomerType == custId
                          select v;
                if (ctx.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Data already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    VolumeRange vr = new VolumeRange();
                    vr.Code = code;
                    vr.LowerRange = lowerRange;
                    vr.UpperRange = upperRange;
                    vr.Description = description;
                    vr.CustomerType = custId;
                    vr.isActive = true;
                    vr.DateCreated = DateTime.Now;
                    vr.DateModified = DateTime.Now;
                    dataContext.VolumeRanges.InsertOnSubmit(vr);
                    dataContext.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> AddWeightRange(string code, decimal? lowerRange, decimal? upperRange)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dataContext = new liblogisticsDataContext();
                var ctx = from w in dataContext.WeightRanges
                          where w.Code == code
                          select w;
                if (ctx.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Data already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    WeightRange wr = new WeightRange();
                    wr.Code = code;
                    wr.LowerRange = lowerRange;
                    wr.UpperRange = upperRange;
                    wr.IsActive = true;
                    wr.DateCreated = DateTime.Now;
                    wr.DateModified = DateTime.Now;
                    dataContext.WeightRanges.InsertOnSubmit(wr);
                    dataContext.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> UpdateEmployeeByAdmin(string UsId, string fname, string lname, int Gend, int locationId)
        {
            var rtmsg = new returnmsg();
            List<returnmsg> msg = new List<returnmsg>();
            try
            {
                List<Employee> emp = (from p in db.Employees
                                      join s in db.AspNetUsers on p.UserId equals s.Id
                                      where s.Id == UsId
                                      select p).ToList();
                if (emp.Count() > 0)
                {
                    foreach (Employee l in emp)
                    {
                        l.FirstName = fname;
                        l.LastName = lname;
                        l.Gender = Gend;
                        l.LocationId = locationId;
                        l.DateModified = DateTime.Now;
                    }

                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "User successfully updated";
                    msg.Add(rtmsg);
                    return msg;
                }
                else
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No user to update";
                    msg.Add(rtmsg);
                    return msg;
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                msg.Add(rtmsg);
                return msg;
            }
        }

        public List<returnmsg> UpdateEmployeeByUser(string UsId, int locationId)
        {
            var rtmsg = new returnmsg();
            List<returnmsg> msg = new List<returnmsg>();
            try
            {
                List<Employee> emp = (from p in _context.Employees
                                      join s in _context.AspNetUsers on p.UserId equals s.Id
                                      where s.Id == UsId
                                      select p).ToList();
                if (emp.Count() > 0)
                {
                    foreach (Employee l in emp)
                    {
                        l.LocationId = locationId;
                        l.DateModified = DateTime.Now;
                    }

                   // db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "User successfully updated";
                    msg.Add(rtmsg);
                    return msg;
                }
                else
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No user to update";
                    msg.Add(rtmsg);
                    return msg;
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                msg.Add(rtmsg);
                return msg;
            }
        }

        public List<returnmsg> UpdateWaybill(decimal vAmount, decimal PaymentAmount, string vwaybill, string vtype, string vdeparture, string vdestination, string TotalAmount, string PaymentMethod, string ReceiverPhoneNumber, string ReceiverName, string paymentStatus)
        {
            var rtmsg = new returnmsg();
            List<returnmsg> msg = new List<returnmsg>();
            try
            {


                List<Shipment> shp = (from p in _context.Shipments
                                      where p.Waybill == vwaybill
                                      select p).ToList();
                if (shp.Count() > 0)
                {
                    if (vtype == "1")
                    {
                        foreach (Shipment s in shp)
                        {
                            s.RefundAmount = vAmount;
                            s.TotalTopay = PaymentAmount;
                            s.GrandTotal = PaymentAmount;
                            s.IsRefund = true;
                        }

                    }
                    else if (vtype == "2")
                    {
                        foreach (Shipment s in shp)
                        {
                            if (paymentStatus != "")
                            {
                                var paymentStatusEnum = (PaymentStatus)Enum.Parse(typeof(PaymentStatus), paymentStatus);
                                s.PaymentStatus = (int)paymentStatusEnum;
                            }
                            if (vdeparture != "")
                            {
                                s.departureLocationId = Convert.ToInt32(vdeparture);
                            }
                            if (vdestination != "")
                            {
                                s.destinationId = Convert.ToInt32(vdestination);
                            }
                            if (ReceiverPhoneNumber != "")
                            {
                                s.receiverPhoneNumber = ReceiverPhoneNumber;
                            }
                            if (ReceiverName != "")
                            {
                                s.receiverName = ReceiverName;
                            }
                            if (TotalAmount != "")
                            {
                                s.totalTopay = Convert.ToDecimal(TotalAmount);
                                s.grandTotal = Convert.ToDecimal(TotalAmount);
                                s.vat = Convert.ToDecimal(TotalAmount) * Convert.ToDecimal(0.07);
                            }
                            if (PaymentMethod == "Credit")
                            {
                                s.paymentMethod = PaymentMethod;
                                s.IsCredit = true;
                            }
                            if (PaymentMethod == "Cash")
                            {
                                s.paymentMethod = PaymentMethod;
                                s.IsCredit = null;
                            }


                        }

                    }

                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Waybill successfully updated";
                    msg.Add(rtmsg);
                    return msg;
                }
                else
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No Waybill to update";
                    msg.Add(rtmsg);
                    return msg;
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                msg.Add(rtmsg);
                return msg;
            }
        }
        public List<returnmsg> UpdateEmployee(string UsId, string fname, string lname, int Gend)
        {
            var rtmsg = new returnmsg();
            List<returnmsg> msg = new List<returnmsg>();

            try
            {
                List<Employee> emp = (from p in dt.Employees
                                      join s in dt.AspNetUsers on p.UserId equals s.Id
                                      where s.Id == UsId
                                      select p).ToList();
                if (emp.Count() > 0)
                {
                    foreach (Employee l in emp)
                    {
                        l.FirstName = fname;
                        l.LastName = lname;
                        l.Gender = Gend;
                        l.DateModified = DateTime.Now;
                    }

                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "User successfully updated";
                    msg.Add(rtmsg);
                    return msg;
                }
                else
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No user to update";
                    msg.Add(rtmsg);
                    return msg;
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                msg.Add(rtmsg);
                return msg;
            }
        }

        public List<returnmsg> UpdateCoupon(CouponModel model)
        {
            return UpdateCoupon(model.Id, model.CouponCode, model.CouponType, model.CouponValue, model.CreationTime, model.Duration, model.DurationType, model.IsUsed, model.Validity, model.VoucherNote, model.TotalUsage, model.VoucherType, model.CouponValueLimit, model.StartDate, model.EndDate, model.PhoneNumber, model.LastModificationTime, model.LastModifierUserId, model.CreatorUserId, model.DateUsed);
        }
        public List<returnmsg> UpdateCoupon(Guid id, string couponCode, int coupontype, decimal couponvalue, DateTime creationtime, int duration, int durationType, bool isused, bool validity, string vouchernote, int? totalusage, int? vouchertype, decimal? couponvaluelimit, DateTime? startdate = null, DateTime? enddate = null, string phonenumber = null, DateTime? lastmodifiedtime = null, string lastmodifieruserid = null, string creatoruserid = null, DateTime? dateused = null)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();

                Coupon coupon = (from p in dt.Coupons
                                 where p.Id == id
                                 select p).ToList().FirstOrDefault();
                if (coupon == null)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No coupon to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    coupon.CouponCode = couponCode;
                    coupon.CouponType = coupontype;
                    coupon.CouponValue = couponvalue;
                    coupon.CreationTime = creationtime;
                    coupon.Duration = duration;
                    coupon.DurationType = durationType;
                    coupon.IsUsed = isused;
                    coupon.Validity = validity;
                    coupon.PhoneNumber = phonenumber;
                    coupon.LastModificationTime = DateTime.Now;
                    coupon.VoucherNote = vouchernote;
                    coupon.LastModifierUserId = lastmodifieruserid;
                    coupon.CreatorUserId = creatoruserid;
                    coupon.CouponValueLimit = (decimal)couponvaluelimit;
                    coupon.VoucherType = (int)vouchertype;
                    coupon.DateUsed = dateused;
                    coupon.TotalUsage = (int)totalusage;
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Coupon successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }


        public List<returnmsg> UpdateLocation(locationModel model)
        {
            return UpdateLocation(model.id, model.name, model.stateId, model.Locationtype, model.Address, model.Image, model.Code, model.Latitude, model.Longitude, model.ContactPerson, model.ContactPerson, model.IsCommision);
        }
        public List<returnmsg> UpdateLocation(int locationId, string locationname, int stateid, int Ltype, string address = null, string image = null, string code = null, float? latitude = null, float? longitude = null, string contactpersonname = null, string contactpersonphone = null, bool? isCommissioned = null)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                //List<Location> locations = (from p in dt.Locations
                //                            where p.id == locationId
                //                            select p).ToList();
                Location location = (from p in dt.Locations
                                     where p.id == locationId
                                     select p).ToList().FirstOrDefault();
                if (location == null)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No location to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    location.locationName = locationname;
                    location.stateId = stateid;
                    location.DateModified = DateTime.Now;
                    location.LocationType = Ltype;
                    //added
                    location.Address = address;
                    location.Image = image;
                    location.Code = code;
                    location.Latitude = latitude;
                    location.Longitude = longitude;
                    location.ContactPerson = contactpersonname;
                    location.ContactPersonNo = contactpersonphone;
                    location.IsCommision = isCommissioned;

                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Location successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateSpecialPrice(int itemId, string name, decimal price)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<SpecialShipmentPrice> items = (from p in dt.SpecialShipmentPrices
                                                    where p.id == itemId
                                                    select p).ToList();
                if (items.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No Item to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (SpecialShipmentPrice l in items)
                    {
                        l.ShipmentItem = name;
                        l.PricePerKG = price;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Location successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }



        public List<returnmsg> Updatezone(int itemId, string zoneName, string description = null)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<Zone> zones = (from p in dt.Zones
                                    where p.id == itemId
                                    select p).ToList();
                if (zones.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No zone to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Zone l in zones)
                    {
                        l.name = zoneName;
                        l.DateModified = DateTime.Now;
                        l.ZoneDescription = description;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> UpdateVolumeRange(int volumeRangeId, string volumeCode, int volumeLowerRange, int volumeUpperRange, string volumeDescription)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext vr = new liblogisticsDataContext();
                List<VolumeRange> volumeRanges = (from v in vr.VolumeRanges
                                                  where v.Id == volumeRangeId
                                                  select v).ToList();
                if (volumeRanges.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No volume range to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (VolumeRange l in volumeRanges)
                    {
                        l.Code = volumeCode;
                        l.LowerRange = volumeLowerRange;
                        l.UpperRange = volumeUpperRange;
                        l.Description = volumeDescription;
                        l.DateModified = DateTime.Now;
                    }
                    vr.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateWeightRange(int weightRangeId, string weightCode, decimal weightLowerRange, decimal weightUpperRange)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext wr = new liblogisticsDataContext();
                List<WeightRange> weightRanges = (from w in wr.WeightRanges
                                                  where w.Id == weightRangeId
                                                  select w).ToList();
                if (weightRanges.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No weight range to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (WeightRange l in weightRanges)
                    {
                        l.Code = weightCode;
                        l.LowerRange = weightLowerRange;
                        l.UpperRange = weightUpperRange;
                        l.DateModified = DateTime.Now;
                    }
                    wr.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> UpdateDeliveryoptionPrice(int itemId, int deliverytypeId, decimal itemprice, bool isactive, int zoneId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<DeliveryTypePrice> libmodl = (from p in dt.DeliveryTypePrices
                                                   where p.id == itemId
                                                   select p).ToList();
                if (libmodl.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No option to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (DeliveryTypePrice l in libmodl)
                    {
                        l.deliveryTypeId = deliverytypeId;
                        l.zoneId = zoneId;
                        l.price = itemprice;
                        l.isActive = isactive;
                        l.DateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }



        public List<returnmsg> UpdateServiceOptionPrice(int itemId, int servicetypeId, decimal itemprice, bool isactive, int zoneId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<ServiceTypePrice> libmodl = (from p in dt.ServiceTypePrices
                                                  where p.ID == itemId
                                                  select p).ToList();
                if (libmodl.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No option to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ServiceTypePrice l in libmodl)
                    {
                        l.ServiceTypeId = servicetypeId;
                        l.ZoneId = zoneId;
                        l.Price = itemprice;
                        l.IsActive = isactive;
                        l.DateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }


        public List<returnmsg> UpdateZoneMapping(int itemId, int departId, int destinationId, int zoneId, bool isactive)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<ZoneMapping> libmodl = (from p in dt.ZoneMappings
                                             where p.id == itemId
                                             select p).ToList();
                if (libmodl.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No option to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ZoneMapping l in libmodl)
                    {
                        l.departuteId = departId;
                        l.destinationId = destinationId;
                        l.isActive = isactive;
                        l.zoneId = zoneId;
                        l.DateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateZonePricing(int itemId, int zoneId, decimal price, decimal weight, bool isactive)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<ZonePrice> libmodl = (from p in dt.ZonePrices
                                           where p.id == itemId
                                           select p).ToList();
                if (libmodl.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No option to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ZonePrice l in libmodl)
                    {
                        l.zoneId = zoneId;
                        l.price = price;
                        l.weight = weight;
                        l.isActive = isactive;
                        l.DateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateZonePerKG(int itemId, int zoneId, decimal price)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<ZonePricePerKg> libmodl = (from p in dt.ZonePricePerKgs
                                                where p.id == itemId
                                                select p).ToList();
                if (libmodl.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No option to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ZonePricePerKg l in libmodl)
                    {
                        l.zoneId = zoneId;
                        l.pricePerKg = price;
                        l.dateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateVehicleModel(int itemId, int makeId, string makeName)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<Vehiclemodel> libmodl = (from p in dt.Vehiclemodels
                                              where p.id == itemId
                                              select p).ToList();
                if (libmodl.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No option to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Vehiclemodel l in libmodl)
                    {
                        l.makeId = makeId;
                        l.name = makeName;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> UpdateCustomerType(int itemId, string itemname, bool isActive)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<CustomerType> zones = (from p in dt.CustomerTypes
                                            where p.id == itemId
                                            select p).ToList();
                if (zones.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No delivery type to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (CustomerType l in zones)
                    {
                        l.CusType = itemname;
                        l.isActive = isActive;
                        l.DateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> Updateinsurance(int itemId, string itemname, decimal Invalue)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<insurance> dtx = (from p in dt.insurances
                                       where p.id == itemId
                                       select p).ToList();
                if (dtx.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No insurance to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (insurance l in dtx)
                    {
                        l.name = itemname;
                        l.value = Invalue;
                        l.dateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdatePackagePricing(int itemId, string itemname, decimal price, bool isactive)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<PackagingPricing> dtx = (from p in dt.PackagingPricings
                                              where p.id == itemId
                                              select p).ToList();
                if (dtx.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No packaging price to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (PackagingPricing l in dtx)
                    {
                        l.name = itemname;
                        l.price = price;
                        l.dateModified = DateTime.Now;
                        l.isActive = isactive;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateVehicle(int itemId, string RegNum, string EngineNum, int ModelId, bool isactive, int companyInfoId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<Vehicle> dtx = (from p in dt.Vehicles
                                     where p.id == itemId
                                     select p).ToList();
                if (dtx.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No vehicle to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Vehicle l in dtx)
                    {
                        l.regNumber = RegNum;
                        l.modelId = ModelId;
                        l.EngineNumber = EngineNum;
                        l.DateModified = DateTime.Now;
                        l.isActive = isactive;
                        l.CompanyInfoId = companyInfoId;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateDriver(int itemId, string drivername, string phone, bool isactive, int comapanyInfoId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<Driver> dtx = (from p in dt.Drivers
                                    where p.Id == itemId
                                    select p).ToList();
                if (dtx.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No driver to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Driver l in dtx)
                    {
                        l.DriverName = drivername;
                        l.DriverPhone = phone;
                        l.DateModified = DateTime.Now;
                        l.CompanyInfoId = comapanyInfoId;
                        l.IsActive = isactive;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> UpdategenericPackage(int itemId, string itemname, decimal weight, bool isactive)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<SpecialPackage> dtx = (from p in dt.SpecialPackages
                                            where p.id == itemId
                                            select p).ToList();
                if (dtx.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No package to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (SpecialPackage l in dtx)
                    {
                        l.name = itemname;
                        l.itemWeight = weight;
                        l.DateModified = DateTime.Now;
                        l.isActive = isactive;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public List<returnmsg> UpdategenericPackagePrice(int itemId, decimal itemWeight, int itemzoneId, int specialPkId, decimal price, bool isactive, int shipmentItemCategoryId, bool isbikeable, bool isvanable, bool istruckable)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<SpecialPackagepricing> dtx = (from p in dt.SpecialPackagepricings
                                                   where p.id == itemId
                                                   select p).ToList();
                if (dtx.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No package  to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (SpecialPackagepricing l in dtx)
                    {
                        l.zoneId = itemzoneId;
                        l.weight = itemWeight;
                        l.specialPackageId = specialPkId;
                        l.DateModified = DateTime.Now;
                        l.price = price;
                        l.isActive = isactive;
                        l.ShipmentItemCategoryId = shipmentItemCategoryId;
                        l.IsBikeable = isbikeable;
                        l.IsVanable = isvanable;
                        l.IsTruckable = istruckable;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateDeliveryType(int itemId, string itemname, bool isactive, int custType)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<Deliverytype> zones = (from p in dt.Deliverytypes
                                            where p.id == itemId
                                            select p).ToList();
                if (zones.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No delivery type to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Deliverytype l in zones)
                    {
                        l.name = itemname;
                        l.CustomerType = custType;
                        l.isActive = isactive;
                        l.DateModified = DateTime.Now;
                    }
                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        public bool deleteSpecialPriceById(int itemId)
        {
            try
            {
                using (var ctx = new liblogisticsDataContext())
                {
                    var x = (from y in ctx.SpecialShipmentPrices
                             where y.id == itemId
                             select y).FirstOrDefault();
                    ctx.SpecialShipmentPrices.DeleteOnSubmit(x);
                    ctx.SubmitChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public List<returnmsg> deletelocationById(int locationId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Location> locations = (from p in db.Locations
                                            where p.id == locationId
                                            select p).ToList();
                if (locations.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No location to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Location l in locations)
                    {
                        l.DateModified = DateTime.Now;
                        l.IsDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Location successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> DeleteCustTypeById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<CustomerType> itemDataModel = (from p in db.CustomerTypes
                                                    where p.id == itemId
                                                    select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (CustomerType l in itemDataModel)
                    {
                        l.DateModified = DateTime.Now;
                        l.isDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> SignOffManifest(int itemId, decimal disptach, int driverId, int vehicleId, string dispatchedby, int departureId = 0, int destinationId = 0)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Manifest> itemDataModel = (from p in db.Manifests
                                                where p.ManifestId == itemId
                                                select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No manifest to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Manifest l in itemDataModel)
                    {
                        if (departureId != 0) { l.DepartId = departureId; }
                        if (destinationId != 0) { l.DestinationId = destinationId; }
                        l.DateModified = DateTime.Now;
                        l.DispatchedById = dispatchedby;
                        l.DispatchFee = disptach;
                        l.IsDispatched = true;
                        l.VehicleId = vehicleId;
                        l.DriverId = driverId;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Manifest updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> deleteInsuranceById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<insurance> itemDataModel = (from p in db.insurances
                                                 where p.id == itemId
                                                 select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (insurance l in itemDataModel)
                    {
                        l.dateModified = DateTime.Now;
                        l.isDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public bool UpdateIManifest(string manifestnumber, string userName)
        {
            using (var db = new liblogisticsDataContext())
            {
                Manifest shpmt = new Manifest();
                try
                {
                    List<Manifest> manifests = (from p in db.Manifests
                                                where p.ManifestNumber == manifestnumber
                                                select p).ToList();
                    if (manifests.Count() > 0)
                    {
                        foreach (Manifest l in manifests)
                        {
                            l.IsReceived = true;
                            l.DateModified = DateTime.Now;
                            l.IsReceivedBy = userName;
                            l.IsReceivedDate = DateTime.Now;
                        }
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
        }

        public List<returnmsg> AddGroupShipmentUpdate(string groupnumb)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                GroupshipmentList datamodel = new GroupshipmentList();
                datamodel.groupnumber = groupnumb;
                db.GroupshipmentLists.InsertOnSubmit(datamodel);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        public List<returnmsg> UpdateshipmentStatus(string Waybill, string location, string status)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                ShipmentTracking datamodel = new ShipmentTracking();
                datamodel.Waybill = Waybill;
                datamodel.Location = location;
                datamodel.Status = status;
                datamodel.IsDeleted = false;
                datamodel.DateCreated = DateTime.Now;
                datamodel.DateModified = DateTime.Now;
                db.ShipmentTrackings.InsertOnSubmit(datamodel);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        public List<returnmsg> deletePkpricingById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<PackagingPricing> itemDataModel = (from p in db.PackagingPricings
                                                        where p.id == itemId
                                                        select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (PackagingPricing l in itemDataModel)
                    {
                        l.dateModified = DateTime.Now;
                        l.isDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> deleteGenericPackageById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<SpecialPackage> itemDataModel = (from p in db.SpecialPackages
                                                      where p.id == itemId
                                                      select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (SpecialPackage l in itemDataModel)
                    {
                        l.DateModified = DateTime.Now;
                        l.IsDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> deletDriver(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Driver> itemDataModel = (from p in db.Drivers
                                              where p.Id == itemId
                                              select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Driver l in itemDataModel)
                    {
                        l.DateModified = DateTime.Now;
                        l.Isdeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }




        public List<returnmsg> DeactivateUserById(string userId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Employee> itemDataModel = (from p in db.Employees
                                                where p.UserId == userId
                                                select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Employee l in itemDataModel)
                    {
                        l.ActiveStatus = false;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "User deactivated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<returnmsg> ActivateUserById(string userId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Employee> itemDataModel = (from p in db.Employees
                                                where p.UserId == userId
                                                select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to activate";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Employee l in itemDataModel)
                    {
                        l.ActiveStatus = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "User deactivated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GetuserId(string useremail)
        {
            liblogisticsDataContext liblogisticsData = new liblogisticsDataContext();
            var itdata = (from p in liblogisticsData.AspNetUsers
                          where p.Email == useremail
                          select p.Id).FirstOrDefault();
            return itdata;
        }


        public List<UserList> GetUsersInLocation(int locationId)
        {
            liblogisticsDataContext liblogisticsData = new liblogisticsDataContext();
            var itdata = (from p in liblogisticsData.AspNetUsers
                          join s in liblogisticsData.Employees on p.Id equals s.UserId
                          join f in liblogisticsData.Locations on s.LocationId equals f.id
                          where f.id == locationId
                          select new UserList
                          {
                              Id = p.Email,
                              Name = s.FirstName + " " + s.LastName
                          }).ToList();
            return itdata;
        }

        public string GetuserPhone(string useremail)
        {
            liblogisticsDataContext liblogisticsData = new liblogisticsDataContext();
            var itdata = (from p in liblogisticsData.AspNetUsers
                          where p.Email == useremail
                          select p.PhoneNumber).FirstOrDefault();
            return itdata;
        }

        public bool getUserStatus(string userId)
        {

            var itemDataModel = (from p in db.Employees
                                 where p.UserId == userId
                                 select p.ActiveStatus).FirstOrDefault();
            return itemDataModel;
        }

        public List<returnmsg> deleteDeliveryTypeById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Deliverytype> itemDataModel = (from p in db.Deliverytypes
                                                    where p.id == itemId
                                                    select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Deliverytype l in itemDataModel)
                    {
                        l.DateModified = DateTime.Now;
                        l.IsDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> deleteServicePriceById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<ServiceTypePrice> itemDataModel = (from p in db.ServiceTypePrices
                                                        where p.ID == itemId
                                                        select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ServiceTypePrice s in itemDataModel)
                    {
                        s.DateModified = DateTime.Now;
                        s.IsDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }







        public List<returnmsg> deleteZoneRouteById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<ZoneMapping> itemDataModel = (from p in db.ZoneMappings
                                                   where p.id == itemId
                                                   select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ZoneMapping l in itemDataModel)
                    {
                        l.DateModified = DateTime.Now;
                        l.isDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<returnmsg> deleteZonePriceById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<ZonePrice> itemDataModel = (from p in db.ZonePrices
                                                 where p.id == itemId
                                                 select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ZonePrice l in itemDataModel)
                    {
                        l.DateModified = DateTime.Now;
                        l.isDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> deleteZonePerKGById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<ZonePricePerKg> itemDataModel = (from p in db.ZonePricePerKgs
                                                      where p.id == itemId
                                                      select p).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ZonePricePerKg l in itemDataModel)
                    {
                        l.dateModified = DateTime.Now;
                        l.isDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> deleteModelById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var ctx = new liblogisticsDataContext())
                {
                    var x = (from y in ctx.Vehiclemodels
                             where y.id == itemId
                             select y).FirstOrDefault();
                    ctx.Vehiclemodels.DeleteOnSubmit(x);
                    ctx.SubmitChanges();
                }
                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "Data successfully deleted";
                retmsgs.Add(rtmsg);
                return retmsgs;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<returnmsg> deletezoneById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Zone> zones = (from p in db.Zones
                                    where p.id == itemId
                                    select p).ToList();
                if (zones.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No Zone to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Zone l in zones)
                    {
                        l.DateModified = DateTime.Now;
                        l.IsDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Location successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<returnmsg> AddCustomer(string CustPhone, string fname, string ctemail, int Gender, string address, string codeFromReferrer, DateTime? DateofBirth, string userId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();

            List<customer> cts = new List<customer>();
            cts = (from p in db.customers
                   where p.email == ctemail
                   select p).ToList();
            try
            {

                if (cts.Count() == 0)
                {
                    customer ctsdbx = new customer();
                    ctsdbx.phoneNumber = CustPhone;
                    ctsdbx.firstName = fname;
                    ctsdbx.gender = Gender;
                    ctsdbx.email = ctemail;
                    ctsdbx.address = address;
                    ctsdbx.DateCreated = DateTime.Now;
                    ctsdbx.ReferralCode = RandomDigits(12);
                    ctsdbx.CodeFromReferrer = codeFromReferrer;
                    ctsdbx.DateofBirth = DateofBirth;
                    ctsdbx.UserId = userId;
                    db.customers.InsertOnSubmit(ctsdbx);
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "successfully Added";
                    retmsgs.Add(rtmsg);

                    //coupon would be created for the referrer of the customer
                    if (!string.IsNullOrEmpty(codeFromReferrer))
                    {
                        //get the referrer and give him coupon
                        var referrer = GetCustomerByReferralCode(codeFromReferrer);

                        if (referrer != null)
                        {
                            //create coupon for this person
                            CreateCoupon(new Coupon { PhoneNumber = referrer.phoneNumber });
                        }
                    }

                    return retmsgs;
                }
                else
                {
                    //update the customer table
                    var c = cts.FirstOrDefault();
                    c.phoneNumber = CustPhone;
                    c.firstName = fname;
                    c.gender = Gender;
                    c.address = address;
                    c.CodeFromReferrer = codeFromReferrer;
                    c.DateofBirth = DateofBirth;
                    c.UserId = userId;

                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.errormsg = "Not a new Customer";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public int GetCustomerId(string ctPhone)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.customers
                            where l.phoneNumber == ctPhone
                            select l.id).FirstOrDefault();
                return dtcx;
            }
        }
        //public List<CustomerModel> GetCustomerDetailsByPhoneNumber(string ctPhone)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var dtcx = (from l in db.customers
        //                    where l.phoneNumber == ctPhone
        //                    select new CustomerModel
        //                    {
        //                        address = l.address,
        //                        email = l.email,
        //                        firstName = l.firstName,
        //                        lastName = l.lastName,
        //                        gender = l.gender,
        //                        phoneNumber = l.phoneNumber,
        //                        id = l.id,
        //                        DateCreated = l.DateCreated,
        //                        CodeFromReferrer = l.CodeFromReferrer,
        //                        ReferralCode = l.ReferralCode,
        //                    }).GroupBy(t => t.phoneNumber).Select(grp => grp.FirstOrDefault()).ToList();
        //        return dtcx.ToList();
        //    }
        //}
        public CustomerModel GetCustomerDetails(string email)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.customers
                            where l.email == email
                            select new CustomerModel
                            {
                                address = l.address,
                                email = l.email,
                                firstName = l.firstName,
                                lastName = l.lastName,
                                gender = l.gender,
                                phoneNumber = l.phoneNumber,
                                id = l.id,
                                DateCreated = l.DateCreated,
                                CodeFromReferrer = l.CodeFromReferrer,
                                ReferralCode = l.ReferralCode,
                                DateofBirth = l.DateofBirth
                            }).FirstOrDefault();
                return dtcx;
            }
        }
        public CustomerModel GetCustomerByReferralCode(string referralCode)
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            var customer = (from l in db.customers
                            where l.ReferralCode == referralCode
                            select new CustomerModel
                            {
                                address = l.address,
                                email = l.email,
                                firstName = l.firstName,
                                lastName = l.lastName,
                                gender = l.gender,
                                phoneNumber = l.phoneNumber,
                                id = l.id,
                                DateCreated = l.DateCreated,
                                CodeFromReferrer = l.CodeFromReferrer,
                                ReferralCode = l.ReferralCode,
                            }).FirstOrDefault();
            return customer;
        }
        public int GetStateId(int locationId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.Locations
                            where l.id == locationId
                            select l.stateId).FirstOrDefault();
                return dtcx;
            }
        }

        public int GetLoggedInUserStateId(string userId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.Employees
                            join s in db.Locations on l.LocationId equals s.id
                            where l.UserId == userId
                            select s.stateId).FirstOrDefault();
                return dtcx;
            }
        }

        public List<returnmsg> createMerchantShipment(MerchantShipmentDTO model)
        {
            //save each of 
            return createMerchantShipment(model.MerchantID, model.TotalGrandTotal, model.TotalInsured, model.TotalVat);
        }

        public List<returnmsg> createMerchantShipment(int MerchantID, decimal TotalGrandTotal, decimal TotalInsured, decimal TotalVat)
        {


            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                MerchantShipment merchantshipment = new MerchantShipment();

                merchantshipment.MerchantID = MerchantID;
                merchantshipment.TotalGrandTotal = TotalGrandTotal;
                merchantshipment.TotalInsured = TotalInsured;
                merchantshipment.TotalVat = TotalVat;
                merchantshipment.DateCreated = DateTime.Now;
                merchantshipment.IsDeleted = false;

                _context.MerchantShipments.InsertOnSubmit(merchantshipment);
                _context.SubmitChanges();

                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }
        public List<returnmsg> createShipment(ShipmentModel model)
        {
            //save each of 
            return createShipment(model.Waybill, model.senderphone, model.PaymentStatus, model.typeofCustomer,
         model.customerId, model.departureLocationId, model.destinationId, model.receiverStateId, model.receiverName, model.receiverPhoneNumber, model.receiverEmail, model.receiverAddress,
          model.deliveryTypeId, model.PickupOptions, model.SpecifiedDateofDelivery, model.TotalWeight, model.TotalPrice, model.isCashOnDelivery,
         model.createdBy, model.ValueIsDeceleared, model.deClearedValue, model.isInsured, model.vat, model.insuranceAmount, model.packagingfee,
         model.totalTopay, model.paymentMethod, model.description, model.senderAddress, model.senderStateId, model.DateCreated, model.specialnote,
         model.BookingRefCode, model.PackagingType, model.packagingQuantity, model.UserId, (AccountType)model.AccountType, model.senderLat,
         model.senderLng, model.receiverLat, model.receiverLng, model.senderEmail, model.SenderActualAddress, model.ReceiverActualAddress,
         model.senderName, model.SenderFirstName, model.SenderLastName, model.ReceiverFirstName, model.ReceiverLastName, model.ItemQuantity, model.MerchantShipmentID, model.CustomerPlatform, model.IsHomeDelivery, model.POSReferenceNO, model.TransferName, model.TransferDate,
         model.ServiceType);
        }

        public List<returnmsg> createShipment(string waybillNumber, string senderphone, int paymentStatus, int? customerType,
         int? customerId, int? departureLocationId, int destinationId, int? receiverStateId, string ReceiverName, string ReceiverPhone, string ReceiverEmail, string ReceiverAddress,
          int? DeliveryTypeId, int? PickupOption, string SpecifiedDeliveryDate, decimal? TotalWeight, decimal? TotalPrice, bool isCashonDelivery,
         string CreatedBy, bool ValueIsDeceleared, decimal? ValueDeclearedAmt, bool IsInsured, decimal? VatAmt, decimal? insuranceAmt, decimal? pkfee,
         decimal TotalToPay, string PaymentMethod, string ItemDescription, string SenderAddress, int SenderStateId,
         DateTime CreationDate, string specialnote, string BookingRefCode, string PackagingType,
         int? packagingQuantity, string userId, AccountType accountType, float? senderLat, float? senderLng,
         float? receiverLat, float? receiverLng, string senderEmail, string SenderActualAddress, string ReceiverActualAddress,
         string senderName, string SenderFirstName, string SenderLastName, string ReceiverFirstName, string ReceiverLastName, decimal? ItemQuantities, int? merchantShipmentID, int? customerPlatform, bool isHomeDelivery, string posReferenceNO, string transferName, string transferDate, int? serviceType)
        {
            //string smsmsg = $"Your shipment to {ReceiverName} with waybill number: {waybillNumber} has been created successfully. Track your shipment on https://bit.ly/3eMvrs9 | call us on 09062547031";
            //string smsmsg = $"Hi {senderName} Your shipment to {ReceiverName} with waybill number {waybillNumber} has successfully been created.Track your shipment at http://libmotexpress.com/tracking.html or 09062547031.";
            //string smsmsg = $"Hi {senderName} Your shipment to {GetLocationName(destinationId)} with waybill number {waybillNumber} for {ReceiverName} has successfully been created.Your Value Declared is {ValueDeclearedAmt} and Total Paid is {TotalToPay} Track your shipment at http://libmotexpress.com, share link and waybill number with receiver.";


            //          string barCodeImageLink = GenerateBarCode(waybillNumber, CreationDate, CreatedBy, TotalPrice);

            string smsmsg = $"Hi {senderName} Your shipment {waybillNumber} to {ReceiverName} with cost " +
                $"{TotalToPay} was created & value Decleared is {ValueDeclearedAmt}.Track your shipment at libmotexpress.com share details with receiver.";

            string intraStateMsg = $"Hi {senderName} your shipment with {waybillNumber} to {ReceiverName} will be delivered in 24hrs at the cost of " + $"{TotalToPay}.";

            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                Shipment shp = new shipment();
                if (PaymentMethod == "Credit")
                {
                    shp.IsCredit = true;
                }
                shp.Waybill = waybillNumber;
                shp.customerId = null;
                shp.PaymentStatus = paymentStatus;
                shp.typeofCustomer = customerType;
                shp.departureLocationId = departureLocationId;
                shp.destinationId = destinationId;
                shp.receiverStateId = receiverStateId;
                shp.receiverName = ReceiverFirstName + " " + ReceiverLastName;//ReceiverName ? ReceiverName : string.Concat(ReceiverFirstName, ReceiverLastName);
                shp.receiverPhoneNumber = ReceiverPhone;
                shp.receiverEmail = ReceiverEmail;
                shp.receiverAddress = ReceiverAddress;
                shp.deliveryTypeId = DeliveryTypeId;
                shp.PickupOptions = PickupOption;
                shp.SpecifiedDateofDelivery = SpecifiedDeliveryDate;
                shp.itemsWeight = TotalWeight;
                shp.packagingfee = pkfee;
                shp.isCashOnDelivery = isCashonDelivery;
                shp.grandTotal = TotalPrice;
                shp.createdBy = CreatedBy;
                shp.valueIsDecleared = ValueIsDeceleared;
                shp.deClearedValue = ValueDeclearedAmt;
                shp.isInsured = IsInsured;
                shp.vat = VatAmt;
                shp.insuranceAmount = insuranceAmt;
                shp.totalTopay = TotalToPay;
                shp.paymentMethod = PaymentMethod;
                shp.description = ItemDescription;
                shp.senderAddress = SenderAddress;
                shp.senderStateId = SenderStateId;
                shp.DateCreated = DateTime.Now;
                shp.specialnote = specialnote;
                shp.BookingRefCode = BookingRefCode;
                shp.PackagingType = PackagingType;
                shp.PackageQuantity = packagingQuantity;
                shp.UserId = userId;
                shp.AccountType = (int?)accountType;
                shp.SenderPhoneNumber = senderphone;
                shp.SenderEmail = senderEmail;
                shp.IsCollected = (int)ShipmentCollectionStatus.NotCollected;
                shp.SenderActualAddress = SenderActualAddress;
                shp.ReceiverActualAddress = ReceiverActualAddress;
                shp.SenderName = SenderFirstName + " " + SenderLastName; //senderName ? senderName : string.Concat(SenderFirstName, SenderLastName);
                shp.SenderFirstName = SenderFirstName;
                shp.SenderLastName = SenderLastName;
                shp.ReceiverFirstName = ReceiverFirstName;
                shp.ReceiverLastName = ReceiverLastName;
                shp.ItemQuantity = ItemQuantities;
                shp.MerchantShipmentID = merchantShipmentID;
                shp.CustomerPlatform = customerPlatform;
                shp.IsHomeDelivery = isHomeDelivery;
                shp.POSReferenceNO = posReferenceNO;
                shp.TransferName = transferName;
                shp.TransferDate = transferDate;
                shp.ServiceType = serviceType;
                //shp.BarCodeImageLink = barCodeImageLink;

                shp.IsCollected = 0;//Not collected
             //   db.shipments.InsertOnSubmit(shp);
              //  db.SubmitChanges();

                UpdateshipmentStatus(waybillNumber, db.Locations.Where(l => l.id == departureLocationId).FirstOrDefault()?.locationName, LibmotExpressConstants.ShipmentTrackingStatus.ShipmentProcessing /*"Shipment Processing"*/);

                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Added";
                try
                {
                    if (accountType != AccountType.Android && accountType != AccountType.IOS && SenderStateId != receiverStateId)
                    {

                        List<string> num = new List<string>();

                        num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + senderphone);

                        RequestSMS sms = new RequestSMS();
                        sms.id = Guid.NewGuid().ToString();
                        sms.to = num;
                        sms.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                        sms.body = smsmsg;
                        KonnectAPI konnectAPI = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                        konnectAPI.SendSMS(sms);

                        //ogosmsfunction
                        //SendSmsb(senderphone, smsmsg);
                    }
                    else
                    {
                        List<string> num2 = new List<string>();

                        num2.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + senderphone);

                        RequestSMS sms = new RequestSMS();
                        sms.id = Guid.NewGuid().ToString();
                        sms.to = num2;
                        sms.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                        sms.body = intraStateMsg;
                        KonnectAPI konnectAPI = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                        konnectAPI.SendSMS(sms);
                    }

                }
                catch (Exception)
                {
                };
                retmsgs.Add(rtmsg);

                if (accountType != null && (accountType == AccountType.Android || accountType == AccountType.IOS))
                {
                    var spdata = new ShipmentModel
                    {
                        AccountType = (int?)accountType,
                        Waybill = waybillNumber,
                        senderAddress = SenderAddress,
                        receiverAddress = ReceiverAddress,
                        departureLocationId = departureLocationId,
                        destinationId = destinationId,
                        senderLat = senderLat,
                        senderLng = senderLng,
                        receiverLat = receiverLat,
                        receiverLng = receiverLng
                    };
                    //ShipmentParcel(accountType, waybillNumber, SenderAddress, destinationId, senderLat, senderLng, receiverLat, receiverLng);
                    ShipmentParcel(spdata);
                }
                #region Send Transaction Email
                string dataFile = HostingEnvironment.ApplicationPhysicalPath + "messaging\\transaction-email.html";
                var replacement = new StringDictionary
                {
                    ["WayBillNumber"] = waybillNumber,
                    ["RecieverName"] = ReceiverName,
                    ["DeclaredValue"] = ValueDeclearedAmt.ToString(),
                };

                string emailsubject = $"Thank you for choosing Libmot Express.";

                var mail = new Mail(System.Configuration.ConfigurationManager.AppSettings["UserName"], emailsubject, senderEmail)
                {
                    BodyIsFile = true,
                    BodyPath = dataFile
                };

                Task.Run(async () =>
                {
                    await SmtpEmailService.SendMailAsync(mail, replacement);
                });
                //var f = SmtpEmailService.SendMailAsync(mail, replacement).Result;
                #endregion

                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }
        //public List<returnmsg> ShipmentParcel(AccountType accountType, string waybill, string departureAddress, int destinationId, float? senderLat = null, float? senderLng = null, float? receiverLat = null, float? receiverLng = null)
        public List<returnmsg> ShipmentParcel(ShipmentModel model)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                var user = Thread.CurrentPrincipal;
                var checkForWaybill = (from spcl in db.ShipmentParcels where spcl.Waybill == model.Waybill select spcl)?.FirstOrDefault();
                if (checkForWaybill != null)
                {
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Already exist!!";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                var destinationLocation = (from l in db.Locations where l.id == model.destinationId select l).FirstOrDefault();
                var departureLocation = (from l in db.Locations where l.id == model.departureLocationId select l).FirstOrDefault();
                string dropOffLocation = null;
                dropOffLocation = model.receiverAddress;
                int senderStateId = GetUserStateId((int)model.departureLocationId);
                int reciverStateId = GetUserStateId((int)model.destinationId);
                //i commented this out so home deivrly shipment, not just intra state could enter into shipment parcel

                //if (destinationLocation.stateId == departureLocation.stateId)
                //{//Intra-state (within a state)
                //    dropOffLocation = model.receiverAddress;
                //}
                //else
                //{//Inter-state (between states)
                //    dropOffLocation = departureLocation.Address;
                //}
                ShipmentParcel shipmentParcel = new ShipmentParcel
                {
                    AccountType = model.AccountType,
                    CreatedBy = user?.Identity?.Name,
                    CreatedDate = DateTime.Now,
                    Waybill = model.Waybill,
                    ParcelStatus = ((int?)ParcelStatus.Pending).ToString(),
                    IsAssigned = false,
                    IsAssignedBy = null,
                    IsDroppedOffBy = null,
                    IsDroppedOff = false,
                    IsDroppedOffDate = null,
                    IsPickedDate = null,
                    IsPickedUp = false,
                    IsPickedUpFrom = model.senderAddress,
                    IsPickUpBy = null,
                    ParcelAssignedTo = null,
                    ParcelDropTo = dropOffLocation,
                    Id = default,
                    AcceptedBy = null,
                    AcceptedDate = null,
                    AssignedDate = null,
                    IsAccepted = false,
                    Vehicle = null,
                    PickUpLatitude = model.senderLat,
                    PickUpLongitude = model.senderLng,
                    DropOffLatitude = model.receiverLat,
                    DropOffLongitude = model.receiverLng,
                    IsInterstate = senderStateId != reciverStateId,

                };
                db.ShipmentParcels.InsertOnSubmit(shipmentParcel);
                db.SubmitChanges();
                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }
        //public void UpdateShipmentParcel()
        //{
        //    liblogisticsDataContext db = new liblogisticsDataContext();
        //    var data = (from s in db.shipments where s.AccountType == 0 || s.AccountType == 1 select s).ToList();
        //    foreach(var s in data)
        //    {
        //        var sparcel = (from spl in db.ShipmentParcels where spl.Waybill == s.Waybill select spl).FirstOrDefault();
        //        if(sparcel == null)
        //        {//if the shipment does not already exist in the shipment parcel, then you can create it!
        //            AccountType at = (AccountType)((int)s.AccountType);
        //            ShipmentParcel(at, s.Waybill,s.senderAddress, s.destinationId, null,null,null,null);
        //        }
        //    }
        //}
        //public List<returnmsg> UpdateShipmentItem(ShipmentItemModel shipmentItem)
        //{
        //    var data = new ShipmentItem
        //    {
        //        id = shipmentItem.Id,
        //        itemDescription = shipmentItem.ItemDesc,
        //        ShipmentType = shipmentItem.ShipmentType,
        //        itemWeight = shipmentItem.ItemWeight,
        //        itemNature = shipmentItem.shItemCondition,
        //        price = shipmentItem.Price,
        //        quantity = shipmentItem.Quantity,
        //        ShipmentId = shipmentItem.ShipmentId
        //};
        //    return UpdateShipmentItem(data);
        //}
        public List<returnmsg> UpdateShipmentItem(ShipmentItem shipmentItem)
        {

            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                var item = (from si in db.ShipmentItems where si.id == shipmentItem.id select si).FirstOrDefault();
                item.itemDescription = shipmentItem.itemDescription;
                item.ShipmentType = shipmentItem.ShipmentType;
                item.itemWeight = shipmentItem.itemWeight;
                item.itemNature = shipmentItem.itemNature;
                item.price = shipmentItem.price;
                item.quantity = shipmentItem.quantity;
                item.ShipmentId = shipmentItem.ShipmentId;

                db.SubmitChanges();
                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Updated";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
                throw;
            }
        }


        public List<returnmsg> CreateShipmentPackaging(ShipmentPackagingModel shipmentpackage)
        {
            //ShipmentPackagingModel
            if (shipmentpackage == null) { return null; }
            return CreateShipmentPackaging(shipmentpackage.ShipmentId, shipmentpackage.PackageType, shipmentpackage.Createdby, shipmentpackage.CreationTime, shipmentpackage.PackagingFee,
                shipmentpackage.DepartureLocationId, shipmentpackage.DestinationId);
        }
        public List<returnmsg> CreateShipmentPackaging(string shipmentId, int packageType, string createdby, DateTime creationtime, decimal? packagingfee,
            int? departureLocationId, int? destinationId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                ShipmentPackaging shipmentpckg = new ShipmentPackaging();
                shipmentpckg.ShipmentId = shipmentId;
                shipmentpckg.PackageType = packageType;
                shipmentpckg.Createdby = createdby;
                shipmentpckg.CreationTime = creationtime;
                shipmentpckg.PackagingFee = packagingfee;
                shipmentpckg.DepartureLocationId = departureLocationId;
                shipmentpckg.DestinationId = destinationId;
                db.ShipmentPackagings.InsertOnSubmit(shipmentpckg);
                db.SubmitChanges();
                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex.Message;
                retmsgs.Add(rtmsg);
                return retmsgs;
                //throw;
            }
        }
        public List<returnmsg> CreateShipmentItem(ShipmentItemModel shipmentItem)
        {
            if (shipmentItem == null) { return null; }
            return CreateShipmentItem(shipmentItem.ItemDesc, shipmentItem.ShipmentType, shipmentItem.ItemWeight, shipmentItem.shItemCondition,
            shipmentItem.Price, shipmentItem.Quantity, shipmentItem.ShipmentId, shipmentItem.ItemCategoryId, shipmentItem.ItemCategoryDescription, shipmentItem.SpecialPackagePricingId);
        }
        public List<returnmsg> CreateShipmentItem(string itemdesc, int shipmentType, decimal itemWeight, string ItemNature,
            decimal Price, int quantity, int ShipmentId, int? ItemCategoryId, string ItemCategoryDescription, int? SpecialPackagePricingId)
        {

            //liblogisticsDataContext db = new liblogisticsDataContext();
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                ShipmentItem items = new ShipmentItem();
                items.DateCreated = DateTime.Now;
                items.itemDescription = itemdesc;
                items.ShipmentType = shipmentType;
                items.itemWeight = itemWeight;
                items.itemNature = ItemNature;
                items.price = Price;
                items.quantity = quantity;
                items.ShipmentId = ShipmentId;
                items.ItemCategoryId = ItemCategoryId;
                items.ItemCategoryDescription = ItemCategoryDescription;
                items.SpecialPackagePricingId = SpecialPackagePricingId;
                db.ShipmentItems.InsertOnSubmit(items);
                db.SubmitChanges();
                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Added";
                retmsgs.Add(rtmsg);
                return retmsgs;

            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex.Message;
                retmsgs.Add(rtmsg);
                return retmsgs;
                //throw;
            }
        }
        public int GetShipmentId(string waybillnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.shipments
                            where l.Waybill == waybillnumber
                            select l.Id).FirstOrDefault();
                return dtcx;
            }
        }

        public int GetMerchantShipmentId(int merchantID)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.MerchantShipments.OrderByDescending(m => m.ID)
                            where l.MerchantID == merchantID
                            select l.ID).FirstOrDefault();
                return dtcx;
            }
        }
        //Get shipment method
        public List<ShipmentModel> GetShipmentsByWayBill(string waybillnumber)
        {
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    var dtcx = (from l in db.shipments
                                join dpId in db.Locations on l.departureLocationId equals dpId.id
                                join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id

                                join sType in db.ServiceTypes on l.ServiceType equals sType.ID
                                join arrId in db.Locations on l.destinationId equals arrId.id
                                let shpItems = db.ShipmentItems.Where(z => z.ShipmentId == l.Id)
                                where l.Waybill == waybillnumber
                                select new ShipmentModel
                                {
                                    Id = l.Id,
                                    Waybill = l.Waybill,
                                    //ShpCustomerType = ctType.CusType,
                                    DeliveryTime = l.DeliveryTime,
                                    DeliveryType = dlType.name,
                                    departureLocation = dpId.locationName,
                                    departureLocationId = dpId.id,
                                    departureLocationAddress = dpId.Address,
                                    //departureState = depstate.name,
                                    destinationLocation = arrId.locationName,
                                    destinationLocationAddress = arrId.Address,
                                    destinationId = arrId.id,
                                    //destinationState = arrId.State.name,
                                    GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal ?? 0)),
                                    description = l.description,
                                    TotalWeight = l.itemsWeight,
                                    vat = l.vat,
                                    paymentMethod = l.paymentMethod,
                                    receiverName = l.receiverName,
                                    receiverPhoneNumber = l.receiverPhoneNumber,
                                    receiverAddress = l.receiverAddress,
                                    PaymentStatus = l.PaymentStatus,
                                    senderAddress = l.senderAddress,
                                    senderName = l.SenderName,
                                    senderphone = l.SenderPhoneNumber,
                                    DateCreated = l.DateCreated,
                                    specialnote = l.specialnote,
                                    IsMissing = l.IsMissing,

                                    ServiceType = l.ServiceType,
                                    IsMissingDate = l.IsMissingDate,
                                    IsMissingStatus = l.IsMissingStatus,
                                    DeclaredValue = ConvertAmount(Convert.ToDecimal(l.deClearedValue ?? 0)),
                                    totalTopay = Convert.ToDecimal(l.grandTotal ?? 0),
                                    PayStackPaymentResponse = l.PayStackPaymentResponse,
                                    ShipmentStatus = GetShipmentStatus(l.Waybill),
                                    Items = shpItems.Any() ? shpItems.ToList().ToModelList() : new List<ShipmentItemModel>()
                                }).OrderByDescending(x => x.DateCreated).ToList();
                    return dtcx;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ShipmentModel> GetShipmentsByUserEmail(string UserEmail)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            //join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.createdBy == UserEmail
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = UserEmail,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                isCancelled = l.isCancelled,
                                Items = item.ToModelList()
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<NotificationModel> Notifications(int locationId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join grp in db.GroupWayBillNumbers on l.groupId equals grp.GroupWaybillCode
                            join manmap in db.ManifestMappings on grp.GroupWaybillCode equals manmap.GroupWaybillNumber
                            join manifst in db.Manifests on manmap.ManifestId equals manifst.ManifestId
                            let dr = db.shipments.Where(w => w.Id == l.Id)
                            let lc = db.Locations.Where(k => k.id == dpId.id)
                            where l.destinationId == locationId && l.DateCreated >= DateTime.Now.Date
                            //(l.DateCreated >= DateTime.Now.Date && l.DateCreated <= DateTime.Now.Date.AddDays(1))
                            && manifst.IsDispatched == true
                            select new NotificationModel
                            {
                                Description = lc.Count() + " Shipment(s) from " + dpId.locationName
                            }).ToList();
                return dtcx;
            }

        }
        public List<ShipmentModel> GetShipmentsByLocationId(int locationId)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.destinationId == locationId && l.groupId == null && l.isCancelled == false
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,//customerdata.firstName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList()
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        public List<ShipmentModel> GetShipmentsByDepIdAndDes(int deptId, int destId)
        {

            int AccountTypeIOS = Convert.ToInt32(AccountType.IOS);
            int AccountTypeAndriod = Convert.ToInt32(AccountType.Android);
            int AccountTypeWeb = Convert.ToInt32(AccountType.Web);
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();

                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.destinationId == destId && l.groupId == null && l.isCancelled == false
                            && l.AccountType == AccountTypeWeb && l.departureLocationId == deptId
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList()
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();

                var dtcxs = (from l in db.shipments
                             join dpId in db.Locations on l.departureLocationId equals dpId.id
                             join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                             join arrId in db.Locations on l.destinationId equals arrId.id
                             join parcelSta in db.ShipmentParcels on l.Waybill equals parcelSta.Waybill
                             where (l.destinationId == destId && l.groupId == null && l.isCancelled == false
                             && l.departureLocationId == deptId) && parcelSta.ParcelStatus == "10"
                             && parcelSta.IsInterstate == true && (l.AccountType == AccountTypeAndriod || l.AccountType == AccountTypeIOS)
                             select new ShipmentModel
                             {
                                 Id = l.Id,
                                 Waybill = l.Waybill,
                                 createdBy = l.createdBy,
                                 CustomerType = null,//ctType.CusType,
                                 DeliveryTime = l.DeliveryTime,
                                 DeliveryType = dlType.name,
                                 departureLocation = dpId.locationName,
                                 departureState = null,//depstate.name,
                                 destinationLocation = arrId.locationName,
                                 destinationState = null,//arrState.name,
                                 GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                 description = l.description,
                                 TotalWeight = l.itemsWeight,
                                 vat = l.vat,
                                 paymentMethod = l.paymentMethod,
                                 receiverName = l.receiverName,
                                 receiverPhoneNumber = l.receiverPhoneNumber,
                                 PaymentStatus = l.PaymentStatus,
                                 senderAddress = l.senderAddress,
                                 senderName = l.SenderName,
                                 DateCreated = l.DateCreated,
                                 Items = item.ToModelList()
                             }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();

                var results = dtcx.ToList().Union(dtcxs.ToList()).OrderByDescending(P => P.ShipmentStatus);

                return results.ToList();

            }
        }
        //public List<ShipmentModel> GetInboundByLocationId(int locationId)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        List<ShipmentItem> item = new List<ShipmentItem>();
        //        var dtcx = (from l in db.shipments
        //                    join dpId in db.Locations on l.departureLocationId equals dpId.id
        //                    join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
        //                    join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
        //                    join arrId in db.Locations on l.destinationId equals arrId.id
        //                    join depstate in db.States on l.senderStateId equals depstate.id
        //                    join arrState in db.States on l.receiverStateId equals arrState.id
        //                    where l.destinationId == locationId
        //                    select new ShipmentModel
        //                    {
        //                        Id = l.Id,
        //                        Waybill = l.Waybill,
        //                        createdBy = l.createdBy,
        //                        CustomerType = ctType.CusType,
        //                        DeliveryTime = l.DeliveryTime,
        //                        DeliveryType = dlType.name,
        //                        departureLocation = dpId.locationName,
        //                        departureState = depstate.name,
        //                        destinationLocation = arrId.locationName,
        //                        destinationState = arrState.name,
        //                        GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
        //                        description = l.description,
        //                        TotalWeight = l.itemsWeight,
        //                        vat = l.vat,
        //                        paymentMethod = l.paymentMethod,
        //                        receiverName = l.receiverName,
        //                        receiverPhoneNumber = l.receiverPhoneNumber,
        //                        PaymentStatus = l.PaymentStatus,
        //                        senderAddress = l.senderAddress,
        //                        senderName = l.SenderName,
        //                        DateCreated = l.DateCreated,
        //                        Items = item.ToModelList()
        //                    }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
        //        return dtcx;
        //    }
        //}
        public List<ShipmentModel> GetInboundByLocationIdAndDate(int locationId, DateTime startdate, DateTime enddate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            where l.destinationId == locationId && l.DateCreated >= startdate && l.DateCreated <= enddate.AddDays(1)
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //ShpCustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                //departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                //destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                senderphone = l.SenderPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                IsCredit = l.IsCredit,
                                deClearedValue = l.deClearedValue,
                                CreditPaymentDate = l.CreditPaymentDate
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<ShipmentModel> GetAllInboundShipments(DateTime startdate, DateTime enddate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            where l.senderStateId != 8 && l.receiverStateId == 8 && l.isCancelled == false && l.DateCreated >= startdate && l.DateCreated <= enddate.AddDays(1)
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //ShpCustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                //departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                //destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                senderphone = l.SenderPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                IsCredit = l.IsCredit,
                                deClearedValue = l.deClearedValue,
                                CreditPaymentDate = l.CreditPaymentDate
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<ShipmentModel> GetAllOutboundShipments(DateTime startdate, DateTime enddate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            where l.senderStateId == 8 && l.receiverStateId != 8 && l.isCancelled == false && l.DateCreated >= startdate && l.DateCreated <= enddate.AddDays(1)
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //ShpCustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                //departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                //destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                senderphone = l.SenderPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                IsCredit = l.IsCredit,
                                deClearedValue = l.deClearedValue,
                                CreditPaymentDate = l.CreditPaymentDate
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<ShipmentModel> GetAllShipments(DateTime startdate, DateTime enddate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            where l.isCancelled == false && l.DateCreated >= startdate && l.DateCreated <= enddate.AddDays(1)
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //ShpCustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                //departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                //destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                senderphone = l.SenderPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                IsCredit = l.IsCredit,
                                deClearedValue = l.deClearedValue,
                                CreditPaymentDate = l.CreditPaymentDate
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }


        public List<ShipmentModel> GetShipments(DateTime startdate, DateTime enddate)/* int locationId,int collectedEnum, */
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.DateCreated >= startdate && l.DateCreated <= enddate
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                IsCredit = l.IsCredit,
                                CreditPaymentDate = l.CreditPaymentDate,
                                destinationId = l.destinationId,
                                IsCollected = l.IsCollected,
                                BookingRefCode = l.BookingRefCode,
                                HasArrived = l.HasArrived,
                                isCancelled = l.isCancelled,
                                IsMissing = l.IsMissing,
                                IsMissingDate = l.IsMissingDate,
                                IsMissingStatus = l.IsMissingStatus,
                                expectedDateOfArrival = l.expectedDateOfArrival,
                                senderphone = l.SenderPhoneNumber,
                                IsRefund = l.IsRefund,
                                packagingfee = l.packagingfee,
                                DeclaredValue = l.deClearedValue.ToString(),
                                PayStackPaymentResponse = l.PayStackPaymentResponse,
                                PayStackResponse = l.PayStackResponse,
                                totalTopay = l.totalTopay,
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        //GetShipmentsByLocationIdDate
        public List<ShipmentModel> GetShipmentsByLocationIdDate(int locationId, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.departureLocationId == locationId && l.DateCreated >= fromdate && l.DateCreated <= todate.AddDays(1)
                            && l.isCancelled == false && l.PaymentStatus != 0
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                packagingfee = l.packagingfee,
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderByDescending(x => x.DateCreated).ToList();
                return dtcx;
            }
        }



        //my own method
        public List<ShipmentModel> GetShipmentsByLocationAndUserIdInterState(int locationId, string userid, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.departureLocationId == locationId && l.DateCreated >= fromdate && l.DateCreated <= todate.AddDays(1)
                            && l.isCancelled == false && l.createdBy == userid && l.senderStateId != l.receiverStateId
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                packagingfee = l.packagingfee,
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderByDescending(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<ShipmentModel> GetShipmentsByLocationAndUserIdIntraState(int locationId, string userid, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.departureLocationId == locationId && l.DateCreated >= fromdate && l.DateCreated <= todate.AddDays(1)
                            && l.isCancelled == false && l.createdBy == userid && l.senderStateId == l.receiverStateId
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                packagingfee = l.packagingfee,
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderByDescending(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        //my own method
        public List<ShipmentModel> GetShipmentsByLocationIdDateAndPayType(int locationId, DateTime fromdate, DateTime todate, string paymentmethod)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.departureLocationId == locationId && l.DateCreated >= fromdate && l.DateCreated <= todate.AddDays(1)
                            && l.isCancelled == false && l.paymentMethod == paymentmethod
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                packagingfee = l.packagingfee,
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderByDescending(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        //GetShipmentsByLocationOneDay
        public List<ShipmentModel> GetShipmentsByLocationOneDay(int locationId, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            //join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.departureLocationId == locationId && l.isCancelled == false && l.PaymentStatus != 0 &&
                            //l.DateCreated == fromdate.Date
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.Date.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                DateCreated = l.DateCreated,
                                packagingfee = l.packagingfee,
                                Items = item.ToModelList(),
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<ShipmentModel> GetShipmentsByLocationOneDayAndPayType(int locationId, DateTime fromdate, DateTime todate, string paymentmethod)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            //join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.departureLocationId == locationId && l.isCancelled == false
                            && l.paymentMethod == paymentmethod
                            &&
                            //l.DateCreated == fromdate.Date
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.Date.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                DateCreated = l.DateCreated,
                                packagingfee = l.packagingfee,
                                Items = item.ToModelList(),
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        //GetShipmentsByUsers
        public List<ShipmentModel> GetShipmentsByUsers(string userid, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            //join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            where l.createdBy == userid && l.isCancelled == false && l.PaymentStatus != 0 &&
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.Date.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                //departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                //destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                packagingfee = l.packagingfee,
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        public List<ShipmentModel> GetShipmentsByUsersAndPayType(string userid, DateTime fromdate, DateTime todate, string paymentmethod)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            //join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            where l.createdBy == userid && l.isCancelled == false && l.paymentMethod == paymentmethod &&
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.Date.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                //departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                //destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                packagingfee = l.packagingfee,
                                specialnote = l.specialnote
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        public List<GenericReportModel> GetVatByLocationOneDay(int locationId, DateTime fromdate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<GenericReportModel> item = new List<GenericReportModel>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            where l.departureLocationId == locationId &&
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= fromdate.Date.AddDays(1))
                            select new GenericReportModel
                            {
                                WayBill = l.Waybill,
                                DateCreated = l.DateCreated.ToString(),
                                TotalPerDay = l.vat.ToString(),
                                LocationName = dpId.locationName
                            }).GroupBy(t => t.TotalPerDay).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<GenericReportModel> GetPkByLocationOneDay(int locationId, DateTime fromdate, string packageType)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<GenericReportModel> item = new List<GenericReportModel>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            where l.departureLocationId == locationId &&
                               (l.DateCreated >= fromdate.Date && l.DateCreated <= fromdate.Date.AddDays(1)) ||
                               l.PackagingType == packageType
                            select new GenericReportModel
                            {
                                WayBill = l.Waybill,
                                DateCreated = l.DateCreated.ToString(),
                                TotalPerDay = l.packagingfee.ToString(),
                                LocationName = dpId.locationName,
                                CreatedBy = l.createdBy
                            }).ToList();
                // }).GroupBy(t => t.WayBill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<CancelledShpModel> GetCancelledByLocationOneDay(int locationId, DateTime fromdate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<CancelledShpModel> item = new List<CancelledShpModel>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            where l.departureLocationId == locationId &&
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= fromdate.Date.AddDays(1))
                            select new CancelledShpModel
                            {
                                WayBill = l.Waybill,
                                DateCreated = l.DateCreated.ToString(),
                                TotalPerDay = l.vat.ToString(),
                                LocationName = dpId.locationName,
                                CreatedBy = l.createdBy
                            }).GroupBy(t => t.TotalPerDay).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<GenericReportModel> GetInsuranceByLocationOneDay(int locationId, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<GenericReportModel> item = new List<GenericReportModel>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            where l.departureLocationId == locationId &&
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.Date.AddDays(1))
                            select new GenericReportModel
                            {
                                WayBill = l.Waybill,
                                DateCreated = l.DateCreated.ToString(),
                                TotalPerDay = l.insuranceAmount.ToString(),
                                LocationName = dpId.locationName
                            }).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }

        public List<GroupShipmentListModel> GetGroupshipmentLists(string groupnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.GroupWaybillNumMappings
                           join ar in db.Locations on l.DestinationId equals ar.id
                           join des in db.Locations on l.DepartureId equals des.id
                           where l.GroupWaybillNumber == groupnumber

                           select new GroupShipmentListModel
                           {
                               Waybillnum = l.WaybillNumber,
                               DestinationName = ar.locationName,
                               DepartureName = des.locationName
                           }).ToList();
                return dtx;
            }
        }

        //public List<GroupnumbersListModel> GetGroupNumberByManifestNumber(int manifestId)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var dtx = (from l in db.ManifestMappings
        //                   where l.ManifestId == manifestId
        //                   select new GroupnumbersListModel
        //                   {
        //                       GroupNumber = l.GroupWaybillNumber
        //                   }).ToList();
        //        return dtx;
        //    }
        //}

        //public int GetmanifestIdbyNum(string ManifestNumber)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var dtx = (from l in db.Manifests
        //                   where l.ManifestNumber == ManifestNumber
        //                   select l.ManifestId).FirstOrDefault();
        //        return dtx;
        //    }
        //}
        public List<ManifestModel> GetManifestListsByManifestNum(string manifestNum)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.ManifestMappings
                           join mb in db.GroupWayBillNumbers on l.GroupWaybillNumber equals mb.GroupWaybillCode
                           join mp in db.Manifests on l.ManifestId equals mp.ManifestId
                           join des in db.Locations on mb.arrivalId equals des.id
                           where mp.ManifestNumber == manifestNum && mb.hasManifest == true
                           select new ManifestModel
                           {
                               groupnumber = l.GroupWaybillNumber,
                               destination = des.locationName
                           }).ToList();
                return dtx;
            }
        }
        public List<GroupShipmentListModel> GetGroupshipmentByDeptId(int departureId, DateTime startDate, DateTime endDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.GroupWayBillNumbers
                           join ar in db.Locations on l.arrivalId equals ar.id
                           join des in db.Locations on l.departureId equals des.id
                           //join hubn in db.Locations on l.HubId equals hubn.id
                           let dr = db.Locations.Where(w => w.id == l.HubId)
                           where l.departureId == departureId && (l.DateCreated >= startDate && l.DateCreated <= endDate)
                           select new GroupShipmentListModel
                           {
                               GroupNumber = l.GroupWaybillCode,
                               DestinationName = ar.locationName,
                               DepartureName = des.locationName,
                               dateTime = l.DateCreated,
                               sentToHub = l.SentToHub,
                               HubName = dr.FirstOrDefault().locationName
                           }).ToList();
                return dtx.ToList();
            }
        }
        public List<GroupShipmentListModel> GetGroupshipmentByGroupNum(string Groupnum)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.GroupWayBillNumbers
                           join ar in db.Locations on l.arrivalId equals ar.id
                           join des in db.Locations on l.departureId equals des.id
                           //join hubn in db.Locations on l.HubId equals hubn.id
                           let dr = db.Locations.Where(w => w.id == l.HubId)
                           where l.GroupWaybillCode == Groupnum
                           select new GroupShipmentListModel
                           {
                               GroupNumber = l.GroupWaybillCode,
                               DestinationName = ar.locationName,
                               DepartureName = des.locationName,
                               dateTime = l.DateCreated,
                               sentToHub = l.SentToHub,
                               HubName = dr.FirstOrDefault().locationName
                           }).ToList();
                return dtx.ToList();
            }
        }



        public List<GroupShipmentListModel> GetGroupshipmentBybyHub(int hubId, DateTime startDate, DateTime endDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.GroupWayBillNumbers
                           join ar in db.Locations on l.arrivalId equals ar.id
                           join dep in db.Locations on l.departureId equals dep.id
                           let dr = db.Locations.Where(w => w.id == l.HubId)
                           where (l.HubId == hubId || dep.id == hubId) && (l.DateCreated >= startDate && l.DateCreated <= endDate)
                           select new GroupShipmentListModel
                           {
                               GroupNumber = l.GroupWaybillCode,
                               DestinationName = ar.locationName,
                               DepartureName = dep.locationName,
                               dateTime = l.DateCreated,
                               sentToHub = l.SentToHub,
                               HubName = dr.FirstOrDefault().locationName
                           }).ToList();
                return dtx.ToList();
            }
        }

        //public List<GroupShipmentListModel> GetGroupshipmentByDeptIdNoManifest(int departureId)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //        var dtx = (from l in db.GroupWaybillNumMappings
        //                   join ar in db.Locations on l.DestinationId equals ar.id
        //                   join man in db.GroupWayBillNumbers on l.GroupWaybillNumber equals man.GroupWaybillCode
        //                   join des in db.Locations on l.DepartureId equals des.id
        //                   where l.DepartureId == departureId && man.hasManifest == false

        //                   select new GroupShipmentListModel
        //                   {
        //                       Waybillnum = l.GroupWaybillNumber,
        //                       DestinationName = ar.locationName,
        //                       DepartureName = des.locationName
        //                   }).ToList().GroupBy(i => i.Waybillnum).Select(group => group.FirstOrDefault());
        //        return dtx.ToList();
        //    }
        //}


        public List<GroupShipmentListModel> GetGroupshipmentByDestId(int destinationId, int departureId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.GroupWayBillNumbers
                           join ar in db.Locations on l.arrivalId equals ar.id
                           join des in db.Locations on l.departureId equals des.id
                           join g in db.GroupWaybillNumMappings on l.GroupWaybillCode equals g.GroupWaybillNumber
                           where l.arrivalId == destinationId && l.departureId == departureId
                           && l.hasManifest == false
                           select new GroupShipmentListModel
                           {
                               Waybillnum = l.GroupWaybillCode,
                               DestinationName = ar.locationName,
                               DepartureName = des.locationName,
                               dateTime = l.DateCreated
                           }).ToList().GroupBy(i => i.Waybillnum).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }

        public string ManifestReceivedBy(string waybill)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from m in db.Manifests
                           join mp in db.ManifestMappings
                          on m.ManifestId equals mp.ManifestId
                           join gp in db.GroupWaybillNumMappings
                      on mp.GroupWaybillNumber equals gp.GroupWaybillNumber
                           where
                         m.ManifestNumber == waybill || gp.WaybillNumber == waybill
                           select m.IsReceivedBy).FirstOrDefault();

                return dtx;
            }
        }


        public string ManifestReceivedDate(string waybill)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from m in db.Manifests
                           join mp in db.ManifestMappings
                          on m.ManifestId equals mp.ManifestId
                           join gp in db.GroupWaybillNumMappings
                      on mp.GroupWaybillNumber equals gp.GroupWaybillNumber
                           where
                         m.ManifestNumber == waybill || gp.WaybillNumber == waybill
                           select m.IsReceivedDate.ToString()).FirstOrDefault();

                return dtx;
            }
        }


        public bool ReceiveShipment(string waybilnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                shipment shpmt = new shipment();
                try
                {
                    List<shipment> shp = (from p in db.shipments
                                          where p.Waybill == waybilnumber
                                          select p).ToList();
                    if (shp.Count() > 0)
                    {
                        foreach (shipment l in shp)
                        {
                            if (l.HasArrived) { continue; }
                            var destinationAddress = (from loc in db.Locations where loc.id == l.destinationId select loc).FirstOrDefault().locationName;
                            //string msg = $"Dear {l.receiverName}, your item has arrived. Visit our {destinationAddress} office with valid I.D Card and waybill no. for pickup within 3 days, to avoid demurrage of NGN200/day. 09062547031,for further enquiries";
                            //string msg = $"Dear {l.receiverName}, your item has arrived.Visit our {destinationAddress} office with valid ID Card and waybill no. for pickup within 2 days, to aviod demurrage of NGN200/day.To locate your pick up address use the waybill number ({l.Waybill}) on the tracking page via http://libmotexpress.com";

                            string msg = $"Hi {l.receiverName}, your item has arrived.Visit our {destinationAddress} office with valid ID and waybill no. for pickup within 2 days, to avoid demurrage of NGN200/day. Kindly confirm your item before leaving the office.";


                            string senderMessage = $"Hi {l.SenderName}. Your item has arrived at {destinationAddress}.";


                            //string homeDelivrymsg = $"Dear {l.receiverName}, your shipment with WayBill No: {l.Waybill}. Has arrrived at {destinationAddress} Hub and would be delivered to your doorstep within 24hours.To track your shipment address, visit the website http://libmotexpress.com";
                            //string homeDelivrymsg = $"Dear {l.receiverName}, your shipment with WayBill No: {l.Waybill}. Has arrrived at {destinationAddress} Hub and would be delivered to your doorstep soon for free.Please Note:Extra Charges May Apply Based on Location.To track your shipment address, visit the website http://libmotexpress.com";
                            string homeDelivrymsg = $"Hi {l.receiverName}, your shipment from{l.SenderName} has arrrived at {destinationAddress} Hub & would be delivered to your doorstep.";

                            l.HasArrived = true;
                            l.DateModified = DateTime.Now;
                            l.actualArrivalDate = DateTime.Now;
                            SendSms(l.receiverPhoneNumber, msg);
                            SendSms(l.SenderPhoneNumber, senderMessage);

                            if ((l.receiverStateId == 8) || (l.receiverStateId == 16))
                            {
                                List<string> num = new List<string>();
                                //reciever
                                num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.receiverPhoneNumber);
                                num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.SenderPhoneNumber);

                                RequestSMS sms = new RequestSMS();
                                sms.id = Guid.NewGuid().ToString();
                                sms.to = num;
                                sms.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                                sms.body = homeDelivrymsg;
                                KonnectAPI konnectAPI = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                                konnectAPI.SendSMS(sms);

                            }
                            else
                            {
                                List<string> num = new List<string>();

                                num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.SenderPhoneNumber);
                                RequestSMS sms = new RequestSMS();
                                sms.id = Guid.NewGuid().ToString();
                                sms.to = num;
                                sms.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                                sms.body = senderMessage;
                                KonnectAPI konnectAPI = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                                konnectAPI.SendSMS(sms);
                                // SendSms(l.receiverPhoneNumber, msg);
                                //sender sms
                                List<string> num2 = new List<string>();
                                num2.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.receiverPhoneNumber);
                                //                              num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.SenderPhoneNumber);
                                RequestSMS sms2 = new RequestSMS();
                                sms2.id = Guid.NewGuid().ToString();
                                sms2.to = num2;
                                sms2.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                                sms2.body = msg;
                                KonnectAPI konnectAPI2 = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                                konnectAPI2.SendSMS(sms2);
                                //List<RequestSMS> requestSMs = new List<RequestSMS>();
                                //requestSMs.Add(sms);
                                //requestSMs.Add(sms2);
                                //foreach (var sm in requestSMs)
                                //{
                                //    konnectAPI.SendSMS(sm);

                                //}
                            }
                        }
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool ArrivalActivation(string waybilnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                shipment shpmt = new shipment();
                try
                {
                    List<shipment> shp = (from p in db.shipments
                                          where p.Waybill == waybilnumber
                                          select p).ToList();
                    if (shp.Count() > 0)
                    {
                        foreach (shipment l in shp)
                        {
                            if (l.HasArrived) { continue; }
                            var destinationAddress = (from loc in db.Locations where loc.id == l.destinationId select loc).FirstOrDefault().locationName;
                            string msg = $"Hi {l.receiverName}, your item have been delayed {destinationAddress} King David";

                            l.HasArrived = true;
                            l.DateModified = DateTime.Now;
                            l.actualArrivalDate = DateTime.Now;
                            // SendSms(l.receiverPhoneNumber, msg);

                            List<string> num = new List<string>();
                            num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.receiverPhoneNumber);
                            num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.SenderPhoneNumber);
                            RequestSMS sms = new RequestSMS();
                            sms.id = Guid.NewGuid().ToString();
                            sms.to = num;
                            sms.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                            sms.body = msg;
                            KonnectAPI konnectAPI = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                            konnectAPI.SendSMS(sms);
                            // SendSms(l.receiverPhoneNumber, msg);

                        }
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool CheckReceiveShipmentStatus(string waybill)
        {
            using (var db = new liblogisticsDataContext())
            {
                var shp = (from l in db.shipments
                           where l.Waybill == waybill && l.HasArrived == true
                           select l.Waybill).FirstOrDefault();
                if (shp != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool CheckReleasedShipmentStatus(string waybill)
        {
            using (var db = new liblogisticsDataContext())
            {
                var shp = (from l in db.shipments
                           where l.Waybill == waybill && l.IsCollected == 1
                           select l.Waybill).FirstOrDefault();
                if (shp != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
        public bool MissingShipment(string waybillnumber)
        {
            using (var db = new liblogisticsDataContext())
            {

                shipment shpmt = new shipment();
                try
                {
                    List<shipment> shp = (from p in db.shipments
                                          where p.Waybill == waybillnumber
                                          select p).ToList();
                    if (shp.Count() > 0)
                    {
                        foreach (shipment l in shp)
                        {
                            l.IsMissing = true;
                            l.IsMissingDate = DateTime.Now;
                            l.IsMissingStatus = SysModels.ismissingstatus.Pending.ToString();
                        }
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool TamperedShipment(string waybillnumber, int temperedStatus)
        {
            using (var db = new liblogisticsDataContext())
            {

                shipment shpmt = new shipment();
                try
                {
                    List<shipment> shp = (from p in db.shipments
                                          where p.Waybill == waybillnumber
                                          select p).ToList();
                    if (shp.Count() > 0)
                    {
                        foreach (shipment l in shp)
                        {
                            l.IsTampered = true;
                            l.IsTamperedStatus = temperedStatus;
                            l.IsTamperedDate = DateTime.Now;
                        }
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool CheckIsMissingShipmentStatus(string waybill)
        {
            using (var db = new liblogisticsDataContext())
            {
                var shp = (from l in db.shipments
                           where l.Waybill == waybill && l.IsMissing == true
                           select l.Waybill).FirstOrDefault();
                if (shp != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool CheckIsCancelledShipmentStatus(string waybill)
        {
            using (var db = new liblogisticsDataContext())
            {
                var shp = (from l in db.shipments
                           where l.Waybill == waybill && l.isCancelled == true
                           select l.Waybill).FirstOrDefault();
                if (shp != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public List<ManifestModel> GetManifestByDepttId(int departureId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.Manifests
                           join dp in db.Locations on l.DepartId equals dp.id
                           join dl in db.Locations on l.DestinationId equals dl.id into destination
                           from dl in destination.DefaultIfEmpty()
                           join dn in db.Drivers on l.DriverId equals dn.Id
                           join vh in db.Vehicles on l.VehicleId equals vh.id
                           let dr = db.Drivers.Where(w => w.Id == l.DriverId)
                           let vd = db.Vehicles.Where(n => n.id == l.VehicleId)
                           where l.DepartId == departureId && (l.DateCreated >= fromDate && l.DateCreated <= toDate.AddDays(1))
                           select new ManifestModel
                           {
                               DateCreated = l.DateCreated,
                               departureLocation = dp.locationName,
                               dispatchedBy = l.DispatchedById,
                               IsDispatched = l.IsDispatched,
                               IsReceived = l.IsReceived,
                               ManifestId = l.ManifestId,
                               ManifestNumber = l.ManifestNumber,
                               IsReceivedBy = l.ReceiverById,
                               driverphone = dn.DriverPhone,
                               DriverInfo = dr.Any() ? dr.ToList() : new List<Driver>(),
                               VehicleInfo = vd.Any() ? vd.ToList() : new List<Vehicle>(),
                               DispatchFee = l.DispatchFee,
                               driverName = dn.DriverName,
                               vehicleNumber = vh.regNumber,
                               destination = GetlocationsbyID(l.DestinationId.GetValueOrDefault())
                           }).ToList().GroupBy(i => i.ManifestId).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }

        public List<ManifestModel> GetManifestByHubId(int hubId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.Manifests
                           join hb in db.LocationHubs on l.DepartId equals hb.LocationId
                           join dp in db.Locations on hb.LocationId equals dp.id
                           join dl in db.Locations on l.DestinationId equals dl.id into destination
                           from dl in destination.DefaultIfEmpty()
                           join dn in db.Drivers on l.DriverId equals dn.Id
                           join vh in db.Vehicles on l.VehicleId equals vh.id
                           let dr = db.Drivers.Where(w => w.Id == l.DriverId)
                           let vd = db.Vehicles.Where(n => n.id == l.VehicleId)
                           where hb.HubId == hubId && (l.DateCreated >= fromDate && l.DateCreated <= toDate.AddDays(1))

                           select new ManifestModel
                           {
                               DateCreated = l.DateCreated,
                               departureLocation = dp.locationName,
                               dispatchedBy = l.DispatchedById,
                               IsDispatched = l.IsDispatched,
                               IsReceived = l.IsReceived,
                               ManifestId = l.ManifestId,
                               ManifestNumber = l.ManifestNumber,
                               IsReceivedBy = l.ReceiverById,
                               driverphone = dn.DriverPhone,
                               DriverInfo = dr.Any() ? dr.ToList() : new List<Driver>(),
                               VehicleInfo = vd.Any() ? vd.ToList() : new List<Vehicle>(),
                               DispatchFee = l.DispatchFee,
                               driverName = dn.DriverName,
                               vehicleNumber = vh.regNumber,
                               destination = GetlocationsbyID(l.DestinationId.GetValueOrDefault())
                           }).ToList().GroupBy(i => i.ManifestId).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }



        public List<GroupShipmentListModel> GetGroupByArrivalId(int arrivalId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.GroupWaybillNumMappings
                           join dp in db.Locations on l.DepartureId equals dp.id
                           join des in db.Locations on l.DestinationId equals des.id
                           join shp in db.shipments on l.WaybillNumber equals shp.Waybill
                           join mp in db.ManifestMappings on l.GroupWaybillNumber equals mp.GroupWaybillNumber
                           join mn in db.Manifests on mp.ManifestId equals mn.ManifestId
                           join gp in db.GroupWayBillNumbers on l.GroupWaybillNumber equals gp.GroupWaybillCode
                           where l.DestinationId == arrivalId && gp.hasManifest == true && /*mn.IsReceived == false &&*/ (l.DateCreated >= fromDate && l.DateCreated <= toDate.AddDays(1))
                           orderby des.locationName descending
                           select new GroupShipmentListModel
                           {
                               dateTime = l.DateCreated,
                               DepartureName = dp.locationName,
                               DestinationName = des.locationName,
                               Waybillnum = l.WaybillNumber,
                               GroupNumber = l.GroupWaybillNumber,
                               manifestID = mn.ManifestId,
                               manifestNumber = mn.ManifestNumber,
                               HasArrived = shp.HasArrived,
                               CreatedBy = mn.CreatedBy
                           }).ToList().GroupBy(i => i.GroupNumber).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }
        public List<GroupShipmentListModel> GetGroupWithoutArrivalId(DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.GroupWaybillNumMappings
                           join dp in db.Locations on l.DepartureId equals dp.id
                           join des in db.Locations on l.DestinationId equals des.id
                           join shp in db.shipments on l.WaybillNumber equals shp.Waybill
                           join mp in db.ManifestMappings on l.GroupWaybillNumber equals mp.GroupWaybillNumber
                           join mn in db.Manifests on mp.ManifestId equals mn.ManifestId
                           join gp in db.GroupWayBillNumbers on l.GroupWaybillNumber equals gp.GroupWaybillCode
                           where gp.hasManifest == true && /*mn.IsReceived == false &&*/ (l.DateCreated >= fromDate && l.DateCreated <= toDate.AddDays(1))
                           orderby des.locationName descending
                           select new GroupShipmentListModel
                           {
                               dateTime = l.DateCreated,
                               DepartureName = dp.locationName,
                               DestinationName = des.locationName,
                               Waybillnum = l.WaybillNumber,
                               GroupNumber = l.GroupWaybillNumber,
                               manifestID = mn.ManifestId,
                               manifestNumber = mn.ManifestNumber,
                               HasArrived = shp.HasArrived,
                               CreatedBy = mn.CreatedBy
                           }).ToList().GroupBy(i => i.GroupNumber).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }

        public List<ManifestModel> GetManifestByManifestId(string manifestId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.Manifests
                           join dp in db.Locations on l.DepartId equals dp.id
                           join dn in db.Drivers on l.DriverId equals dn.Id
                           join vh in db.Vehicles on l.VehicleId equals vh.id
                           where l.ManifestNumber == manifestId
                           select new ManifestModel
                           {
                               DateCreated = l.DateCreated,
                               departureLocation = dp.locationName,
                               dispatchedBy = l.DispatchedById,
                               IsDispatched = l.IsDispatched,
                               DispatchFee = l.DispatchFee,
                               driverName = dn.DriverName,
                               vehicleNumber = vh.regNumber,
                               IsReceived = l.IsReceived,
                               ManifestId = l.ManifestId,
                               ManifestNumber = l.ManifestNumber,
                               IsReceivedBy = l.ReceiverById
                           }).ToList().GroupBy(i => i.ManifestId).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }

        public List<ShipmentModel> SeachShipment(string waybillnumber, DateTime fromdate, DateTime todate)
        {
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    List<ShipmentItem> item = new List<ShipmentItem>();
                    var dtcx = (from l in db.shipments
                                join dpId in db.Locations on l.departureLocationId equals dpId.id
                                join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                                join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                                join arrId in db.Locations on l.destinationId equals arrId.id
                                join depstate in db.States on l.senderStateId equals depstate.id
                                join arrState in db.States on l.receiverStateId equals arrState.id
                                join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                              into manimaps
                                from manimap in manimaps.DefaultIfEmpty()

                                join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                                into manifests
                                from manifest in manifests.DefaultIfEmpty()

                                join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                                into Vhcs
                                from Vhc in Vhcs.DefaultIfEmpty()

                                where (l.Waybill == waybillnumber || l.SenderPhoneNumber == waybillnumber || l.BookingRefCode == waybillnumber) &&
                                (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
                                select new ShipmentModel
                                {
                                    Id = l.Id,
                                    Waybill = l.Waybill,
                                    createdBy = l.createdBy,
                                    CustomerType = ctType.CusType,
                                    DeliveryTime = l.DeliveryTime,
                                    DeliveryType = dlType.name,
                                    departureLocation = dpId.locationName,
                                    departureState = depstate.name,
                                    destinationLocation = arrId.locationName,
                                    destinationState = arrState.name,
                                    GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                    description = l.description,
                                    TotalWeight = l.itemsWeight,
                                    vat = l.vat,
                                    paymentMethod = l.paymentMethod,
                                    receiverName = l.receiverName,
                                    receiverPhoneNumber = l.receiverPhoneNumber,
                                    PaymentStatus = l.PaymentStatus,
                                    senderAddress = l.senderAddress,
                                    senderName = l.SenderName,
                                    DateCreated = l.DateCreated,
                                    Items = item.ToModelList(),
                                    GroupNumber = l.groupId,
                                    BookingRefCode = l.BookingRefCode,
                                    ManifestNumber = manifest.ManifestNumber,
                                    VehicleRegNumber = Vhc.regNumber,
                                    isCancelled = l.isCancelled,
                                    IsRefund = l.IsRefund

                                });
                    var ans = dtcx.GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                    return ans;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<ShipmentModel> SeachCustomerHistory(string phonenumber, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            //join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            where l.SenderPhoneNumber == phonenumber &&
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList()
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }


        public List<ShipmentModel> SeachAllShipment(DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()
                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()
                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()
                            join sc in db.ShipmentCollections on l.Waybill equals sc.wayBillNumber
                            into scs
                            from sc in scs.DefaultIfEmpty()
                            where (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
                            orderby l.DateCreated descending
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = null,//ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = null,//depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = null,//arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                deClearedValue = l.deClearedValue,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                senderphone = l.SenderPhoneNumber,
                                IsRefund = l.IsRefund,
                                HasArrived = l.HasArrived,
                                ShipmentStatus = GetShipmentStatus(l.Waybill),
                                ReleasedBy = sc.releasedBy,
                                ReleasedDate = sc.DateCreated
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).ToList();
                return dtcx.ToList();
            }
        }
        public List<ShipmentModel> SeachAllShipment(DateTime fromdate, DateTime todate, string waybillOrPhoneOrBookingRefCode)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id

                            join SId in db.ServiceTypes on l.ServiceType equals SId.ID
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id

                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()

                            where (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1)) && (l.Waybill == waybillOrPhoneOrBookingRefCode || l.SenderPhoneNumber == waybillOrPhoneOrBookingRefCode || l.BookingRefCode == waybillOrPhoneOrBookingRefCode)
                            orderby l.DateCreated descending
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = null,//ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.id.ToString(),
                                ServiceType = SId.ID,
                                departureLocation = dpId.locationName,
                                departureState = null,//depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = null,//arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                senderphone = l.SenderPhoneNumber,//customerdata.phoneNumber,
                                IsRefund = l.IsRefund,
                                HasArrived = l.HasArrived,
                                ShipmentStatus = GetShipmentStatus(l.Waybill),
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).ToList();
                return dtcx.ToList();
            }
        }


        public List<ShipmentModel> SearchShipment(DateTime fromdate, DateTime todate, string waybillOrPhoneOrBookingRefCode)
        {

                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in _context.Shipments
                            where (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1)) && (l.Waybill == waybillOrPhoneOrBookingRefCode || l.SenderPhoneNumber == waybillOrPhoneOrBookingRefCode || l.BookingRefCode == waybillOrPhoneOrBookingRefCode)
                            orderby l.DateCreated descending
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                DeliveryTime = l.DeliveryTime,
                                departureState = null,//depstate.name,
                                destinationState = null,//arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                isCancelled = l.isCancelled,
                                senderphone = l.SenderPhoneNumber,//customerdata.phoneNumber,
                                IsRefund = l.IsRefund,
                                HasArrived = l.HasArrived,
                                ShipmentStatus = GetShipmentStatus(l.Waybill),
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).ToList();
                return dtcx.ToList();
            
        }


        public List<ShipmentCollectionModel> CheckCollecedItems(string waybillNumber)
        {

                var dtx = (from l in _context.ShipmentCollections
                           where l.WayBillNumber == waybillNumber
                           select new ShipmentCollectionModel
                           {
                               waybillNumber = l.WayBillNumber,
                               Datemodified = l.DateModified,
                               DateReleased = l.DateCreated,
                               MeansOfId = l.MeansOfID,
                               ReceiverAddress = l.Address,
                               ReceiverEmail = l.Email,
                               Receivername = l.ReceiverName,
                               ReceiverPhone = l.PhoneNumber,
                               Releasedby = l.ReleasedBy
                           }).ToList();
                return dtx;
            
        }
        public bool CheckcollectionStatus(string waybill)
        {

                var dtx = (from l in _context.ShipmentCollections
                           where l.WayBillNumber == waybill
                           select l.WayBillNumber).FirstOrDefault();
                if (dtx != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public bool CheckComplaintStatus(bool responded)
        {

                var dtx = (from c in db.Complaints
                           where c.Responded == responded
                           select c.Responded).FirstOrDefault();
                if (dtx == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public string GetWhoReleasedItem(string waybill)
        {

                var dtx = (from l in _context.Shipments
                           where l.WayBillNumber == waybill
                           select new { l.ReleasedBy, l.DateCreated }).FirstOrDefault();
                if (dtx != null)
                {
                    return dtx.ReleasedBy + " on " + "(" + dtx.DateCreated + ")";
                }
                else
                {
                    return "Not Yet Released";
                }
        }
        public List<ShipmentModel> SeachShipmentByWayBill(string waybillnumber)
        {
            bool collected = CheckcollectionStatus(waybillnumber);

                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in _context.Shipments
                            join dpId in _context.Locations on l.DepartureLocationId equals dpId.Id
                            join dlType in _context.Deliverytypes on l.DeliveryTypeId equals dlType.Id
                            //join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in _context.Locations on l.DestinationId equals arrId.Id
                            join depstate in _context.States on l.SenderStateId equals depstate.Id
                            join arrState in _context.States on l.ReceiverStateId equals arrState.Id
                            //join customerdata in db.customers on l.customerId equals customerdata.id
                            join manimap in _context.ManifestMappings on l.GroupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in _context.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in _context.Vehicles on manifest.VehicleId equals Vhc.Id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()
                            where (l.Waybill == waybillnumber || l.SenderPhoneNumber == waybillnumber)
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                //CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                DateCreated = l.DateCreated,
                                Released = collected,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund

                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
           
        }

        public List<returnmsg> ReleaseShipment(ShipmentCollection collection, string phonenum)
        {
            return ReleaseShipment(collection.WayBillNumber, phonenum, collection.Address, collection.ReceiverName, collection.PhoneNumber, collection.MeansOfID, collection.ReleasedBy, collection.Addcomment);
        }

        public List<returnmsg> ReleaseShipment(string waybillNum, string phonenum, string address, string rname, string rphone, string meansId, string userName, string comment)
        {
            string msg = "Your shipment with waybill number:" + waybillNum + " has been delivered. Thank you for using Libmot Express";
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                    ShipmentCollection collection = new ShipmentCollection();
                    collection.Address = address;
                    collection.DateCreated = DateTime.Now;
                    collection.DateModified = DateTime.Now;
                    collection.IsDeleted = false;
                    collection.PhoneNumber = rphone;
                    collection.ReceiverName = rname;
                    collection.ReleasedBy = userName;
                    collection.WayBillNumber = waybillNum;
                    collection.MeansOfID = meansId;
                    collection.Addcomment = comment;
                    //_context.ShipmentCollections.InsertOnSubmit(collection);
                    //UpdateIsCollected(waybillNum);
                    //db.SubmitChanges();
                    List<string> num = new List<string>();

                    num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + phonenum);

                    RequestSMS sms = new RequestSMS();
                    sms.id = Guid.NewGuid().ToString();
                    sms.to = num;
                    sms.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                    sms.body = msg;
                    KonnectAPI konnectAPI = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                    konnectAPI.SendSMS(sms);
                    //SendSms(phonenum, msg);
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Shipment released";
                    retmsgs.Add(rtmsg);
                    #region Update the Shipment Tracking indicating that the shipment has been collected
                    var trackingData = (from t in db.ShipmentTrackings where t.Waybill == waybillNum select t).FirstOrDefault();
                    var destinationId = db.shipments.Where(s => s.Waybill == waybillNum).FirstOrDefault().destinationId;
                    if (trackingData == null)
                    {

                        UpdateshipmentStatus(waybillNum, db.Locations.Where(l => l.id == destinationId).FirstOrDefault().locationName, LibmotExpressConstants.ShipmentTrackingStatus.Collected/*"Collected"*/);
                    }
                    else
                    {
                        //trackingData.Status = LibmotExpressConstants.ShipmentTrackingStatus.Collected;//"Collected";
                        //trackingData.DateModified = DateTime.Now;
                        //trackingData.Location = db.Locations.Where(l => l.id == destinationId).FirstOrDefault().locationName;
                        // db.SubmitChanges();
                        var locationName = db.Locations.Where(l => l.id == destinationId).FirstOrDefault().locationName;
                        UpdateshipmentStatus(waybillNum, locationName, LibmotExpressConstants.ShipmentTrackingStatus.Collected);
                    }
                    #endregion
                    return retmsgs;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                rtmsg.completed = true;
                rtmsg.code = "Error";
                rtmsg.errormsg = "Item already collected";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }




        public bool UpdateShipmentGroup(string waybilnumber, string groupNumber)
        {

                Shipment shpmt = new Shipment();
                try
                {
                    List<Shipment> shp = (from p in db.shipments
                                          where p.Waybill == waybilnumber
                                          select p).ToList();
                    if (shp.Count() > 0)
                    {
                        foreach (Shipment l in shp)
                        {
                            l.groupId = groupNumber;
                            l.DateModified = DateTime.Now;
                        }
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            
        }


        public bool UpdateShipmentGroupManifest(string groupNumber, bool hasmanifest)
        {
            using (var db = new liblogisticsDataContext())
            {
                try
                {
                    List<GroupWayBillNumber> shp = (from p in db.GroupWayBillNumbers
                                                    where p.GroupWaybillCode == groupNumber
                                                    select p).ToList();
                    if (shp.Count() > 0)
                    {
                        foreach (GroupWayBillNumber l in shp)
                        {
                            l.hasManifest = hasmanifest;
                            l.DateModified = DateTime.Now;

                        }
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool RemovefromGroupMapping(string waybillnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                GroupWaybillNumMapping shpmt = new GroupWaybillNumMapping();
                try
                {
                    var shp = (from p in db.GroupWaybillNumMappings
                               where p.WaybillNumber == waybillnumber
                               select p).FirstOrDefault();
                    if (shp != null)
                    {
                        db.GroupWaybillNumMappings.DeleteOnSubmit(shp);
                        db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public bool UpdateShipmentGroupRemove(string waybilnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                try
                {
                    List<shipment> shp = (from p in db.shipments
                                          where p.Waybill == waybilnumber
                                          select p).ToList();
                    if (shp.Count() > 0)
                    {
                        foreach (shipment l in shp)
                        {
                            l.groupId = null;
                            l.DateModified = DateTime.Now;
                        }
                        db.SubmitChanges();
                        RemovefromGroupMapping(waybilnumber);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public bool UpdateShipmentManifest(string Groupwaybilnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                try
                {
                    var shp = (from p in db.ManifestMappings
                               where p.GroupWaybillNumber == Groupwaybilnumber
                               select p).FirstOrDefault();
                    db.ManifestMappings.DeleteOnSubmit(shp);
                    db.SubmitChanges();
                    UpdateShipmentGroupManifest(Groupwaybilnumber, false);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
        public List<returnmsg> AddToGroup(string waybilnumber, int deptId, int arriId, string GroupWaybilnum)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    GroupWaybillNumMapping numMapping = new GroupWaybillNumMapping();
                    numMapping.DateCreated = DateTime.Now;
                    numMapping.DepartureId = deptId;
                    numMapping.DestinationId = arriId;
                    numMapping.GroupWaybillNumber = GroupWaybilnum;
                    numMapping.WaybillNumber = waybilnumber;
                    numMapping.IsDeleted = false;
                    numMapping.IsActive = true;
                    db.GroupWaybillNumMappings.InsertOnSubmit(numMapping);
                    db.SubmitChanges();
                    UpdateShipmentGroup(waybilnumber, GroupWaybilnum);
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Added to Group";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int GetManifestId(string manifestNumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from s in db.Manifests
                                where s.ManifestNumber == manifestNumber
                                select s.ManifestId).FirstOrDefault();
                return statdata;

            }
        }
        public List<returnmsg> AddToManifest(string GroupWayBillNum, int manifestID)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            ManifestMapping dbb = new ManifestMapping();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    List<ManifestMapping> mnp = (from p in db.ManifestMappings
                                                 where p.GroupWaybillNumber == GroupWayBillNum
                                                 select p).ToList();
                    if (mnp.Count > 0)
                    {
                        rtmsg.completed = true;
                        rtmsg.code = "Success";
                        rtmsg.successmsg = "Already Exist";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                    else
                    {
                        ManifestMapping manMapping = new ManifestMapping();
                        manMapping.ManifestId = manifestID;
                        manMapping.DateCreated = DateTime.Now;
                        manMapping.GroupWaybillNumber = GroupWayBillNum;
                        manMapping.IsDeleted = false;
                        manMapping.IsActive = true;
                        db.ManifestMappings.InsertOnSubmit(manMapping);
                        db.SubmitChanges();
                        UpdateShipmentGroupManifest(GroupWayBillNum, true);
                        rtmsg.completed = true;
                        rtmsg.code = "Success";
                        rtmsg.successmsg = "Added to Manifest";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<returnmsg> GenerateManifest(string ManifestNumber, int deptId, int? destId, string createdby)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    Manifest manifestnum = new Manifest();
                    manifestnum.ManifestNumber = ManifestNumber;
                    manifestnum.DepartId = deptId;
                    manifestnum.DestinationId = destId;
                    manifestnum.DateCreated = DateTime.Now;
                    manifestnum.DriverId = 4;
                    manifestnum.VehicleId = 4;
                    manifestnum.IsReceived = false;
                    manifestnum.CreatedBy = createdby;
                    db.Manifests.InsertOnSubmit(manifestnum);
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Manifest created";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {
                rtmsg.completed = true;
                rtmsg.code = "Error";
                rtmsg.errormsg = "Could not generate manifest";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        public List<returnmsg> CancelShipment(int shipmentId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    List<shipment> mnp = (from p in db.shipments
                                          where p.Id == shipmentId
                                          select p).ToList();
                    if (mnp.Count > 0)
                    {
                        foreach (shipment l in mnp)
                        {
                            l.isCancelled = true;
                            l.DateModified = DateTime.Now;
                        }
                        db.SubmitChanges();
                        rtmsg.completed = true;
                        rtmsg.code = "Success";
                        rtmsg.successmsg = "Shipment Cancelled";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                    else
                    {
                        rtmsg.completed = false;
                        rtmsg.code = "Error";
                        rtmsg.successmsg = "Unable to cancel shipment";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        public List<returnmsg> DeleteShipment(int shipmentId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    List<shipment> mnp = (from p in db.shipments
                                          where p.Id == shipmentId
                                          select p).ToList();
                    if (mnp.Count > 0)
                    {
                        foreach (shipment l in mnp)
                        {
                            l.isDeleted = true;
                            l.DateModified = DateTime.Now;
                        }
                        db.SubmitChanges();
                        rtmsg.completed = true;
                        rtmsg.code = "Success";
                        rtmsg.successmsg = "Shipment deleted";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                    else
                    {
                        rtmsg.completed = false;
                        rtmsg.code = "Error";
                        rtmsg.successmsg = "Unable to delete shipment";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        public List<returnmsg> DeleteShipmentItem(int shipmentItemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    List<ShipmentItem> mnp = (from p in db.ShipmentItems
                                              where p.id == shipmentItemId
                                              select p).ToList();
                    if (mnp.Count > 0)
                    {
                        foreach (ShipmentItem l in mnp)
                        {
                            l.IsDeleted = true;
                            l.DateModified = DateTime.Now;
                        }
                        db.SubmitChanges();
                        rtmsg.completed = true;
                        rtmsg.code = "Success";
                        rtmsg.successmsg = "Shipment item deleted";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                    else
                    {
                        rtmsg.completed = false;
                        rtmsg.code = "Error";
                        rtmsg.successmsg = "Unable to delete shipment item";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        public List<returnmsg> GenerateGroupWayBill(string Gwaybilnum, int deptId, int ArriId, string CreatedBy)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    GroupWayBillNumber groupWayBill = new GroupWayBillNumber();
                    groupWayBill.GroupWaybillCode = Gwaybilnum;
                    groupWayBill.createdBy = CreatedBy;
                    groupWayBill.IsActive = true;
                    groupWayBill.IsDeleted = false;
                    groupWayBill.departureId = deptId;
                    groupWayBill.arrivalId = ArriId;
                    groupWayBill.hasManifest = false;
                    groupWayBill.DateCreated = DateTime.Now;
                    groupWayBill.SentToHub = false;
                    db.GroupWayBillNumbers.InsertOnSubmit(groupWayBill);
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Group Waybill created";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {
                rtmsg.completed = true;
                rtmsg.code = "Error";
                rtmsg.errormsg = "Could not generate group waybill";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        public List<GroupWayBillNumberModel> GetListOfGroupWayBillNumbers(int destId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from s in db.GroupWayBillNumbers
                                where s.departureId == destId
                                select new GroupWayBillNumberModel
                                {
                                    arrivalId = s.departureId,
                                    waybillNumber = s.GroupWaybillCode
                                }).ToList();
                return statdata;

            }
        }
        public List<GroupWayBillNumberModel> GetListOfGroupWayBillNumberByDepAr(int dept, int destId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var statdata = (from s in db.GroupWayBillNumbers
                                where s.departureId == dept && s.arrivalId == destId
                                select new GroupWayBillNumberModel
                                {
                                    arrivalId = s.departureId,
                                    waybillNumber = s.GroupWaybillCode
                                }).ToList();
                return statdata;

            }
        }


        public List<ShipmentItemModel> GeteachShipmentItem(int shipmentId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.ShipmentItems
                            join sm in db.shipments on l.ShipmentId equals sm.Id
                            where l.ShipmentId == shipmentId
                            select new ShipmentItemModel
                            {
                                Id = sm.Id,
                                wayBillNum = sm.Waybill,
                                DateCreated = l.DateCreated,
                                shItemCondition = l.itemNature,
                                ItemDesc = l.itemDescription,
                                ItemWeight = l.itemWeight,
                                Price = l.price,
                                Quantity = l.quantity,
                                senderphone = sm.SenderPhoneNumber,
                                senderName = sm.SenderName,
                                receiverPhone = sm.receiverPhoneNumber,
                                receiverName = sm.receiverName,
                                IsCredit = sm.IsCredit,
                                PaymentMethod = sm.paymentMethod
                            }).ToList();
                return dtcx;
            }
        }
        public bool getIsCreditShipment(int shipmentId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var get = (from l in db.ShipmentItems
                           join sm in db.shipments on l.ShipmentId equals sm.Id
                           where l.ShipmentId == shipmentId
                           select new ShipmentItemModel
                           {
                               IsCredit = sm.IsCredit,
                           }).FirstOrDefault();
                if (get?.IsCredit == true)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        public bool UpdateCreditShipment(int shipmentId, string waybillNum, string phonenum, string address, string rname, string rphone, string meansId, string userName, string comment)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();

            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();
                List<shipment> items = (from p in dt.shipments
                                        where p.IsCredit == true && p.Id == shipmentId
                                        select p).ToList();

                foreach (shipment p in items)
                {
                    p.IsCredit = false;
                    p.CreditPaymentDate = (DateTime?)DateTime.Now;

                    dt.SubmitChanges();

                    ReleaseShipment(waybillNum, phonenum, address, rname, rphone, meansId, userName, comment);
                }
            }
            catch (Exception ex)
            {

            }

            return true;
        }
        public List<ManifestInformation> GetManifestInformation(string manifestId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.Manifests
                            join dr in db.Drivers on l.DriverId equals dr.Id
                            join vh in db.Vehicles on l.VehicleId equals vh.id
                            join jour in db.JourneyManagements on l.JourneyManagementId equals jour.Id into jours
                            from jour in jours.DefaultIfEmpty()
                            where l.ManifestNumber == manifestId
                            select new ManifestInformation
                            {
                                drivername = dr.DriverName,
                                driverphone = dr.DriverPhone,
                                manifestdate = l.DateCreated.ToString(),
                                Vehicleinfo = vh.regNumber,
                                JourneyCode = jour.JourneyCode ?? "No Journey Code Assigned"
                            }).ToList();
                return dtcx;

            }
        }
        public List<GroupManifestModel> GetManifestGroup(string manifestId)
        {

            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.ManifestMappings
                            join sm in db.GroupWayBillNumbers on l.GroupWaybillNumber equals sm.GroupWaybillCode
                            join wn in db.GroupWaybillNumMappings on l.GroupWaybillNumber equals wn.GroupWaybillNumber
                            join manid in db.Manifests on l.ManifestId equals manid.ManifestId
                            join ides in db.Locations on sm.arrivalId equals ides.id
                            join idp in db.Locations on sm.departureId equals idp.id
                            join shp in db.shipments on sm.GroupWaybillCode equals shp.groupId
                            join shpitems in db.ShipmentItems on shp.Id equals shpitems.ShipmentId
                            join dr in db.Drivers on manid.DriverId equals dr.Id
                            join vh in db.Vehicles on manid.VehicleId equals vh.id
                            let waybills = db.shipments.Where(w => w.groupId == shp.groupId)
                            //let manifestDetails = db.Manifests.Where(n => n.ManifestId == l.ManifestId)
                            let shpItems = db.ShipmentItems.Where(z => z.ShipmentId == shp.Id)
                            where manid.ManifestNumber == manifestId
                            select new GroupManifestModel
                            {
                                manifestnum = manid.ManifestNumber,
                                Driver = dr.DriverName,
                                DriverPhone = dr.DriverPhone,
                                Vehiclename = vh.regNumber,
                                manifestdate = manid.DateCreated,
                                //manifestinfo = manifestDetails.Any() ? manifestDetails.ToList() : new List<Manifest>(),
                                GroupWayBillNumber = sm.GroupWaybillCode,
                                destination = ides.locationName,
                                shipmentItemList = shpItems.Any() ? shpItems.ToList() : new List<ShipmentItem>(),
                                shipmentItem = waybills.Any() ? waybills.ToList() : new List<shipment>()
                            });
                return dtcx.ToList().GroupBy(x => x.GroupWayBillNumber)
                    .Select(g => g.FirstOrDefault()).ToList();
            }
        }

        public string GetGroupIdbyManifestNumber(string manifestnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = from l in db.Manifests
                           join mp in db.ManifestMappings on l.ManifestId equals mp.ManifestId
                           where l.ManifestNumber == manifestnumber
                           select mp.GroupWaybillNumber;
                return dtcx.FirstOrDefault();
            }
        }
        public List<string> GetListOfGroupIdbyManifestNumber(string manifestnumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = from l in db.Manifests
                           join mp in db.ManifestMappings on l.ManifestId equals mp.ManifestId
                           where l.ManifestNumber == manifestnumber
                           select mp.GroupWaybillNumber;
                return dtcx.ToList();
            }
        }
        public bool removeFromGList(string groupnumber)
        {

            using (var ctx = new liblogisticsDataContext())
            {
                var x = (from y in ctx.GroupshipmentLists
                         where y.groupnumber == groupnumber
                         select y).FirstOrDefault();
                ctx.GroupshipmentLists.DeleteOnSubmit(x);
                ctx.SubmitChanges();
            }
            return true;
        }
        public List<ShipmentItemModel> GetGroupShipmentItem(string groupId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcxRevised = (from l in db.ShipmentItems
                                   join sm in db.shipments on l.ShipmentId equals sm.Id
                                   join ides in db.Locations on sm.destinationId equals ides.id
                                   join idp in db.Locations on sm.departureLocationId equals idp.id
                                   where sm.groupId == groupId
                                   select new ShipmentItemModel
                                   {
                                       wayBillNum = sm.Waybill,
                                       Shpdeparture = idp.locationName,
                                       Shpdestination = ides.locationName,
                                       DateCreated = l.DateCreated,
                                       shItemCondition = l.itemNature,
                                       ItemDesc = l.itemDescription,
                                       ItemWeight = l.itemWeight,
                                       Price = l.price,
                                       Quantity = l.quantity,
                                       HasArrived = sm.HasArrived,
                                       Groupnumber = sm.groupId,
                                       TotalToPay = sm.totalTopay
                                   }).ToList();

                return dtcxRevised;
            }
        }
        public List<ShipmentItemModel> GetGroupShipmentItem(List<string> groupIds)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItemModel> resp = new List<ShipmentItemModel>();
                foreach (var groupId in groupIds)
                {
                    var dtcxRevised = (from l in db.ShipmentItems
                                       join sm in db.shipments on l.ShipmentId equals sm.Id
                                       join ides in db.Locations on sm.destinationId equals ides.id
                                       join idp in db.Locations on sm.departureLocationId equals idp.id
                                       where sm.groupId == groupId
                                       select new ShipmentItemModel
                                       {
                                           wayBillNum = sm.Waybill,
                                           Shpdeparture = idp.locationName,
                                           Shpdestination = ides.locationName,
                                           DateCreated = l.DateCreated,
                                           shItemCondition = l.itemNature,
                                           ItemDesc = l.itemDescription,
                                           ItemWeight = l.itemWeight,
                                           Price = l.price,
                                           Quantity = l.quantity,
                                           HasArrived = sm.HasArrived,
                                           Groupnumber = sm.groupId,
                                           TotalToPay = sm.totalTopay
                                       }).ToList();
                    resp.AddRange(dtcxRevised);
                    //return dtcxRevised;
                }
                return resp;
            }
        }
        public List<ShipmentItemModel> GetLocationShipmentItem(int locationId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.ShipmentItems
                            join sm in db.shipments on l.ShipmentId equals sm.Id
                            join ides in db.Locations on sm.destinationId equals ides.id
                            join idp in db.Locations on sm.departureLocationId equals idp.id
                            where sm.destinationId == locationId
                            select new ShipmentItemModel
                            {
                                wayBillNum = sm.Waybill,
                                Shpdeparture = idp.locationName,
                                Shpdestination = ides.locationName,
                                DateCreated = l.DateCreated,
                                shItemCondition = l.itemNature,
                                ItemDesc = l.itemDescription,
                                ItemWeight = l.itemWeight,
                                Price = l.price,
                                Quantity = l.quantity,
                                HasArrived = sm.HasArrived,
                                Groupnumber = sm.groupId,
                                TotalToPay = sm.totalTopay
                            }).GroupBy(s => s.wayBillNum).Select(g => g.FirstOrDefault()).ToList();
                return dtcx;
            }
        }
        public List<ShipmentItemModel> ValidateIsrecieveshipmentItem(string groupId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtcx = (from l in db.ShipmentItems
                            join sm in db.shipments on l.ShipmentId equals sm.Id
                            join ides in db.Locations on sm.destinationId equals ides.id
                            join idp in db.Locations on sm.departureLocationId equals idp.id
                            where sm.groupId == groupId && sm.HasArrived == false
                            select new ShipmentItemModel
                            {
                                wayBillNum = sm.Waybill,
                                Shpdeparture = idp.locationName,
                                Shpdestination = ides.locationName,
                                DateCreated = l.DateCreated,
                                shItemCondition = l.itemNature,
                                ItemDesc = l.itemDescription,
                                ItemWeight = l.itemWeight,
                                Price = l.price,
                                Quantity = l.quantity,
                                HasArrived = sm.HasArrived,
                                Groupnumber = sm.groupId
                            }).GroupBy(s => s.wayBillNum).Select(g => g.FirstOrDefault()).ToList();
                return dtcx;
            }
        }
        //Get Total Amount of Manifest
        public decimal? GetTotalAmountPerGroup(string groupId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdataRevised = (from l in db.ShipmentItems
                                     join sm in db.shipments on l.ShipmentId equals sm.Id
                                     join ides in db.Locations on sm.destinationId equals ides.id
                                     join idp in db.Locations on sm.departureLocationId equals idp.id
                                     where sm.groupId == groupId
                                     select l.price).Sum();//revised

                return ctdataRevised;
            }

        }
        public decimal? GetTotalDeclearedValuePerGroup(string groupId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdataRevised = (from l in db.ShipmentItems
                                     join sm in db.shipments on l.ShipmentId equals sm.Id
                                     join ides in db.Locations on sm.destinationId equals ides.id
                                     join idp in db.Locations on sm.departureLocationId equals idp.id
                                     where sm.groupId == groupId
                                     select sm.deClearedValue).Sum();

                return ctdataRevised;
            }

        }
        //Get Total Weight of Manifest
        public decimal? GetTotalWeightPerGroup(string groupId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from l in db.ShipmentItems
                              join sm in db.shipments on l.ShipmentId equals sm.Id
                              join ides in db.Locations on sm.destinationId equals ides.id
                              join idp in db.Locations on sm.departureLocationId equals idp.id
                              where sm.groupId == groupId
                              select l.itemWeight).Sum();

                return ctdata;
            }

        }

        //Reports Services
        public IList<SalesPerWeekModel> GetSalesPerWeek()
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                                  //join shp in db.ShipmentItems on s.Id equals shp.ShipmentId
                              where s.DateCreated >= DateTime.Now.AddDays(-7) && s.DateCreated <= DateTime.Now && s.isCancelled == false
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              select new SalesPerWeekModel
                              {
                                  DateofSales = g.FirstOrDefault().DateCreated,
                                  TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              }).ToList();

                return ctdata;
            }
        }

        public IList<SalesPerLocationModel> GetSalesPerLocation(int locationId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1) &&
                              s.departureLocationId == locationId && s.isCancelled == false
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  GrandTotal = g.Select(x => x.grandTotal).Sum().ToString(),
                                  LocationId = g.FirstOrDefault().departureLocationId,
                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                              }).ToList();

                return ctdata;
            }
        }
        public IList<SalesPerLocationModel> GetSalesPerLocationGroupByUser(int locationId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1) &&
                              s.departureLocationId == locationId && s.isCancelled == false && s.PaymentStatus != 0
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day, s.createdBy } into g
                              //group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.First().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  LocationName = g.First().Location.locationName,
                                  GrandTotal = g.Select(x => x.grandTotal).Sum().ToString(),
                                  LocationId = g.First().departureLocationId,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                                  CreatedBy = g.First().createdBy
                              }).ToList();

                return ctdata;
            }
        }
        public IList<SalesPerLocationModel> GetSalesPerLocationByPayTypeGroupByUser(int locationId, DateTime fromDate, DateTime toDate, string paymentmethod)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1) &&
                              s.departureLocationId == locationId && s.paymentMethod == paymentmethod
                               && s.isCancelled == false
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day, s.createdBy } into g
                              //group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  GrandTotal = g.Select(x => x.grandTotal).Sum().ToString(),
                                  LocationId = g.FirstOrDefault().departureLocationId,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                                  CreatedBy = g.FirstOrDefault().createdBy
                              }).ToList();

                return ctdata;
            }
        }
        public IList<SalesPerLocationModel> GetSalesForAllLocations(DateTime fromDate, DateTime toDate)
        {
            var ctdata = (from s in db.shipments
                          join lname in db.Locations on s.departureLocationId equals lname.id
                          where s.DateCreated >= fromDate.Date && s.DateCreated <= toDate.Date.AddDays(1).AddHours(-1)
                          && s.isCancelled == false
                          let ln = s.departureLocationId
                          let dt = s.DateCreated
                          let pm = s.paymentMethod
                          group s by new { y = lname.id } into g
                          let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                          let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                          let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                          select new SalesPerLocationModel
                          {
                              DateofSales = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),/*Date Range*/
                              TotalSalesPerDay = TotalSalesPerDay,/* Total Sales Per Given Interval*/
                              TotalVat = TotalVat,
                              TotalPkFee = TotalPkFee,
                              TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                              TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                              TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                              TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                              TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                              LocationName = g.FirstOrDefault().Location.locationName,
                              LocationId = g.FirstOrDefault().departureLocationId
                          });/*TotalOthers*/
            return ctdata.ToList().GroupBy(x => x.LocationName)
                .Select(g => g.FirstOrDefault()).ToList();
        }
        public IList<SalesPerLocationModel> GetSalesForAllLocationsGroupByUser(DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.isCancelled == false && s.PaymentStatus != 0
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              let pm = s.paymentMethod
                              group s by new { y = s.createdBy } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId,
                                  CreatedBy = g.FirstOrDefault().createdBy
                              });
                return ctdata.ToList().GroupBy(x => x.CreatedBy)
                    .Select(g => g.FirstOrDefault()).OrderBy(x => x.DateofSales).ToList(); ;
            }
        }
        public IList<SalesPerLocationModel> GetSalesForAllLocationsByPayTypeGroupByUser(DateTime fromDate, DateTime toDate, string paymentmethod)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.isCancelled == false && s.paymentMethod == paymentmethod
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              let pm = s.paymentMethod
                              group s by new { y = s.createdBy } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.First().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                                  LocationName = g.First().Location.locationName,
                                  LocationId = g.First().departureLocationId,
                                  CreatedBy = g.First().createdBy
                              });
                return ctdata.ToList().GroupBy(x => x.CreatedBy)
                    .Select(g => g.First()).OrderBy(x => x.DateofSales).ToList(); ;
            }
        }

        public IList<SalesPerLocationModel> GetSalesPeruser(string useremail, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.createdBy == useremail && s.isCancelled == false
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId,

                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }
        public IList<SalesPerLocationModel> GetSalesPeruserGroupByUser(string useremail, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.createdBy == useremail && s.isCancelled == false && s.PaymentStatus != 0
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day, s.createdBy } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                                  CreatedBy = g.FirstOrDefault().createdBy
                              });
                return ctdata.ToList().GroupBy(x => x.DateofSales)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }
        public IList<SalesPerLocationModel> GetSalesPeruserByPayTypeGroupByUser(string useremail, DateTime fromDate, DateTime toDate, string paymentmethod)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.createdBy == useremail && s.isCancelled == false && s.paymentMethod == paymentmethod
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day, s.createdBy } into g
                              let TotalSalesPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              let TotalVat = g.Sum(d => d.vat).GetValueOrDefault().ToString()
                              let TotalPkFee = g.Sum(f => f.packagingfee).GetValueOrDefault().ToString()
                              select new SalesPerLocationModel
                              {
                                  DateofSales = g.First().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalSalesPerDay = TotalSalesPerDay,
                                  LocationName = g.First().Location.locationName,
                                  LocationId = g.First().departureLocationId,
                                  TotalVat = TotalVat,
                                  TotalPkFee = TotalPkFee,
                                  TotalCash = g.AsEnumerable().Where(f => f.paymentMethod == "Cash").Sum(x => x.grandTotal).ToString(),
                                  TotalPos = g.AsEnumerable().Where(f => f.paymentMethod == "POS").Sum(x => x.grandTotal).ToString(),
                                  TotalTransfer = g.AsEnumerable().Where(f => f.paymentMethod == "Transfer").Sum(x => x.grandTotal).ToString(),
                                  TotalCredit = g.AsEnumerable().Where(f => f.paymentMethod == "Credit").Sum(x => x.grandTotal).ToString(),
                                  TotalCashandPOS = g.AsEnumerable().Where(f => f.paymentMethod == "Cash and POS").Sum(x => x.grandTotal).ToString(),
                                  CreatedBy = g.First().createdBy
                              });
                return ctdata.ToList().GroupBy(x => x.DateofSales)
                    .Select(g => g.First()).ToList(); ;
            }
        }
        public IList<GenericReportModel> GetVatForAll(DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.isCancelled == false
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalPerDay = g.Sum(k => k.vat).GetValueOrDefault().ToString()
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }

        public IList<GenericReportModel> GetPkForAll(DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.isCancelled == false
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalPerDay = g.Sum(k => k.packagingfee).GetValueOrDefault().ToString()
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }

        public IList<GenericReportModel> GetPkPeruser(DateTime fromDate, DateTime toDate, string userId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.isCancelled == false && s.createdBy == userId
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalPerDay = g.Sum(k => k.packagingfee).GetValueOrDefault().ToString()
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }
        public IList<GenericReportModel> GetPkPertype(DateTime fromDate, DateTime toDate, string packagingType)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              && s.isCancelled == false && s.PackagingType == packagingType && s.PackagingType != null
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalPerDay = g.Sum(k => k.packagingfee).GetValueOrDefault().ToString()
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }
        public IList<CancelledShipmentsModel> GetAllCancelledShipments(DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate && s.isCancelled == true
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalPerDay = g.Sum(k => k.grandTotal).GetValueOrDefault().ToString()
                              select new CancelledShipmentsModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId,
                                  CreatedBy = g.FirstOrDefault().createdBy
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }
        public IList<GenericReportModel> GetInsuranceForAll(DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalPerDay = ConvertAmount(Convert.ToDecimal(g.Sum(k => k.insuranceAmount).ToString()))
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }

        public IList<GenericReportModel> GetSalesForAll(DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)
                              let ln = s.departureLocationId
                              let dt = s.DateCreated
                              group s by new { y = lname.id } into g
                              let TotalPerDay = g.Sum(k => k.insuranceAmount).GetValueOrDefault().ToString()
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  LocationId = g.FirstOrDefault().departureLocationId
                              });
                return ctdata.ToList().GroupBy(x => x.LocationName)
                    .Select(g => g.FirstOrDefault()).ToList(); ;
            }
        }

        public IList<GenericReportModel> GetVatPerLocation(int locationId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1) &&
                              s.departureLocationId == locationId
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              let TotalPerDay = g.Sum(k => k.vat).GetValueOrDefault().ToString()
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  GrandTotal = g.Select(x => x.vat).Sum().ToString(),
                                  LocationId = g.FirstOrDefault().departureLocationId
                              }).ToList();

                return ctdata;
            }
        }
        public IList<GenericReportModel> GetPkPerLocation(int locationId, DateTime fromDate, DateTime toDate, string packageType)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where (s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1)) &&
                              s.departureLocationId == locationId ||
                              s.PackagingType == packageType
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              let TotalPerDay = g.Sum(k => k.packagingfee).GetValueOrDefault().ToString()
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = TotalPerDay,
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  GrandTotal = g.Select(x => x.packagingfee).Sum().ToString(),
                                  LocationId = g.FirstOrDefault().departureLocationId,
                                  CreatedBy = g.FirstOrDefault().createdBy,
                                  PackagingType = g.FirstOrDefault().PackagingType
                              }).ToList();

                return ctdata;
            }
        }

        public bool Sendtohub(string groupnumber, int hubId)
        {
            try
            {

                List<GroupWayBillNumber> cvt = (from p in db.GroupWayBillNumbers
                                                where p.GroupWaybillCode == groupnumber
                                                select p).ToList();

                if (cvt.Count() > 0)
                {
                    foreach (GroupWayBillNumber l in cvt)
                    {
                        l.DateModified = DateTime.Now;
                        l.SentToHub = true;
                        l.HubId = hubId;
                    }
                    db.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }


        }

        public IList<CancelledShpModel> GetCancelledShpPerLocation(int locationId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate &&
                              s.departureLocationId == locationId && s.isCancelled == true
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              select new CancelledShpModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = ConvertAmount(Convert.ToDecimal(g.Sum(k => k.grandTotal).ToString())),
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  GrandTotal = g.Select(x => x.vat).Sum().ToString(),
                                  LocationId = g.FirstOrDefault().departureLocationId,
                                  CreatedBy = g.FirstOrDefault().createdBy
                              }).ToList();

                return ctdata;
            }
        }



        public IList<GenericReportModel> GetInsurancePerLocation(int locationId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join lname in db.Locations on s.departureLocationId equals lname.id
                              where s.DateCreated >= fromDate && s.DateCreated <= toDate.AddDays(1) &&
                              s.departureLocationId == locationId
                              let dt = s.DateCreated
                              group s by new { y = dt.Year, m = dt.Month, d = dt.Day } into g
                              select new GenericReportModel
                              {
                                  WayBill = g.FirstOrDefault().Waybill,
                                  DateCreated = g.FirstOrDefault().DateCreated.ToString("ddd, dd MMMM yyyy", culture),
                                  TotalPerDay = ConvertAmount(Convert.ToDecimal(g.Sum(k => k.insuranceAmount).ToString())),
                                  LocationName = g.FirstOrDefault().Location.locationName,
                                  GrandTotal = g.Select(x => x.insuranceAmount).Sum().ToString(),
                                  LocationId = g.FirstOrDefault().departureLocationId
                              }).ToList();

                return ctdata;
            }
        }

        public string GetGroupNumberById(int id)
        {
            try
            {
                liblogisticsDataContext db = new liblogisticsDataContext();
                return (from gn in db.GroupWayBillNumbers where gn.GroupId == Convert.ToInt32(id) select gn).FirstOrDefault().GroupWaybillCode;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        //Shipment Tracking

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public IList<ShipmentModel> TrackShipment(string waybill)
        {
            bool collected = CheckcollectionStatus(waybill);
            using (var db = new liblogisticsDataContext())
            {
                var ctdata = (from s in db.shipments
                              join d in db.Locations on s.departureLocationId equals d.id
                              join a in db.Locations on s.destinationId equals a.id
                              where s.Waybill == waybill
                              select new ShipmentModel
                              {
                                  BookingRefCode = s.BookingRefCode,
                                  Collected = collected,
                                  createdBy = s.createdBy,
                                  CreditPaymentDate = s.CreditPaymentDate,
                                  DeclaredValue = s.deClearedValue != null ? s.deClearedValue.ToString() : null,
                                  departureState = GetStatebyID(s.senderStateId),
                                  destinationState = GetStatebyID(s.receiverStateId != null ? (int)s.receiverStateId : 0),
                                  expectedDateOfArrival = s.expectedDateOfArrival,
                                  GrandTotalValue = s.grandTotal != null ? s.grandTotal.ToString() : null,
                                  GroupNumber = s.groupId != null ? GetGroupNumberById(Convert.ToInt32(s.groupId)) : null,
                                  Id = s.Id,
                                  isCancelled = s.isCancelled,
                                  IsCredit = s.IsCredit,
                                  IsMissing = s.IsMissing,
                                  IsMissingDate = s.IsMissingDate,
                                  IsMissingStatus = s.IsMissingStatus,
                                  isReceived = collected,
                                  IsReceivedDate = s.expectedDateOfArrival,
                                  IsRefund = s.IsRefund,
                                  description = s.description,
                                  packagingfee = s.packagingfee,
                                  paymentMethod = s.paymentMethod,
                                  PaymentStatus = s.PaymentStatus,
                                  PayStackPaymentResponse = s.PayStackPaymentResponse,
                                  PayStackReference = s.PayStackReference,
                                  PayStackResponse = s.PayStackResponse,
                                  receiverName = s.receiverName,
                                  receiverPhoneNumber = s.receiverPhoneNumber,
                                  senderName = s.SenderName,
                                  senderphone = s.SenderPhoneNumber,
                                  DeliveryType = s.Deliverytype.name,
                                  TotalWeight = s.itemsWeight,
                                  senderAddress = s.senderAddress,
                                  specialnote = s.specialnote,
                                  DeliveryTime = s.DeliveryTime,
                                  totalTopay = s.totalTopay,
                                  vat = s.vat,
                                  Waybill = s.Waybill,
                                  DateCreated = s.DateCreated,
                                  departureLocation = d.locationName,
                                  destinationLocation = d.locationName,
                                  Released = collected,
                                  HasArrived = s.HasArrived

                              }).ToList();
                return ctdata;
            }
        }

        public class KonnectAPI
        {
            private readonly HttpClient _httpClient;
            Response resp;
            string baseurl;
            /// <summary>
            /// Initialize constructor with authkey and accountid parameters. 
            /// </summary>
            /// <param name="authKey"></param>
            /// <param name="authId"></param>
            public KonnectAPI(string authKey, string accountId)
            {
                baseurl = $"https://konnect.kirusa.com/api/v1/Accounts/{accountId}/";
                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(baseurl);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", authKey);

            }

            public async Task<bool> SendSMS(RequestSMS request)
            {
                try
                {
                    var uri = baseurl + "Messages";
                    var content = JsonConvert.SerializeObject(request, Formatting.None, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                    var buffer = Encoding.UTF8.GetBytes(content);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                    var response = await _httpClient.PostAsync(uri, byteContent);
                    var result = response.Content.ReadAsStringAsync().Result;
                    resp = JsonConvert.DeserializeObject<Response>(result);
                    if (resp.status == "ok")
                    {
                        return true;
                    }

                    return false;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString() + $"Error code returned: {resp.error_code} with reason {resp.error_reason}");
                }
                return false;
            }
        }

        public class Response
        {
            public string status { get; set; }
            public string error_code { get; set; }
            public string error_reason { get; set; }
        }

        public class RequestSMS
        {
            // Unique transaction id for the request
            public string id { get; set; }
            //The sender phone number. If not specified, the default sender id is used.
            public string from { get; set; }
            // The destination phone number, multiple phone numbers should be passed as a JSON array separated by comma.
            public List<string> to { get; set; }
            // The string to mask the sender.
            public string sender_mask { get; set; }
            // The full text of the message
            public string body { get; set; }

        }

        public string SendSms(string receipient, string msg)
        {
            string html = string.Empty;

            string url = @"http://www.ogosms.com/dynamicapi/?username=LibraMotors&password=Libra123%24&sender=LIBMOTLOGS&numbers=" + receipient + "&message=" + msg + "";

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                //using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        // Do stuff with response.GetResponseStream();
                        using (Stream stream = response.GetResponseStream())
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            html = reader.ReadToEnd();
                        }
                    }
                }
            }
            catch (WebException ex)
            {
                using (var stream = ex.Response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
                throw ex;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return html;

        }

        public string UpdateIsCollected(string waybillNum)
        {
            var s = (from sh in db.shipments where sh.Waybill == waybillNum select sh).FirstOrDefault();
            s.IsCollected = (int)ShipmentCollectionStatus.Collected;
            db.SubmitChanges();
            return s.Waybill;
        }

        public async Task<string> SendSmsb(string receipient, string msg)
        {
            string html = string.Empty;

            string url = @"http://www.ogosms.com/dynamicapi/?username=LibraMotors&password=Libra123%24&sender=LIBMOTLOGS&numbers=" + receipient + "&message=" + msg + "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            return await Task.FromResult(html);
        }

        public List<ShipmentModel> SeachShipmentByManifestNumber(string manifestnumber)
        {
            //bool collected = CheckcollectionStatus(manifestnumber);
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()
                            join sc in db.ShipmentCollections on l.Waybill equals sc.wayBillNumber
                            into scs
                            from sc in scs.DefaultIfEmpty()
                            where (manifest.ManifestNumber == manifestnumber || l.SenderPhoneNumber == manifestnumber || l.Waybill == manifestnumber)
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                deClearedValue = l.deClearedValue,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                HasArrived = l.HasArrived,
                                ReleasedBy = sc.releasedBy,
                                ReleasedDate = sc.DateCreated
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        public List<ShipmentModel> SeachShipmentByTerminal(int Terminal, DateTime fromdate, DateTime todate)
        {
            //bool collected = CheckcollectionStatus(manifestnumber);
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()
                            join sc in db.ShipmentCollections on l.Waybill equals sc.wayBillNumber
                            into scs
                            from sc in scs.DefaultIfEmpty()
                            where dpId.id == Terminal
                            && (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                deClearedValue = l.deClearedValue,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                HasArrived = l.HasArrived,
                                ReleasedBy = sc.releasedBy,
                                ReleasedDate = sc.DateCreated
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        public List<ShipmentModel> SeachShipmentByHubId(int hubId, DateTime fromdate, DateTime todate)
        {
            //bool collected = CheckcollectionStatus(manifestnumber);
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join hb in db.LocationHubs on dpId.id equals hb.LocationId
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id
                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()
                            join sc in db.ShipmentCollections on l.Waybill equals sc.wayBillNumber
                            into scs
                            from sc in scs.DefaultIfEmpty()
                            where hb.HubId == hubId
                            && (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                deClearedValue = l.deClearedValue,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                HasArrived = l.HasArrived,
                                ReleasedBy = sc.releasedBy,
                                ReleasedDate = sc.DateCreated

                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }
        public List<ShipmentModel> SearchShipmentReport(string manifestnumber, DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id

                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()

                            join sc in db.ShipmentCollections on l.Waybill equals sc.wayBillNumber
                            into scs
                            from sc in scs.DefaultIfEmpty()

                            where (manifest.ManifestNumber == manifestnumber || l.SenderPhoneNumber == manifestnumber || l.Waybill == manifestnumber) &&
                            (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                deClearedValue = l.deClearedValue,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                BookingRefCode = l.BookingRefCode,
                                ManifestNumber = manifest.ManifestNumber,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                HasArrived = l.HasArrived,
                                ReleasedBy = sc.releasedBy,
                                ReleasedDate = sc.DateCreated,
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx;
            }
        }



        //public string SearchShipmentReportS(string manifestnumber, DateTime fromdate, DateTime todate)
        //{
        //    using (var db = new liblogisticsDataContext())
        //    {
        //       // ShipmentModel items = new ShipmentModel();
        //        var items = (from l in db.shipments
        //                    join dpId in db.Locations on l.departureLocationId equals dpId.id
        //                    join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
        //                    join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
        //                    join arrId in db.Locations on l.destinationId equals arrId.id
        //                    join depstate in db.States on l.senderStateId equals depstate.id
        //                    join arrState in db.States on l.receiverStateId equals arrState.id

        //                    join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
        //                    into manimaps
        //                    from manimap in manimaps.DefaultIfEmpty()

        //                    join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
        //                    into manifests
        //                    from manifest in manifests.DefaultIfEmpty()

        //                    join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
        //                    into Vhcs
        //                    from Vhc in Vhcs.DefaultIfEmpty()

        //                    join sc in db.ShipmentCollections on l.Waybill equals sc.wayBillNumber
        //                    into scs
        //                    from sc in scs.DefaultIfEmpty()

        //                    where (manifest.ManifestNumber == manifestnumber || l.SenderPhoneNumber == manifestnumber || l.Waybill == manifestnumber) &&
        //                    (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
        //                    select new ShipmentModel
        //                    {
        //                        Id = l.Id,
        //                        Waybill = l.Waybill,
        //                        createdBy = l.createdBy,
        //                        CustomerType = ctType.CusType,
        //                        DeliveryTime = l.DeliveryTime,
        //                        DeliveryType = dlType.name,
        //                        departureLocation = dpId.locationName,
        //                        departureState = depstate.name,
        //                        destinationLocation = arrId.locationName,
        //                        destinationState = arrState.name,
        //                        GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
        //                        description = l.description,
        //                        TotalWeight = l.itemsWeight,
        //                        deClearedValue = l.deClearedValue,
        //                        vat = l.vat,
        //                        paymentMethod = l.paymentMethod,
        //                        receiverName = l.receiverName,
        //                        PaymentStatus = l.PaymentStatus,
        //                        senderAddress = l.senderAddress,
        //                        senderName = l.SenderName,
        //                        senderphone = l.SenderPhoneNumber,
        //                        receiverPhoneNumber = l.receiverPhoneNumber,
        //                        DateCreated = l.DateCreated,
        //                        //Items = item.ToModelList(),
        //                        GroupNumber = l.groupId,
        //                        BookingRefCode = l.BookingRefCode,
        //                        ManifestNumber = manifest.ManifestNumber,
        //                        VehicleRegNumber = Vhc.regNumber,
        //                        isCancelled = l.isCancelled,
        //                        IsRefund = l.IsRefund,
        //                        HasArrived = l.HasArrived,
        //                        ReleasedBy = sc.releasedBy,
        //                        ReleasedDate = sc.DateCreated,
        //                    });

        //        //).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
        //       return items.ReleasedBy;

        //    }
        //}

        public List<ShipmentModel> SeachAllMissingShipment(DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id

                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()

                            where (l.IsMissingDate >= fromdate.Date && l.IsMissingDate <= todate.AddDays(1)) && l.HasArrived == false && l.IsMissing == true
                            orderby l.IsMissingDate ascending
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                isReceived = manifest.IsReceived,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                IsMissing = l.IsMissing,
                                IsMissingStatus = l.IsMissingStatus,
                                IsMissingDate = l.IsMissingDate
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx.ToList();
            }
        }
        public List<ShipmentModel> SeachAllMissingShipment()
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id

                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()

                            where l.HasArrived == false && l.IsMissing == true
                            orderby l.IsMissingDate ascending
                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                isReceived = manifest.IsReceived,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                IsMissing = l.IsMissing,
                                IsMissingStatus = l.IsMissingStatus,
                                IsMissingDate = l.IsMissingDate
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx.ToList();
            }
        }

        public List<ShipmentModel> SearchAllTamperedShipment(DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id

                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()

                            where (l.IsTamperedDate >= fromdate.Date && l.IsTamperedDate <= todate.AddDays(1)) && l.HasArrived == false && l.IsTampered == true
                            orderby l.IsTamperedDate ascending
                            let tamperedStatus = l.IsTamperedStatus != null ? ((TamperedStatus)l.IsTamperedStatus.GetValueOrDefault()).ToString() : null

                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                isReceived = manifest.IsReceived,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                IsTampered = l.IsTampered,
                                IsTamperedDate = l.IsTamperedDate,
                                IsTamperedStatus = tamperedStatus
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx.ToList();
            }
        }

        public List<ShipmentModel> SearchAllTamperedShipment()
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dtcx = (from l in db.shipments
                            join dpId in db.Locations on l.departureLocationId equals dpId.id
                            join dlType in db.Deliverytypes on l.deliveryTypeId equals dlType.id
                            join ctType in db.CustomerTypes on l.typeofCustomer equals ctType.id
                            join arrId in db.Locations on l.destinationId equals arrId.id
                            join depstate in db.States on l.senderStateId equals depstate.id
                            join arrState in db.States on l.receiverStateId equals arrState.id

                            join manimap in db.ManifestMappings on l.groupId equals manimap.GroupWaybillNumber
                            into manimaps
                            from manimap in manimaps.DefaultIfEmpty()

                            join manifest in db.Manifests on manimap.ManifestId equals manifest.ManifestId
                            into manifests
                            from manifest in manifests.DefaultIfEmpty()

                            join Vhc in db.Vehicles on manifest.VehicleId equals Vhc.id
                            into Vhcs
                            from Vhc in Vhcs.DefaultIfEmpty()

                            where l.HasArrived == false && l.IsTampered == true
                            orderby l.IsTamperedDate ascending
                            let tamperedStatus = l.IsTamperedStatus != null ? ((TamperedStatus)l.IsTamperedStatus.GetValueOrDefault()).ToString() : null

                            select new ShipmentModel
                            {
                                Id = l.Id,
                                Waybill = l.Waybill,
                                createdBy = l.createdBy,
                                CustomerType = ctType.CusType,
                                DeliveryTime = l.DeliveryTime,
                                DeliveryType = dlType.name,
                                departureLocation = dpId.locationName,
                                departureState = depstate.name,
                                destinationLocation = arrId.locationName,
                                destinationState = arrState.name,
                                GrandTotalValue = ConvertAmount(Convert.ToDecimal(l.grandTotal)),
                                description = l.description,
                                TotalWeight = l.itemsWeight,
                                vat = l.vat,
                                paymentMethod = l.paymentMethod,
                                receiverName = l.receiverName,
                                receiverPhoneNumber = l.receiverPhoneNumber,
                                PaymentStatus = l.PaymentStatus,
                                senderAddress = l.senderAddress,
                                senderName = l.SenderName,
                                senderphone = l.SenderPhoneNumber,
                                DateCreated = l.DateCreated,
                                Items = item.ToModelList(),
                                GroupNumber = l.groupId,
                                ManifestNumber = manifest.ManifestNumber,
                                isReceived = manifest.IsReceived,
                                VehicleRegNumber = Vhc.regNumber,
                                isCancelled = l.isCancelled,
                                IsRefund = l.IsRefund,
                                IsTampered = l.IsTampered,
                                IsTamperedDate = l.IsTamperedDate,
                                IsTamperedStatus = tamperedStatus
                            }).GroupBy(t => t.Waybill).Select(grp => grp.FirstOrDefault()).OrderBy(x => x.DateCreated).ToList();
                return dtcx.ToList();
            }
        }

        public bool UpdateTamperedStatus(Shipment model)
        {
            try
            {

                var shipment = (from s in _context.Shipments where s.Waybill == model.Waybill select s).FirstOrDefault();
                if (shipment != null)
                {
                    shipment.IsTamperedStatus = model.IsTamperedStatus;
                }
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool UpdateWayBillStatus(string waybilnumber, string manifestnumber, string status, string currentuser, string groupNumber)
        {

                Shipment shpmt = new Shipment();
                try
                {
                    List<Shipment> shp = (from p in _context.Shipments
                                          where p.Waybill == waybilnumber
                                          select p).ToList();

                    List<Manifest> manifests = (from p in _context.Manifests
                                                where p.ManifestNumber == manifestnumber
                                                select p).ToList();

                    if (shp.Count() > 0)
                    {
                        foreach (Shipment l in shp)
                        {
                            if (status == "Found")
                            {
                                l.IsMissingStatus = status;
                                l.DateModified = DateTime.Now;
                                l.HasArrived = true;
                              //  db.SubmitChanges();
                                ////send sms
                                string msg = "You have a shipment to collect, kindly visit our terminal with this waybill number: " + l.Waybill + "  Call 09062547031 for more information ";

                                List<string> num = new List<string>();

                                num.Add(LibmotExpressConstants.KonnectAPIDetails.CountryCode + l.receiverPhoneNumber);

                                RequestSMS sms = new RequestSMS();
                                sms.id = Guid.NewGuid().ToString();
                                sms.to = num;
                                sms.sender_mask = LibmotExpressConstants.KonnectAPIDetails.SenderMask;
                                sms.body = msg;
                                KonnectAPI konnectAPI = new KonnectAPI(LibmotExpressConstants.KonnectAPIDetails.AuthKey, LibmotExpressConstants.KonnectAPIDetails.AccountId);
                                konnectAPI.SendSMS(sms);



                                //SendSms(l.receiverPhoneNumber, msg);
                                if (manifests.Count() > 0)
                                {
                                    var IsreceivedShipment = ValidateIsrecieveshipmentItem(groupNumber);
                                    if (IsreceivedShipment.Count.ToString() == "0")
                                    {
                                        foreach (Manifest p in manifests)
                                        {
                                            p.IsReceived = true;
                                            p.IsReceivedBy = currentuser;
                                            p.IsReceivedDate = DateTime.Now;
                                            p.DateModified = DateTime.Now;
                                        }
                                       // db.SubmitChanges();
                                        return true;
                                    }
                                }
                                else
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                l.IsMissingStatus = status;
                                l.DateModified = DateTime.Now;
                            }
                        }
                      //  db.SubmitChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
        }
        public int GetmanifestVehicleByManifestNumber(string manifestNumber)
        {

            using (var db = new liblogisticsDataContext())
            {
                var manifes = (from l in db.Manifests
                               where l.ManifestNumber == manifestNumber
                               select l.VehicleId).FirstOrDefault();

                return manifes.GetValueOrDefault();
            }
        }
        public int GenerateJourneyManagementId(string currentuser, string journeyCode)
        {

            using (var db = new liblogisticsDataContext())
            {
                JourneyManagement newJourney = new JourneyManagement();
                newJourney.ApprovedBy = currentuser;
                newJourney.CreatedBy = currentuser;
                newJourney.DateCreated = DateTime.Now;
                newJourney.JourneyDate = DateTime.Now;
                newJourney.JourneyStatus = JourneyStatus.Approved.ToString();
                newJourney.JourneyType = JourneyType.Loaded.ToString();
                newJourney.JourneyCode = journeyCode;
                db.JourneyManagements.InsertOnSubmit(newJourney);
                db.SubmitChanges();
                var journeymanagementid = db.JourneyManagements.Where(x => x.DateCreated == newJourney.DateCreated).FirstOrDefault();
                return journeymanagementid.Id;
            }
        }
        public string GenerateJourneyCode(string departure)
        {
            string Code = RandomDigits(5);
            string journeyCode = departure + "" + Code;
            return journeyCode;
        }
        public bool ApproveJourney(string manifestnumber, int journeymanagementId, string userLocation)
        {

            using (var db = new liblogisticsDataContext())
            {

                try
                {
                    List<Manifest> manifests = (from p in db.Manifests
                                                where p.ManifestNumber == manifestnumber
                                                select p).ToList();

                    var journeyCode = db.JourneyManagements.Where(j => j.Id == journeymanagementId).FirstOrDefault()?.JourneyCode;



                    foreach (Manifest manifest in manifests)
                    {
                        manifest.JourneyManagementId = journeymanagementId;
                        manifest.DateModified = DateTime.Now;
                        //UpdateShipmentTrackingToInTransitByManifest(manifest.ManifestNumber);
                    }
                    db.SubmitChanges();
                    MassUpdateWaybillStatus_(journeyCode, userLocation);
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }

                return true;
            }
        }
        public void UpdateShipmentTrackingToInTransitByManifest(string manifest)
        {
            #region update (on the ShipmentTracking table) all the shipments attached to this waybill with the status "In Transit"
            //First get all the shipments attached to this manifest
            var data = (from sh in db.shipments
                        join gm in db.GroupWaybillNumMappings on sh.groupId equals gm.GroupWaybillNumber
                        join mm in db.ManifestMappings on gm.GroupWaybillNumber equals mm.GroupWaybillNumber
                        join m in db.Manifests on mm.ManifestId equals m.ManifestId
                        where m.ManifestNumber == manifest
                        select sh);
            //update on the shipment tracking table all whose waybill correspond to the waybill on the data
            foreach (var d in data)
            {
                var trackingData = (from t in db.ShipmentTrackings where t.Waybill == d.Waybill select t).FirstOrDefault();
                var departureLocationId = db.Locations.Where(l => l.id == d.departureLocationId).FirstOrDefault()?.locationName;
                if (trackingData == null)
                {//create on the shipment tracking table if it does not exist (just to be defensive)
                    UpdateshipmentStatus(d.Waybill, departureLocationId, LibmotExpressConstants.ShipmentTrackingStatus.InTransit/*"In Transit"*/);
                    db.SubmitChanges();
                    //trackingData = (from t in db.ShipmentTrackings where t.Waybill == d.Waybill select t).FirstOrDefault();
                }
                //if (trackingData != null)
                else
                {
                    trackingData.Status = LibmotExpressConstants.ShipmentTrackingStatus.InTransit;// "In Transit";
                    trackingData.DateModified = DateTime.Now;
                    trackingData.Location = departureLocationId;
                }
            }
            db.SubmitChanges();
            #endregion
        }
        public void UpdateShipmentTrackingToAvailableForPickupByWaybill(string waybill)
        {
            #region update (on the ShipmentTracking table) all the shipments attached to this waybill with the status "Available for pickup"
            //update on the shipment tracking table all whose waybill correspond to the waybill given

            // join sp in db.ShipmentParcels
            //      on s.Waybill equals sp.Waybill
            // where s.Waybill == waybilnumber && sp.IsHomeDelivery == false
            // select s).ToList(
            var trackingData = (from t in db.ShipmentTrackings
                                join s in db.shipments
                                on t.Waybill equals s.Waybill
                                where t.Waybill == waybill && s.IsHomeDelivery != true
                                select t).FirstOrDefault();
            var destinationId = db.shipments.Where(s => s.Waybill == waybill).FirstOrDefault().destinationId;
            if (trackingData != null)
            {
                // trackingData.Status = LibmotExpressConstants.ShipmentTrackingStatus.AvailableForPickup;//"Available for pickup";
                //trackingData.DateModified = DateTime.Now;
                //trackingData.Location = db.Locations.Where(st => st.id == destinationId).FirstOrDefault().locationName;
                var locationName = db.Locations.Where(st => st.id == destinationId).FirstOrDefault().locationName;
                UpdateshipmentStatus(waybill, locationName, LibmotExpressConstants.ShipmentTrackingStatus.AvailableForPickup);
                //db.SubmitChanges();
            }
            //    if (trackingData == null)
            //{//create on the shipment tracking table if it does not exist (just to be defensive)

            //    UpdateshipmentStatus(waybill, db.Locations.Where(l => l.id == destinationId).FirstOrDefault().locationName, LibmotExpressConstants.ShipmentTrackingStatus.AvailableForPickup/*"Available for pickup"*/);
            //    //db.SubmitChanges();
            //}
            //else
            //{
            //    trackingData.Status = LibmotExpressConstants.ShipmentTrackingStatus.AvailableForPickup;//"Available for pickup";
            //    trackingData.DateModified = DateTime.Now;
            //    trackingData.Location = db.Locations.Where(st => st.id == destinationId).FirstOrDefault().locationName;
            //    db.SubmitChanges();
            //}
            #endregion
        }


        public void UpdateShipmentTrackingToAssignedToDispatchRider(string waybill)
        {
            #region update (on the ShipmentTracking table) all the shipments attached to this waybill with the status "Assigned To Dispatch Rider"

            var trackingData = (from t in db.ShipmentTrackings
                                join s in db.shipments
                                on t.Waybill equals s.Waybill
                                where t.Waybill == waybill && s.IsHomeDelivery == true
                                select t).FirstOrDefault();
            var destinationId = db.shipments.Where(s => s.Waybill == waybill).FirstOrDefault().destinationId;
            if (trackingData != null)
            {

                trackingData.Status = LibmotExpressConstants.ShipmentTrackingStatus.AssignedToDispatchRider;//"Assigned to Dispatch Rider";
                trackingData.DateModified = DateTime.Now;
                trackingData.Location = db.Locations.Where(st => st.id == destinationId).FirstOrDefault().locationName;
                db.SubmitChanges();

            }
            //if (trackingData == null)
            //{//create on the shipment tracking table if it does not exist (just to be defensive)

            //    //i will need to remove this code below because it still create another update of the same shipment cos of the condition equal == null
            //    UpdateshipmentStatus(waybill, db.Locations.Where(l => l.id == destinationId).FirstOrDefault().locationName, LibmotExpressConstants.ShipmentTrackingStatus.AvailableForPickup/*"Available for pickup"*/);
            //    //db.SubmitChanges();
            //}
            //else
            //{
            //    trackingData.Status = LibmotExpressConstants.ShipmentTrackingStatus.AssignedToDispatchRider;//"Available for pickup";
            //    trackingData.DateModified = DateTime.Now;
            //    trackingData.Location = db.Locations.Where(st => st.id == destinationId).FirstOrDefault().locationName;
            //    db.SubmitChanges();
            //}
            #endregion
        }
        public List<JourneyManagementModel> SeachAllApprovedManifest(DateTime fromdate, DateTime todate)
        {
            using (var db = new liblogisticsDataContext())
            {
                List<ShipmentItem> item = new List<ShipmentItem>();
                var dctx = from l in db.JourneyManagements
                           where (l.DateCreated >= fromdate.Date && l.DateCreated <= todate.AddDays(1))
                           orderby l.DateCreated descending
                           select new JourneyManagementModel
                           {
                               Id = l.Id,
                               TransloadedJourneyId = l.TransloadedJourneyId,
                               DateCreated = l.DateCreated,
                               JourneyDate = l.JourneyDate,
                               ApprovedBy = l.ApprovedBy,
                               JourneyStatus = l.JourneyStatus,
                               JourneyType = l.JourneyType,
                               JourneyCode = l.JourneyCode
                           };
                dctx = dctx.Distinct();
                return dctx.ToList().GroupBy(x => x.Id).Select(g => g.First()).OrderByDescending(x => x.DateCreated).ToList();
            }
        }
        public List<ManifestModel> GetManifestDetailsByJournId(int journManId)
        {

            //get journey type
            var journeyType = db.JourneyManagements.Where(x => x.Id == journManId).FirstOrDefault();
            if (journeyType.JourneyType == JourneyType.Transload.ToString())
            {
                using (var db = new liblogisticsDataContext())
                {
                    var dctx = from journ in db.JourneyManagements
                               join mn in db.Manifests on journ.Id equals mn.JourneyManagementId
                               join dpId in db.Locations on mn.DepartId equals dpId.id
                               join arrId in db.Locations on mn.DestinationId equals arrId.id
                               into arrivals
                               from arrid in arrivals.DefaultIfEmpty()
                               join Vhc in db.Vehicles on mn.TransloadedVehicle equals Vhc.id into Vhcs
                               from Vhc in Vhcs.DefaultIfEmpty()
                               join drv in db.Drivers on mn.TransloadedDriver equals drv.Id into drvs
                               from drv in drvs.DefaultIfEmpty()
                               where mn.TransloadedJourneyId == journManId
                               select new ManifestModel
                               {
                                   ManifestNumber = mn.ManifestNumber,
                                   DateCreated = mn.DateCreated,
                                   destination = arrid.locationName,
                                   departureLocation = dpId.locationName,
                                   vehicleNumber = Vhc.regNumber,
                                   driverName = drv.DriverName,
                                   VehicleId = mn.VehicleId,
                                   DriverId = mn.DriverId
                               };
                    return dctx.ToList();
                }
            }
            else
            {
                using (var db = new liblogisticsDataContext())
                {
                    var dctx = from journ in db.JourneyManagements
                               join mn in db.Manifests on journ.Id equals mn.JourneyManagementId
                               join dpId in db.Locations on mn.DepartId equals dpId.id
                               join arrId in db.Locations on mn.DestinationId equals arrId.id
                               into arrivals
                               from arrid in arrivals.DefaultIfEmpty()
                               join Vhc in db.Vehicles on mn.VehicleId equals Vhc.id into Vhcs
                               from Vhc in Vhcs.DefaultIfEmpty()
                               join drv in db.Drivers on mn.DriverId equals drv.Id into drvs
                               from drv in drvs.DefaultIfEmpty()
                               where mn.JourneyManagementId == journManId
                              || mn.TransloadedJourneyId == journManId
                               select new ManifestModel
                               {
                                   ManifestNumber = mn.ManifestNumber,
                                   DateCreated = mn.DateCreated,
                                   destination = arrid.locationName,
                                   departureLocation = dpId.locationName,
                                   vehicleNumber = Vhc.regNumber,
                                   driverName = drv.DriverName,
                                   VehicleId = mn.VehicleId,
                                   DriverId = mn.DriverId
                               };
                    return dctx.ToList();
                }
            }
        }
        public List<ManifestModel> GetManifestsByJourneyManId(int journManId)
        {

            //get journey type
            using (var db = new liblogisticsDataContext())
            {
                var dctx = from journ in db.JourneyManagements
                           join mn in db.Manifests on journ.Id equals mn.JourneyManagementId
                           join dpId in db.Locations on mn.DepartId equals dpId.id
                           join arrId in db.Locations on mn.DestinationId equals arrId.id
                           into arrivals
                           from arrid in arrivals.DefaultIfEmpty()
                           join Vhc in db.Vehicles on mn.VehicleId equals Vhc.id into Vhcs
                           from Vhc in Vhcs.DefaultIfEmpty()
                           join drv in db.Drivers on mn.DriverId equals drv.Id into drvs
                           from drv in drvs.DefaultIfEmpty()
                           where mn.JourneyManagementId == journManId
                           //|| mn.TransloadedJourneyId == journManId
                           select new ManifestModel
                           {
                               ManifestNumber = mn.ManifestNumber,
                               DateCreated = mn.DateCreated,
                               destination = arrid.locationName,
                               departureLocation = dpId.locationName,
                               vehicleNumber = Vhc.regNumber,
                               driverName = drv.DriverName,
                               VehicleId = mn.VehicleId,
                               DriverId = mn.DriverId
                           };
                return dctx.ToList();
            }
        }
        public bool Transload(int vehicle, int driver, int journId, string currentuser, string journeycode)
        {
            using (var db = new liblogisticsDataContext())
            {
                //generate new journey
                try
                {
                    JourneyManagement newJourney = new JourneyManagement();
                    newJourney.DateCreated = DateTime.Now;
                    newJourney.JourneyDate = DateTime.Now.Date;
                    newJourney.JourneyStatus = JourneyStatus.Approved.ToString();
                    newJourney.JourneyType = JourneyType.Rescue.ToString();
                    newJourney.ApprovedBy = currentuser;
                    newJourney.CreatedBy = currentuser;
                    newJourney.JourneyCode = journeycode;
                    db.JourneyManagements.InsertOnSubmit(newJourney);
                    db.SubmitChanges();

                    var journeymanagementid = db.JourneyManagements.Where(x => x.DateCreated == newJourney.DateCreated).FirstOrDefault();

                    //get manifest to attach journeymana
                    List<Manifest> manifests = (from p in db.Manifests
                                                where p.JourneyManagementId == journId
                                                select p).ToList();
                    foreach (Manifest p in manifests)
                    {
                        p.TransloadedJourneyId = p.JourneyManagementId;
                        p.JourneyManagementId = journeymanagementid.Id;
                        p.TransloadedVehicle = p.VehicleId;
                        p.TransloadedDriver = p.DriverId;
                        p.DriverId = driver;
                        p.VehicleId = vehicle;
                        p.DateModified = DateTime.Now;
                    }
                    //get the old journey
                    var oldj = db.JourneyManagements.Where(c => c.Id == journId).FirstOrDefault();
                    oldj.TransloadedJourneyId = journeymanagementid.Id;
                    oldj.TransloadedBy = currentuser;
                    oldj.TransloadedDate = DateTime.Now;
                    oldj.JourneyStatus = JourneyStatus.Transloaded.ToString();
                    oldj.JourneyType = JourneyType.Transload.ToString();
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return true;
            }
        }


        public bool TransloadTracking(int? vehicle, int? driver, int journId, string currentuser, string journeycode)
        {
            using (var db = new liblogisticsDataContext())
            {
                //generate new journey
                try
                {
                    JourneyManagement newJourney = new JourneyManagement();
                    newJourney.DateCreated = DateTime.Now;
                    newJourney.JourneyDate = DateTime.Now.Date;
                    newJourney.JourneyStatus = JourneyStatus.Approved.ToString();
                    newJourney.JourneyType = JourneyType.Rescue.ToString();
                    newJourney.ApprovedBy = currentuser;
                    newJourney.CreatedBy = currentuser;
                    newJourney.JourneyCode = journeycode;
                    db.JourneyManagements.InsertOnSubmit(newJourney);
                    db.SubmitChanges();

                    var journeymanagementid = db.JourneyManagements.Where(x => x.DateCreated == newJourney.DateCreated).FirstOrDefault();

                    //get manifest to attach journeymana
                    List<Manifest> manifests = (from p in db.Manifests
                                                where p.JourneyManagementId == journId
                                                select p).ToList();
                    foreach (Manifest p in manifests)
                    {
                        p.TransloadedJourneyId = p.JourneyManagementId;
                        p.JourneyManagementId = journeymanagementid.Id;
                        p.TransloadedVehicle = p.VehicleId;
                        p.TransloadedDriver = p.DriverId;
                        p.DriverId = driver;
                        p.VehicleId = vehicle;
                        p.DateModified = DateTime.Now;
                    }
                    //get the old journey
                    var oldj = db.JourneyManagements.Where(c => c.Id == journId).FirstOrDefault();
                    oldj.TransloadedJourneyId = journeymanagementid.Id;
                    oldj.TransloadedBy = currentuser;
                    oldj.TransloadedDate = DateTime.Now;
                    oldj.JourneyStatus = JourneyStatus.Transloaded.ToString();
                    oldj.JourneyType = JourneyType.Transload.ToString();
                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
                return true;
            }
        }

        public bool RevalidateApprovedManifest(int oldjournId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var man = db.Manifests.Where(x => x.JourneyManagementId == oldjournId);
                if (man.Count() > 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public bool TransloadManifest(int vehicle, int driver, int oldjournId, int newjournId, string manifestnumber, string currentuser)
        {
            using (var db = new liblogisticsDataContext())
            {
                try
                {
                    //get manifest to update 
                    var p = db.Manifests.Where(x => x.ManifestNumber == manifestnumber).FirstOrDefault();
                    p.TransloadedJourneyId = p.JourneyManagementId;
                    p.JourneyManagementId = newjournId;
                    p.TransloadedVehicle = p.VehicleId;
                    p.TransloadedDriver = p.DriverId;
                    p.DriverId = driver;
                    p.VehicleId = vehicle;
                    p.DateModified = DateTime.Now;
                    db.SubmitChanges();

                    // get new journey to update status to rescue

                    var newj = db.JourneyManagements.Where(c => c.Id == newjournId).FirstOrDefault();
                    newj.JourneyType = JourneyType.Rescue.ToString();
                    db.SubmitChanges();
                    //get the old journey
                    var oldj = db.JourneyManagements.Where(c => c.Id == oldjournId).FirstOrDefault();
                    oldj.TransloadedJourneyId = newjournId;
                    oldj.TransloadedBy = currentuser;
                    oldj.TransloadedDate = DateTime.Now;
                    db.SubmitChanges();

                    //revalidatejourney
                    bool result = RevalidateApprovedManifest(oldjournId);
                    if (result == true)
                    {
                        oldj.JourneyStatus = JourneyStatus.Transloaded.ToString();
                        oldj.JourneyType = JourneyType.Transload.ToString();
                        db.SubmitChanges();
                    }
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                }
            }
            return true;
        }
        public List<ManifestModel> GetUnApprovedManifestByHubId(int hubId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.Manifests
                           join hb in db.LocationHubs on l.DepartId equals hb.LocationId
                           join dp in db.Locations on hb.LocationId equals dp.id
                           join dl in db.Locations on l.DestinationId equals dl.id into destination
                           from dl in destination.DefaultIfEmpty()
                           join dn in db.Drivers on l.DriverId equals dn.Id
                           join vh in db.Vehicles on l.VehicleId equals vh.id
                           let dr = db.Drivers.Where(w => w.Id == l.DriverId)
                           let vd = db.Vehicles.Where(n => n.id == l.VehicleId)
                           where hb.HubId == hubId && (l.DateCreated >= fromDate && l.DateCreated <= toDate.AddDays(1))
                           && l.JourneyManagementId == null

                           select new ManifestModel
                           {
                               DateCreated = l.DateCreated,
                               departureLocation = dp.locationName,
                               dispatchedBy = l.DispatchedById,
                               IsDispatched = l.IsDispatched,
                               IsReceived = l.IsReceived,
                               ManifestId = l.ManifestId,
                               ManifestNumber = l.ManifestNumber,
                               IsReceivedBy = l.ReceiverById,
                               driverphone = dn.DriverPhone,
                               DriverInfo = dr.Any() ? dr.ToList() : new List<Driver>(),
                               VehicleInfo = vd.Any() ? vd.ToList() : new List<Vehicle>(),
                               DispatchFee = l.DispatchFee,
                               driverName = dn.DriverName,
                               vehicleNumber = vh.regNumber,
                               destination = GetlocationsbyID(l.DestinationId.GetValueOrDefault())
                           }).OrderByDescending(o => o.DateCreated).GroupBy(i => i.ManifestId).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }
        public List<ManifestModel> GetUnApprovedManifestByDepttId(int departureId, DateTime fromDate, DateTime toDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from l in db.Manifests
                           join dp in db.Locations on l.DepartId equals dp.id
                           join dl in db.Locations on l.DestinationId equals dl.id into destination
                           from dl in destination.DefaultIfEmpty()
                           join dn in db.Drivers on l.DriverId equals dn.Id
                           join vh in db.Vehicles on l.VehicleId equals vh.id
                           let dr = db.Drivers.Where(w => w.Id == l.DriverId)
                           let vd = db.Vehicles.Where(n => n.id == l.VehicleId)
                           where l.DepartId == departureId && (l.DateCreated >= fromDate && l.DateCreated <= toDate.AddDays(1))
                           && l.JourneyManagementId == null
                           select new ManifestModel
                           {
                               DateCreated = l.DateCreated,
                               departureLocation = dp.locationName,
                               dispatchedBy = l.DispatchedById,
                               IsDispatched = l.IsDispatched,
                               IsReceived = l.IsReceived,
                               ManifestId = l.ManifestId,
                               ManifestNumber = l.ManifestNumber,
                               IsReceivedBy = l.ReceiverById,
                               driverphone = dn.DriverPhone,
                               DriverInfo = dr.Any() ? dr.ToList() : new List<Driver>(),
                               VehicleInfo = vd.Any() ? vd.ToList() : new List<Vehicle>(),
                               DispatchFee = l.DispatchFee,
                               driverName = dn.DriverName,
                               vehicleNumber = vh.regNumber,
                               destination = GetlocationsbyID(l.DestinationId.GetValueOrDefault())
                           }).OrderByDescending(o => o.DateCreated).GroupBy(i => i.ManifestId).Select(group => group.FirstOrDefault());
                return dtx.ToList();
            }
        }
        public string UserActivationCode(string userid)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from lib in db.AccountsConfirmations
                           where lib.UserId == userid
                           select lib.ActivationCode);
                return dtx.FirstOrDefault();
            }
        }
        public bool UpdateAccountConfirm(string userId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var dtx = (from lib in db.AccountsConfirmations
                           where lib.UserId == userId
                           select lib).ToList();
                if (dtx.Count() > 0)
                {
                    foreach (var p in dtx)
                    {
                        p.IsConfirmed = true;
                        //p.ActivationCode = null;
                        db.SubmitChanges();
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }
        public bool CheckIfWalletNumberExists(string walletNumber)
        {
            using (var db = new liblogisticsDataContext())
            {
                var check = (from dbct in db.WalletNumbers
                             where dbct.WalletPan == walletNumber
                             select dbct.Id).ToList();
                if (check.Count > 0)
                    return true;
                else
                {
                    return false;
                }
            }
        }

        public WalletNumber GetLastValidWalletNumber()
        {
            using (var db = new liblogisticsDataContext())
            {
                var wallets = from dbct in db.WalletNumbers
                              orderby dbct.WalletPan descending
                              select dbct;
                return wallets.FirstOrDefault();
            }
        }
        public WalletNumber GenerateNextValidWalletNumber()
        {
            using (var db = new liblogisticsDataContext())
            {
                var walletNumber = GetLastValidWalletNumber();

                var walletPan = walletNumber?.WalletPan ?? "0";

                var number = long.Parse(walletPan) + 1;
                var numberStr = number.ToString("0000000000");

                return new WalletNumber
                {
                    WalletPan = numberStr,
                    IsActive = true
                };
            }
        }
        public WalletModel GetWalletDetails(string userId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var wallet = (from dtcx in db.Wallets
                              where dtcx.IsDeleted == false && dtcx.IsReset == false && dtcx.UserId == userId
                              select new WalletModel
                              {
                                  Id = dtcx.Id,
                                  Balance = dtcx.Balance,
                                  UserId = dtcx.UserId,
                                  UserType = dtcx.UserType,
                                  WalletNumber = dtcx.WalletNumber,
                                  IsDeleted = dtcx.IsDeleted
                              }).FirstOrDefault();
                //if wallet does not exist, create it!
                if (wallet == null)
                {
                    var ut = GetUserType(userId);
                    CreateWallet(userId, ut?.ToString());
                    GetWalletDetails(userId);
                }
                return wallet;
            }
        }
        public List<WalletTransactionDTO> GetWalletTransactions(string userId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var user = (from dcx in db.WalletTransactions
                            join wt in db.Wallets on dcx.WalletId equals wt.Id
                            where wt.UserId == userId && wt.IsDeleted == false && wt.IsReset == false
                            select new WalletTransactionDTO
                            {
                                Id = dcx.Id,
                                UserId = dcx.UserId,
                                TransactionDate = dcx.TransactionDate,
                                TransactionAmount = dcx.TransactionAmount,
                                TransactedBy = dcx.TransactedBy,
                                WalletId = dcx.WalletId,
                                TransType = dcx.TransactionType,
                                TransactionDescription = dcx.TransactionDescription,
                                LineBalance = dcx.LineBalance,
                                IsCompleted = dcx.IsCompleted
                            });

                return user.ToList();
            }
        }

        public List<WalletTransactionDTO> GetWalletTransactionsCrd(string userId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var user = (from dcx in db.WalletTransactions
                            join wt in db.Wallets on dcx.WalletId equals wt.Id
                            where wt.UserId == userId && wt.IsDeleted == false && wt.IsReset == false && dcx.TransactionType == "Credit"
                            select new WalletTransactionDTO
                            {
                                Id = dcx.Id,
                                UserId = dcx.UserId,
                                TransactionDate = dcx.TransactionDate,
                                TransactionAmount = dcx.TransactionAmount,
                                TransactedBy = dcx.TransactedBy,
                                WalletId = dcx.WalletId,
                                TransType = dcx.TransactionType,
                                TransactionDescription = dcx.TransactionDescription,
                                LineBalance = dcx.LineBalance,
                                IsCompleted = dcx.IsCompleted
                            });

                return user.ToList();
            }
        }


        public List<WalletTransactionDTO> GetWalletTransactionsDeb(string userId)
        {
            using (var db = new liblogisticsDataContext())
            {
                var user = (from dcx in db.WalletTransactions
                            join wt in db.Wallets on dcx.WalletId equals wt.Id
                            where wt.UserId == userId && wt.IsDeleted == false && wt.IsReset == false && dcx.TransactionType == "Debit"
                            select new WalletTransactionDTO
                            {
                                Id = dcx.Id,
                                UserId = dcx.UserId,
                                TransactionDate = dcx.TransactionDate,
                                TransactionAmount = dcx.TransactionAmount,
                                TransactedBy = dcx.TransactedBy,
                                WalletId = dcx.WalletId,
                                TransType = dcx.TransactionType,
                                TransactionDescription = dcx.TransactionDescription,
                                LineBalance = dcx.LineBalance,
                                IsCompleted = dcx.IsCompleted
                            });

                return user.ToList();
            }
        }
        public bool ValidateWallet(string userId, decimal amount)
        {
            var wallet = GetWalletDetails(userId);
            bool result;
            result = (wallet?.Balance >= amount && wallet?.IsDeleted == false);
            return result;
        }
        public WalletTransactionDTO GenerateWalletTransactionReference(TransactionType transactionType, string userId, string usernameoremail, decimal TransactionAmount)
        {
            using (var db = new liblogisticsDataContext())
            {
                var wallet = GetWalletDetails(userId);
                var refnum = RandomDigits(8);
                var referenceNumber = refnum + "WF";

                if (transactionType == TransactionType.Debit)
                    throw new Exception("Use WayBill Number as Reference");
                //if it is credit
                var insert = new WalletTransaction
                {
                    Id = GenerateComb(),
                    CreatedBy = usernameoremail,
                    CreationTime = DateTime.Now,
                    IsDeleted = false,
                    LineBalance = wallet.Balance + TransactionAmount,
                    WalletId = wallet.Id,
                    UserId = userId,
                    TransactionType = transactionType.ToString(),
                    TransactionDate = DateTime.Now,
                    TransactionAmount = TransactionAmount,
                    TransactedBy = usernameoremail,
                    TransactionDescription = "Payment Into Wallet",
                    IsCompleted = false,
                    Reference = referenceNumber
                };
                db.WalletTransactions.InsertOnSubmit(insert);
                db.SubmitChanges();
                var response = new WalletTransactionDTO
                {
                    Id = insert.Id,
                    IsCompleted = insert.IsCompleted,
                    LineBalance = insert.LineBalance,
                    TransactionDate = insert.TransactionDate,
                    TransactionAmount = insert.TransactionAmount,
                    WalletId = insert.WalletId,
                    Reference = insert.Reference,
                    TransactedBy = usernameoremail,
                    UserId = userId,
                    TransactionType = transactionType,
                    TransType = transactionType.ToString(),
                    TransactionDescription = insert.TransactionDescription
                };
                return response;
            }
        }
        public List<returnmsg> CancelWalletTransaction(Guid Id)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    List<WalletTransaction> mnp = (from p in db.WalletTransactions
                                                   where p.Id == Id
                                                   select p).ToList();
                    if (mnp.Count > 0)
                    {
                        foreach (WalletTransaction l in mnp)
                        {
                            l.IsCompleted = false;
                            l.IsDeleted = true;
                            l.LastModificationTime = DateTime.Now;
                        }
                        db.SubmitChanges();
                        rtmsg.completed = true;
                        rtmsg.code = "Success";
                        rtmsg.successmsg = "Wallet Transaction Cancelled";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                    else
                    {
                        rtmsg.completed = false;
                        rtmsg.code = "Error";
                        rtmsg.successmsg = "Unable to cancel Wallet Transaction";
                        retmsgs.Add(rtmsg);
                        return retmsgs;
                    }
                }
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex.InnerException.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }
        public bool VerifyWalletTransaction(string Reference)
        {
            using (var db = new liblogisticsDataContext())
            {
                var check = (from p in db.WalletTransactions
                             where p.Reference == Reference && p.IsCompleted == true
                             select p).ToList();

                return check is null ? false : true;
            }
        }
        public bool UpdateWalletTransaction(WalletTransactionDTO WalletTransData)
        {
            using (var db = new liblogisticsDataContext())
            {
                try
                {
                    if (WalletTransData == null) { return false; }
                    var result = (from p in db.WalletTransactions
                                  where p.Reference == WalletTransData.Reference
                                  select p).FirstOrDefault();

                    if (result == null)
                        return false;
                    result.LastModificationTime = DateTime.Now;
                    result.IsCompleted = true;
                    result.PayStackResponse = WalletTransData.PayStackResponse;
                    result.paymentMethod = WalletTransData.paymentMethod;
                    result.TransactionType = WalletTransData.TransactionType.ToString();
                    result.TransactionAmount = WalletTransData.TransactionAmount;
                    result.Reference = WalletTransData.Reference;
                    result.PayStackReference = WalletTransData.PayStackReference;
                    result.LastModificationTime = DateTime.Now;

                    db.SubmitChanges();
                    return true;
                }
                catch (Exception ex)
                {
                    ex.Message.ToString();
                    return false;
                }
            }
        }
        public PaymentResponseDTO FundWallet(string userId, string usernameoremail, decimal TransactionAmount)
        {
            using (var db = new liblogisticsDataContext())
            {
                var wallet = GetWalletDetails(userId);
                if (wallet != null)
                {
                    var walet = db.Wallets.Where(c => c.UserId == userId).FirstOrDefault();
                    var newBalance = walet.Balance + TransactionAmount;
                    walet.Balance = newBalance;
                    db.SubmitChanges();
                    var response = new PaymentResponseDTO
                    {
                        Status = true,
                        CreatedBy = usernameoremail,
                        CreationDate = DateTime.Now,
                        TransactionAmount = TransactionAmount,
                        TransactionType = TransactionType.Credit,
                        WalletBalance = newBalance,
                        Response = "Transaction Successful"
                    };
                    #region to conform with requirement, add a point for the user transaction discount if the amount is more than #5,000 (hardcode alert)
                    //add transaction amount to discount
                    var transactionPoint = (int)(TransactionAmount / 5000);
                    var user = (from a in db.AspNetUsers where a.Id == userId select a).FirstOrDefault();
                    if (user != null)
                    {
                        user.TransactionalDiscount += transactionPoint;
                        db.SubmitChanges();
                    }
                    #endregion
                    return response;
                }
                return null;
            }
        }
        public PaymentResponseDTO PayFromWallet(string userId, string usernameoremail, decimal TransactionAmount, string Reference)
        {
            using (var db = new liblogisticsDataContext())
            {
                //Get Customer Wallet details

                var details = GetWalletDetails(userId);
                //validate 
                var validate = ValidateWallet(userId, TransactionAmount);
                if (validate)
                {
                    var walet = db.Wallets.Where(c => c.UserId == userId).FirstOrDefault();
                    var newBalance = walet.Balance - TransactionAmount;
                    walet.Balance = newBalance;
                    db.SubmitChanges();

                    //insert wallet transaction
                    var walletTrans = new WalletTransaction
                    {
                        Id = GenerateComb(),
                        CreatedBy = usernameoremail,
                        CreationTime = DateTime.Now,
                        IsDeleted = false,
                        LineBalance = newBalance,
                        WalletId = walet.Id,
                        UserId = userId,
                        TransactionType = TransactionType.Debit.ToString(),
                        TransactionDate = DateTime.Now,
                        TransactionAmount = TransactionAmount,
                        TransactedBy = usernameoremail,
                        TransactionDescription = "Payment Using Wallet",
                        Reference = Reference,
                        IsCompleted = true
                    };

                    db.WalletTransactions.InsertOnSubmit(walletTrans);
                    db.SubmitChanges();
                    var response = new PaymentResponseDTO
                    {
                        Status = true,
                        CreatedBy = usernameoremail,
                        CreationDate = DateTime.Now,
                        TransactionAmount = TransactionAmount,
                        TransactionType = TransactionType.Debit,
                        WalletBalance = newBalance,
                        Response = "Transaction Successful"
                    };
                    var sh = (from s in db.shipments where s.Waybill == Reference select s).FirstOrDefault();
                    if (sh != null)
                    {
                        sh.PaymentStatus = (int)PaymentStatus.Paid;
                        sh.paymentMethod = PaymentMethod.EWallet.ToString();
                        db.SubmitChanges();
                    }
                    return response;
                }
                else
                {
                    var response = new PaymentResponseDTO
                    {
                        Status = false,
                        Response = "Insufficient Funds. Invalid Transaction",
                        WalletBalance = details.Balance
                    };
                    return response;
                }
            }
        }
        public async Task<ShipmentResponseDTO> ProcessPaystackPayment(string WayBillOrReference, TransactionType transactionType)
        {
            using (var db = new liblogisticsDataContext())
            {
                var paystackSecret = System.Configuration.ConfigurationManager.AppSettings["PayStackSecret"];
                if (transactionType == TransactionType.Debit)
                {
                    var WaybillToUpdate = db.shipments.Where(c => c.Waybill == WayBillOrReference && c.isCancelled == false && c.isDeleted == false).FirstOrDefault();

                    if (WaybillToUpdate == null)
                    {
                        return new ShipmentResponseDTO() { Response = "WayBill Not Found" };
                    }
                    if (WaybillToUpdate.PaymentStatus != (int)PaymentStatus.Pending)
                    {
                        ShipmentResponseDTO notPendingWaybillResponseData = new ShipmentResponseDTO
                        {
                            Response = ((PaymentStatus)WaybillToUpdate.PaymentStatus).ToString(),
                            Amount = WaybillToUpdate.totalTopay,
                            WayBill = WaybillToUpdate.Waybill,
                            DateCreated = WaybillToUpdate.DateCreated.ToString(),
                            departureLocation = WaybillToUpdate.departureLocationId?.ToString(),
                            destinationLocation = WaybillToUpdate.destinationId.ToString()
                        };
                        return notPendingWaybillResponseData;
                    }
                    var transactionReference = WayBillOrReference;

                    PaystackVerifyResponseDto paystackVerifyResponseDto = null;
                    try
                    {
                        #region newPaystackVerifyImplementation
                        var client = new RestClient("https://api.paystack.co/transaction/verify/" + transactionReference);
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("Authorization", "Bearer " + paystackSecret);
                        IRestResponse response = await client.ExecuteTaskAsync(request);
                        var payResponse = response.Content;
                        paystackVerifyResponseDto = JsonConvert.DeserializeObject<PaystackVerifyResponseDto>(response.Content);
                        #endregion
                    }
                    catch (Exception)
                    {
                    }
                    var responseData = paystackVerifyResponseDto?.data;

                    //if not found of paystack
                    if (WaybillToUpdate.paymentMethod == PaymentMethod.PayStack.ToString() && responseData == null)
                    {
                        return new ShipmentResponseDTO
                        {
                            Response = "Transaction Not Found On PayStack",
                            Amount = 0,
                            DateCreated = WaybillToUpdate.DateCreated.ToString(),
                            WayBill = WaybillToUpdate.Waybill
                        };
                    }

                    //if not found (if the response data is null)
                    if (responseData == null)
                    {
                        return new ShipmentResponseDTO
                        {
                            Response = "Transaction Not Found",
                            Amount = 0,
                            DateCreated = WaybillToUpdate.DateCreated.ToString(),
                            WayBill = WaybillToUpdate.Waybill
                        };
                    }

                    var authorization = responseData.authorization;

                    //if authorization is null
                    if (authorization == null)
                    {
                        return new ShipmentResponseDTO
                        {
                            Response = "Transaction Authorization Not Found",
                            Amount = 0,
                            DateCreated = WaybillToUpdate.DateCreated.ToString(),
                            WayBill = WaybillToUpdate.Waybill
                        };
                    }

                    var paystackPaymentResponse = new PayStackPaymentResponse
                    {
                        Reference = responseData.reference,
                        ApprovedAmount = responseData.amount / 100,
                        AuthorizationCode = authorization.authorization_code,
                        CardType = authorization.card_type,
                        Last4 = authorization.last4,
                        Reusable = authorization.reusable,
                        Bank = authorization.bank,
                        ExpireMonth = authorization.exp_month,
                        ExpireYear = authorization.exp_year,
                        TransactionDate = responseData.transaction_date.GetValueOrDefault(),
                        Channel = responseData.channel,
                        Status = responseData.status
                    };

                    if (paystackVerifyResponseDto.data.status.ToLower() != "success")
                    {
                        var response = await PaystackWebHookRefAsync(WayBillOrReference);

                        if (response != null)
                        {
                            paystackPaymentResponse = new PayStackPaymentResponse
                            {
                                Reference = response.Reference,
                                ApprovedAmount = response.ApprovedAmount / 100,
                                AuthorizationCode = response.AuthorizationCode,
                                CardType = response.CardType,
                                Last4 = response.Last4,
                                Reusable = response.Reusable,
                                Bank = response.Bank,
                                ExpireMonth = response.ExpireMonth,
                                ExpireYear = response.ExpireYear,
                                TransactionDate = response.TransactionDate,
                                Channel = response.Channel,
                                Status = response.Status
                            };
                        }
                    }
                    WaybillToUpdate.PayStackPaymentResponse = paystackPaymentResponse;

                    if (paystackPaymentResponse.Status?.ToLower() == "success")
                    {
                        WaybillToUpdate.PaymentStatus = Convert.ToInt32(PaymentStatus.Paid);
                    }
                    else if (paystackPaymentResponse.Status?.ToLower() == "pending")
                    {
                        WaybillToUpdate.PaymentStatus = Convert.ToInt32(PaymentStatus.Pending);
                    }
                    else
                    {//failed
                        WaybillToUpdate.PaymentStatus = Convert.ToInt32(PaymentStatus.Unsuccessful);
                    }
                    WaybillToUpdate.paymentMethod = PaymentMethod.PayStack.ToString();

                    WaybillToUpdate.PayStackResponse = responseData.status;

                    db.SubmitChanges();

                    ShipmentModel wayBillData = new ShipmentModel
                    {
                        receiverName = WaybillToUpdate.receiverName,
                        receiverPhoneNumber = WaybillToUpdate.receiverPhoneNumber,
                        Waybill = WaybillToUpdate.Waybill,
                        PaymentStatus = WaybillToUpdate.PaymentStatus,
                        paymentMethod = WaybillToUpdate.paymentMethod,
                        PayStackPaymentResponse = WaybillToUpdate.PayStackPaymentResponse,
                        PayStackReference = WaybillToUpdate.PayStackReference,
                        PayStackResponse = WaybillToUpdate.PayStackResponse,
                        createdBy = WaybillToUpdate.createdBy
                    };

                    if (paystackPaymentResponse.Status?.ToLower() == "ongoing")
                    {
                        ShipmentResponseDTO OngoingShipmentResponseData = new ShipmentResponseDTO
                        {
                            Response = WaybillToUpdate.PaymentStatus.ToString(),
                            Amount = WaybillToUpdate.totalTopay,
                            WayBill = WaybillToUpdate.Waybill,
                            DateCreated = WaybillToUpdate.DateCreated.ToString(),
                            departureLocation = WaybillToUpdate.departureLocationId.ToString(),
                            destinationLocation = WaybillToUpdate.destinationId.ToString()
                        };
                        return OngoingShipmentResponseData;
                    }
                    try
                    {
                        UpdateOnlineShipment(wayBillData);
                    }
                    catch (Exception)
                    {
                        ShipmentResponseDTO ShipmentResponseData2 = new ShipmentResponseDTO
                        {
                            Response = ((PaymentStatus)WaybillToUpdate.PaymentStatus).ToString(),
                            Amount = paystackVerifyResponseDto.data.amount / 100,
                            WayBill = WaybillToUpdate.Waybill,
                            DateCreated = WaybillToUpdate.DateCreated.ToString(),
                            departureLocation = WaybillToUpdate.departureLocationId.ToString(),
                            destinationLocation = WaybillToUpdate.destinationId.ToString()
                        };
                        return ShipmentResponseData2;
                    }

                    ShipmentResponseDTO ShipmentResponseData = new ShipmentResponseDTO
                    {

                        Response = ((PaymentStatus)WaybillToUpdate.PaymentStatus).ToString(),
                        Amount = paystackVerifyResponseDto?.data?.amount / 100,
                        WayBill = WaybillToUpdate.Waybill,
                        departureLocation = WaybillToUpdate.departureLocationId?.ToString(),
                        destinationLocation = WaybillToUpdate.destinationId.ToString(),
                        DateCreated = WaybillToUpdate.DateCreated.ToString(),
                    };
                    return ShipmentResponseData;
                }
                else if (transactionType == TransactionType.Credit)
                {
                    //check if wallet Transaction Exists
                    var WalletTrans = db.WalletTransactions.Where(c => c.Reference == WayBillOrReference && c.IsDeleted == false).FirstOrDefault();

                    if (WalletTrans == null)
                    {
                        return new ShipmentResponseDTO() { Response = "Reference Number Not Found" };
                    }
                    if (WalletTrans.IsCompleted != false)
                    {
                        ShipmentResponseDTO notPendingWaybillResponseData = new ShipmentResponseDTO
                        {
                            Response = WalletTrans.TransactionDescription,
                            Amount = WalletTrans.TransactionAmount,
                            WalletTransactionReference = WalletTrans.Reference,
                            DateCreated = WalletTrans.CreationTime?.ToString()
                        };
                        return notPendingWaybillResponseData;
                    }
                    var transactionReference = WayBillOrReference;

                    PaystackVerifyResponseDto paystackVerifyResponseDto = null;
                    try
                    {

                        #region newPaystackVerifyImplementation
                        var client = new RestClient("https://api.paystack.co/transaction/verify/" + transactionReference);
                        var request = new RestRequest(Method.GET);
                        request.AddHeader("content-type", "application/json");
                        request.AddHeader("Authorization", "Bearer " + paystackSecret);
                        IRestResponse response = await client.ExecuteTaskAsync(request);
                        var payResponse = response.Content;
                        paystackVerifyResponseDto = JsonConvert.DeserializeObject<PaystackVerifyResponseDto>(response.Content);
                        #endregion
                    }
                    catch (Exception)
                    {
                        //do nothing
                    }

                    var responseData = paystackVerifyResponseDto?.data;

                    //if not found of paystack
                    if (responseData == null)
                    {
                        return new ShipmentResponseDTO
                        {
                            Response = "Transaction Not Found On PayStack",
                            Amount = 0,
                            DateCreated = WalletTrans.TransactionDate.ToString(),
                            WalletTransactionReference = WalletTrans.Reference
                        };
                    }
                    var authorization = responseData.authorization;

                    //if authorization not found of paystack
                    if (authorization == null)
                    {
                        return new ShipmentResponseDTO
                        {
                            Response = "Transaction Authorization Not Found On PayStack",
                            Amount = 0,
                            DateCreated = WalletTrans.TransactionDate.ToString(),
                            WalletTransactionReference = WalletTrans.Reference
                        };
                    }

                    var paystackPaymentResponse = new PayStackPaymentResponse
                    {
                        Reference = responseData.reference,
                        ApprovedAmount = responseData.amount / 100,
                        AuthorizationCode = authorization.authorization_code,
                        CardType = authorization.card_type,
                        Last4 = authorization.last4,
                        Reusable = authorization.reusable,
                        Bank = authorization.bank,
                        ExpireMonth = authorization.exp_month,
                        ExpireYear = authorization.exp_year,
                        TransactionDate = responseData.transaction_date.GetValueOrDefault(),
                        Channel = responseData.channel,
                        Status = responseData.status
                    };

                    if (paystackVerifyResponseDto?.data?.status?.ToLower() != "success")
                    {
                        var response = await PaystackWebHookRefAsync(WayBillOrReference);

                        if (response != null)
                        {
                            paystackPaymentResponse = new PayStackPaymentResponse
                            {
                                Reference = response.Reference,
                                ApprovedAmount = response.ApprovedAmount / 100,
                                AuthorizationCode = response.AuthorizationCode,
                                CardType = response.CardType,
                                Last4 = response.Last4,
                                Reusable = response.Reusable,
                                Bank = response.Bank,
                                ExpireMonth = response.ExpireMonth,
                                ExpireYear = response.ExpireYear,
                                TransactionDate = response.TransactionDate,
                                Channel = response.Channel,
                                Status = response.Status
                            };
                        }
                    }

                    WalletTrans.PayStackPaymentResponse = paystackPaymentResponse;

                    var payStackSuccess = paystackPaymentResponse?.Status?.ToLower() == "success";

                    WalletTrans.IsCompleted = payStackSuccess ? true : false;

                    WalletTrans.paymentMethod = PaymentMethod.PayStack.ToString();

                    WalletTrans.PayStackResponse = responseData.status;

                    db.SubmitChanges();

                    WalletTransactionDTO WalletTransData = new WalletTransactionDTO
                    {
                        IsCompleted = WalletTrans.IsCompleted,
                        TransactionAmount = WalletTrans.TransactionAmount,
                        Reference = WalletTrans.Reference,
                        TransactedBy = WalletTrans.Reference,
                        TransType = WalletTrans.TransactionType,
                        TransactionDate = WalletTrans.TransactionDate,
                        TransactionDescription = WalletTrans.TransactionDescription,
                        WalletId = WalletTrans.WalletId,
                        LineBalance = WalletTrans.LineBalance,
                        paymentMethod = WalletTrans.paymentMethod,
                        PayStackReference = WalletTrans.PayStackReference,
                        PayStackResponse = WalletTrans.PayStackResponse,
                        PayStackWebhookReference = WalletTrans.PayStackWebhookReference,
                        TransactionType = transactionType
                    };

                    if (paystackPaymentResponse?.Status?.ToLower() == "ongoing")
                    {
                        ShipmentResponseDTO OngoingShipmentResponseData = new ShipmentResponseDTO
                        {
                            Response = WalletTrans.IsCompleted.ToString(),
                            Amount = WalletTrans.TransactionAmount,
                            WalletTransactionReference = WalletTrans.Reference,
                            DateCreated = WalletTrans.TransactionDate.ToString(),
                            PaymentResponse = WalletTrans.PayStackResponse
                        };
                        return OngoingShipmentResponseData;
                    }
                    try
                    {
                        UpdateWalletTransaction(WalletTransData);
                    }
                    catch (Exception)
                    {
                        ShipmentResponseDTO ShipmentResponseData2 = new ShipmentResponseDTO
                        {
                            Response = WalletTrans.IsCompleted.ToString(),
                            Amount = WalletTrans.TransactionAmount,
                            WalletTransactionReference = WalletTrans.Reference,
                            DateCreated = WalletTrans.TransactionDate.ToString(),
                            PaymentResponse = WalletTrans.PayStackResponse
                        };
                        return ShipmentResponseData2;
                    }
                    ShipmentResponseDTO ShipmentResponseData = new ShipmentResponseDTO
                    {

                        Response = WalletTrans.IsCompleted.ToString(),
                        Amount = WalletTrans.TransactionAmount,
                        WalletTransactionReference = WalletTrans.Reference,
                        DateCreated = WalletTrans.TransactionDate.ToString(),
                        PaymentResponse = WalletTrans.PayStackResponse
                    };
                    return ShipmentResponseData;
                }
                return null;
            }
        }
        public async Task<ShipmentResponseDTO> ProcessPaystackWebhook(string WayBill)
        {
            using (var db = new liblogisticsDataContext())
            {
                var paystackSecret = System.Configuration.ConfigurationManager.AppSettings["PayStackSecret"];

                //var api = new PayStackApi(paystackSecret);

                //Get Shipment By Waybill

                var WaybillToUpdate = db.shipments.Where(c => c.Waybill == WayBill).FirstOrDefault();
                //var WaybillToUpdate = GetShipmentsByWayBill(WayBill).FirstOrDefault();

                PaystackVerifyResponseDto paystackVerifyResponseDto = null;
                #region newPaystackVerifyImplementation
                var client = new RestClient("https://api.paystack.co/transaction/verify/" + WayBill);
                var request = new RestRequest(Method.GET);
                request.AddHeader("content-type", "application/json");
                request.AddHeader("Authorization", "Bearer " + paystackSecret);
                IRestResponse response = await client.ExecuteTaskAsync(request);
                var payResponse = response.Content;
                paystackVerifyResponseDto = JsonConvert.DeserializeObject<PaystackVerifyResponseDto>(response.Content);
                #endregion

                var responseData = paystackVerifyResponseDto?.data;
                //if not found of paystack
                if (responseData == null)
                {
                    return new ShipmentResponseDTO
                    {
                        Response = "Transaction Not Found On PayStack",
                        Amount = 0,
                        DateCreated = WaybillToUpdate.DateCreated.ToString(),
                        WayBill = WaybillToUpdate.Waybill
                    };
                }
                var authorization = responseData.authorization;

                var PayStackPaymentResponse = new PayStackWebhookResponse
                {
                    Reference = responseData.reference,
                    ApprovedAmount = responseData.amount / 100,
                    AuthorizationCode = authorization.authorization_code,
                    CardType = authorization.card_type,
                    Last4 = authorization.last4,
                    Reusable = authorization.reusable,
                    Bank = authorization.bank,
                    ExpireMonth = authorization.exp_month,
                    ExpireYear = authorization.exp_year,
                    TransactionDate = responseData.transaction_date.GetValueOrDefault(),
                    Channel = responseData.channel,
                    Status = responseData.status
                };

                WaybillToUpdate.PayStackWebhookResponse = PayStackPaymentResponse;

                db.SubmitChanges();

                PaymentStatus e = (PaymentStatus)WaybillToUpdate.PaymentStatus;

                ShipmentResponseDTO shipmentResponseData = new ShipmentResponseDTO
                {
                    Response = e.ToString(),
                    Amount = responseData.amount / 100,
                    WayBill = WaybillToUpdate.Waybill
                };
                return shipmentResponseData;
            }
        }
        public bool UpdateOnlineShipment(ShipmentModel model)
        {
            liblogisticsDataContext dt = new liblogisticsDataContext();
            try
            {
                if (model.PayStackPaymentResponse == null)
                {
                    return false;
                }
                List<shipment> shp = (from p in dt.shipments
                                      where p.Waybill == model.Waybill
                                      select p).ToList();

                PayStackPaymentResponse payStackPaymentResponse = new PayStackPaymentResponse
                {
                    Reference = model.PayStackPaymentResponse.Reference,
                    ApprovedAmount = model.PayStackPaymentResponse.ApprovedAmount,
                    AuthorizationCode = model.PayStackPaymentResponse.AuthorizationCode,
                    CardType = model.PayStackPaymentResponse.CardType,
                    Last4 = model.PayStackPaymentResponse.Last4,
                    Reusable = model.PayStackPaymentResponse.Reusable,
                    Bank = model.PayStackPaymentResponse.Bank,
                    ExpireMonth = model.PayStackPaymentResponse.ExpireMonth,
                    ExpireYear = model.PayStackPaymentResponse.ExpireYear,
                    TransactionDate = model.PayStackPaymentResponse.TransactionDate,
                    Channel = model.PayStackPaymentResponse.Channel,
                    Status = model.PayStackPaymentResponse.Status
                };

                foreach (var s in shp)
                {
                    s.PaymentStatus = model.PaymentStatus;
                    s.paymentMethod = model.paymentMethod;
                    s.DateModified = DateTime.Now;
                    s.PayStackPaymentResponse = payStackPaymentResponse;
                    s.PayStackReference = model.PayStackReference;
                    s.PayStackResponse = model.PayStackResponse;
                    s.IsRefund = false;
                }
                dt.SubmitChanges();
                return true;
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return false;
            }
        }
        public async Task<PayStackWebhookResponseDTO> PaystackWebHookRefAsync(string WayBill)
        {
            try
            {
                if (WayBill == null) { return null; }
                using (var db = new liblogisticsDataContext())
                {
                    var paystackWebHookDetails = from dbcx in db.PayStackWebhookResponses
                                                 where dbcx.Reference == WayBill
                                                 select new PayStackWebhookResponseDTO
                                                 {
                                                     Reference = dbcx.Reference,
                                                     ApprovedAmount = dbcx.ApprovedAmount / 100,
                                                     AuthorizationCode = dbcx.AuthorizationCode,
                                                     CardType = dbcx.CardType,
                                                     Last4 = dbcx.Last4,
                                                     Reusable = dbcx.Reusable,
                                                     Bank = dbcx.Bank,
                                                     ExpireMonth = dbcx.ExpireMonth,
                                                     ExpireYear = dbcx.ExpireYear,
                                                     TransactionDate = dbcx.TransactionDate,
                                                     Channel = dbcx.Channel,
                                                     Status = dbcx.Status
                                                 };

                    return paystackWebHookDetails.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<ShipmentModel> PendingShipments(int transactionDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var TransactionDate = DateTime.Now.AddDays(transactionDate);
                var UpwardLimit = DateTime.Now.AddMinutes(-20);

                var todayShipment = (from p in db.shipments
                                     where p.DateCreated >= TransactionDate
                                     && p.DateCreated <= UpwardLimit
                                     && p.PaymentStatus == Convert.ToInt32(PaymentStatus.Pending)
                                     && (p.paymentMethod == PaymentMethod.PayStack.ToString() ||
                                     p.paymentMethod == PaymentMethod.Isonhold.ToString())
                                     && p.isCancelled == false
                                     select new ShipmentModel
                                     {
                                         Id = p.Id,
                                         deliveryTypeId = p.deliveryTypeId.GetValueOrDefault(),
                                         Waybill = p.Waybill,
                                         senderAddress = p.senderAddress,
                                         PaymentStatus = p.PaymentStatus,
                                         paymentMethod = p.paymentMethod,
                                         DateCreated = p.DateCreated,
                                         createdBy = p.createdBy,
                                         totalTopay = p.totalTopay,
                                         customerId = p.customerId.GetValueOrDefault(),
                                         isInsured = p.isInsured,
                                         ValueIsDeceleared = p.valueIsDecleared,
                                         deClearedValue = p.deClearedValue.GetValueOrDefault()
                                     });

                return todayShipment.ToList();
            }
        }
        public List<WalletTransactionDTO> UncompletedWalletFunding(int transactionDate)
        {
            using (var db = new liblogisticsDataContext())
            {
                var TransactionDate = DateTime.Now.AddDays(transactionDate);
                var UpwardLimit = DateTime.Now.AddMinutes(-20);
                var todayWalletFund = (from p in db.WalletTransactions
                                       where p.IsCompleted == false && p.IsDeleted == false
                                       && p.CreationTime >= TransactionDate
                                       && p.CreationTime <= UpwardLimit
                                       select new WalletTransactionDTO
                                       {
                                           Id = p.Id,
                                           WalletId = p.WalletId,
                                           UserId = p.UserId,
                                           paymentMethod = p.paymentMethod,
                                           TransType = p.TransactionType,
                                           Reference = p.Reference,
                                           IsCompleted = p.IsCompleted,
                                           TransactedBy = p.TransactedBy,
                                           TransactionAmount = p.TransactionAmount,
                                           TransactionDate = p.TransactionDate,
                                           TransactionDescription = p.TransactionDescription,
                                           LineBalance = p.LineBalance,
                                           CreationTime = p.CreationTime.GetValueOrDefault()
                                       });

                return todayWalletFund.ToList();
            }
        }
        public async Task RevalidateAllPendingShipmentsWithCardPayment(int txnTime)
        {
            var allPendingShipments = PendingShipments(txnTime);

            if (allPendingShipments.Count > 0)
            {

                foreach (var p in allPendingShipments)
                {
                    var newDate = p.DateCreated.AddHours(2);
                    var newDate1Hour = p.DateCreated.AddMinutes(90);

                    if ((p.paymentMethod == PaymentMethod.PayStack.ToString()) && DateTime.Now >= newDate)
                    {
                        try
                        {
                            CancelShipment(p.Id);
                        }
                        catch (Exception ex)
                        {
                            ex.InnerException.ToString();
                        }
                    }

                    else
                    {
                        if (p.paymentMethod == PaymentMethod.PayStack.ToString())
                        {
                            try
                            {

                                await ProcessPaystackPayment(p.Waybill, TransactionType.Debit);
                            }
                            catch (Exception ex)
                            {
                                ex.InnerException.ToString();
                            }

                        }

                        if (p.paymentMethod == PaymentMethod.Isonhold.ToString())
                        {

                            if (p.PaymentStatus == Convert.ToInt32(PaymentStatus.Pending) && DateTime.Now >= newDate)
                            {
                                try
                                {
                                    CancelShipment(p.Id);

                                }
                                catch (Exception ex)
                                {
                                    ex.InnerException.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }
        public async Task RevalidatePendingFundWallet(int txnTime)
        {
            //Get all uncompleted wallet transaction
            var allPendingWalletFund = UncompletedWalletFunding(txnTime);
            if (allPendingWalletFund.Count > 0)
            {
                foreach (var p in allPendingWalletFund)
                {
                    var newDate = p.CreationTime.AddHours(2);
                    var newDate1Hour = p.CreationTime.AddMinutes(90);

                    if ((p.paymentMethod == PaymentMethod.PayStack.ToString()) && DateTime.Now >= newDate)
                    {
                        try
                        {
                            CancelWalletTransaction(p.Id);
                        }
                        catch (Exception ex)
                        {
                            ex.InnerException.ToString();
                        }
                    }
                    else
                    {
                        if (p.paymentMethod == PaymentMethod.PayStack.ToString())
                        {
                            try
                            {

                                await ProcessPaystackPayment(p.Reference, TransactionType.Credit);
                            }
                            catch (Exception ex)
                            {
                                ex.InnerException.ToString();
                            }

                        }

                        if (p.paymentMethod == PaymentMethod.Isonhold.ToString())
                        {

                            if (p.IsCompleted == false && DateTime.Now >= newDate)
                            {
                                try
                                {
                                    CancelWalletTransaction(p.Id);
                                }
                                catch (Exception ex)
                                {
                                    ex.InnerException.ToString();
                                }
                            }
                        }
                    }
                }
            }
        }

        public string GetUserAccountActivationCode(string userid)
        {
            using (var db = new liblogisticsDataContext())
            {
                var code = from p in db.AccountsConfirmations
                           where p.UserId == userid
                           select p.ActivationCode;
                return code.FirstOrDefault();
            }
        }
        #region complaint
        public List<returnmsg> CreateComplaint(Complaint model)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                model.CreationTime = DateTime.UtcNow.AddHours(1);
                db.Complaints.InsertOnSubmit(model);
                db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "your complaints was successfully saved!!";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.completed = false;
                rtmsg.errormsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }
        public List<Complaint> GetComplaints()
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            var complaints = (from c in db.Complaints select c).ToList();
            return complaints;
        }

        public IList<ComplaintModel> GetCompliantLists()
        {
            using (var db = new liblogisticsDataContext())
            {
                var itemdata = (from c in db.Complaints
                                select new ComplaintModel
                                {
                                    Id = c.Id,
                                    FullName = c.FullName,
                                    Email = c.Email,
                                    ComplaintType = c.ComplaintType,
                                    PriorityLevel = c.PriorityLevel,
                                    WayBillNumber = c.WayBillNumber,
                                    Message = c.Message,
                                    CreationTime = c.CreationTime,
                                    CreatorUserId = c.CreatorUserId,
                                    DeleterUserId = c.DeleterUserId,
                                    DeletionTime = c.DeletionTime,
                                    IsDeleted = c.IsDeleted,
                                    LastModificationTime = c.LastModificationTime,
                                    LastModifierUserId = c.LastModifierUserId,
                                    TransDate = c.TransDate,
                                    RepliedMessage = c.RepliedMessage,
                                    Responded = c.Responded
                                }).ToList();
                return itemdata;
            }

        }
        public Complaint GetComplaintById(int Id)
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            var complaint = (from c in db.Complaints where c.Id == Id select c).FirstOrDefault();
            return complaint;
        }
        public bool UpdateComplaint(Complaint model)
        {
            try
            {
                liblogisticsDataContext db = new liblogisticsDataContext();
                var complaints = (from c in db.Complaints where c.Id == model.Id select c).FirstOrDefault();
                //complaints = model;
                if (complaints != null)
                {
                    complaints.RepliedMessage = model.RepliedMessage;
                    complaints.Responded = model.Responded;
                }
                db.SubmitChanges();

                #region Send Complaint Response Email
                string dataFile = HostingEnvironment.ApplicationPhysicalPath + "messaging\\complaint-reply-email.html";
                var replacement = new StringDictionary
                {
                    ["WayBillNumber"] = complaints.WayBillNumber,
                    ["FullName"] = complaints.FullName,
                    ["RepliedMessage"] = complaints.RepliedMessage,
                };

                string emailsubject = $"Thank you for choosing Libmot Express.";

                var mail = new Mail(System.Configuration.ConfigurationManager.AppSettings["UserName"], emailsubject, complaints.Email)
                {
                    BodyIsFile = true,
                    BodyPath = dataFile
                };

                Task.Run(async () =>
                {
                    await SmtpEmailService.SendMailAsync(mail, replacement);
                });
                //var f = SmtpEmailService.SendMailAsync(mail, replacement).Result;
                #endregion

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public List<returnmsg> DeleteComplaint(int Id)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext db = new liblogisticsDataContext();
                var complaints = (from c in db.Complaints where c.Id == Id select c).FirstOrDefault();
                if (complaints != null)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    complaints.LastModificationTime = DateTime.Now;
                    complaints.IsDeleted = true;
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                //db.SubmitChanges();
                //return true;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.errormsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }

        }
        #endregion

        #region coupon
        public IQueryable<Coupon> GetAllCoupons()
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            return _ = db.Coupons.AsQueryable();
        }
        public async Task<Coupon> GetValidCouponByPhone(string couponCode, string phone)
        {
            phone = phone?.Trim();
            if (string.IsNullOrEmpty(phone) || (!phone.StartsWith("+234") && !phone.StartsWith("234") && !phone.StartsWith("0")))
            {
                throw new Exception("Invalid phone number");
            }

            var coupon = await GetCouponByCodeAsync(couponCode);

            if (coupon == null)
            {
                throw new Exception("Coupon does not exist");
            }
            coupon = await GetValidCouponByCodeAsync(couponCode);
            if (coupon == null)
            {
                throw new Exception("Coupon is invalid");
            }

            var couponexpiryDate = await GetCouponExpiryDate(couponCode);

            if (DateTime.Now > couponexpiryDate)
            {
                throw new Exception("Coupon has Expired");
            }


            var couponuseByGuest = await GetCouponUsedByPhoneAsync(couponCode, phone);
            if (couponuseByGuest != null)
            {
                throw new Exception("Coupon already used");
            }
            return coupon;
        }

        public Task<Coupon> GetCouponUsedByPhoneAsync(string couponCode, string phone)
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            var coupons = (from c in db.Coupons
                           where
      //c.PhoneNumber.Trim() == phone
      c.PhoneNumber.Trim().Substring(c.PhoneNumber.Trim().Length - 10) == phone.Substring(phone.Length - 10)
                           select c).FirstOrDefault();

            return Task.FromResult(coupons);
        }

        public async Task<DateTime> GetCouponExpiryDate(string couponCode)
        {
            DateTime Expiry = DateTime.Now;
            var coupon = await GetCouponByCodeAsync(couponCode);

            if (coupon == null)
            {
                throw new Exception("The coupon does not exist!!");
            }

            if (coupon.DurationType == (int)DurationType.Second)
            {
                Expiry = coupon.CreationTime.AddSeconds(coupon.Duration);
            }
            else if (coupon.DurationType == (int)DurationType.Minute)
            {
                Expiry = coupon.CreationTime.AddMinutes(coupon.Duration);
            }
            else if (coupon.DurationType == (int)DurationType.Hour)
            {
                Expiry = coupon.CreationTime.AddHours(coupon.Duration);
            }
            else if (coupon.DurationType == (int)DurationType.Day)
            {
                Expiry = coupon.CreationTime.AddDays(coupon.Duration);
            }
            else if (coupon.DurationType == (int)DurationType.Month)
            {
                Expiry = coupon.CreationTime.AddMonths(coupon.Duration);
            }
            else if (coupon.DurationType == (int)DurationType.Year)
            {
                Expiry = coupon.CreationTime.AddYears(coupon.Duration);
            }

            return Expiry;
        }

        public Task<Coupon> GetValidCouponByCodeAsync(string couponCode)
        {
            var coupons =
                from coupon in GetAllCoupons()
                where coupon.CouponCode == couponCode && coupon.Validity == true
                select new Coupon
                {
                    Id = coupon.Id,
                    CouponType = coupon.CouponType,
                    CouponCode = coupon.CouponCode,
                    Validity = coupon.Validity,
                    CouponValue = coupon.CouponValue,
                    CreationTime = coupon.CreationTime,
                    Duration = coupon.Duration,
                    DurationType = coupon.DurationType
                };

            return Task.FromResult(coupons.FirstOrDefault());
        }

        public Task<Coupon> GetCouponByCodeAsync(string couponCode)
        {
            var coupons =
                 from coupon in GetAllCoupons()
                 where coupon.CouponCode == couponCode
                 select new Coupon
                 {
                     Id = coupon.Id,
                     CouponType = coupon.CouponType,
                     CouponCode = coupon.CouponCode,
                     Validity = coupon.Validity,
                     CouponValue = coupon.CouponValue,
                     CreationTime = coupon.CreationTime,
                     Duration = coupon.Duration,
                     DurationType = coupon.DurationType
                 };
            return coupons.FirstOrDefaultAsync();
        }
        public List<returnmsg> CreateCoupon(Coupon model)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                using (var db = new liblogisticsDataContext())
                {
                    model.CouponCode = RandomDigits(9);
                    model.CouponType = (int)CouponType.Fixed;
                    model.CouponValue = 1000;
                    model.CreationTime = DateTime.Now;
                    model.DurationType = (int)DurationType.Year;
                    model.Id = Guid.NewGuid();
                    model.IsUsed = false;
                    model.LastModificationTime = DateTime.Now;
                    model.PhoneNumber = model.PhoneNumber;
                    model.Validity = true;

                    db.Coupons.InsertOnSubmit(model);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "your coupon was successfully saved!!";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.completed = false;
                rtmsg.errormsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }
        public Task<List<returnmsg>> CreateCouponAsync(Coupon model)
        {
            return Task.FromResult(CreateCoupon(model));
        }
        public Task<List<returnmsg>> DeleteCoupon(int Id)
        {
            return default;
        }
        public Task<bool> EditCoupon(Coupon model)
        {
            return default;
        }
        #endregion

        #region MarketingCoupons
        public bool UpdateCoupon(UpdateMarketingCouponDTO model)
        {
            try
            {
                liblogisticsDataContext db = new liblogisticsDataContext();
                var coupon = (from c in db.Coupons where c.CouponCode == model.CouponCode select c).FirstOrDefault();
                if (coupon != null)
                {
                    coupon.TotalUsage = coupon.TotalUsage + 1;
                }
                db.SubmitChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public IList<MarketingCouponDTO> GetMarketingCoupons()
        {
            using (var db = new liblogisticsDataContext())
            {
                var currentDateTime = DateTime.Now;

                var itemdata = (from C in db.Coupons
                                where C.IsUsed == false && C.Validity == true
                                && C.TotalUsage != C.VoucherType
                                && C.StartDate >= currentDateTime
                                && C.EndDate <= currentDateTime
                                select new MarketingCouponDTO
                                {
                                    Id = C.Id,
                                    CouponCode = C.CouponCode,
                                    CouponValue = C.CouponValue
                                }
                               ).ToList();
                return itemdata;
            }
        }

        public IList<MarketingCouponDTO> GetValidMarketingCoupon(string couponCode)
        {
            var currentDateTime = DateTime.Now.Date;

            var cc = couponCode;
            string Ecoupon = cc.Trim().Replace(" ", "+");


            var coupon = (from C in db.Coupons
                          where C.IsUsed == false && C.Validity == true
                          && C.TotalUsage != C.VoucherType
                          && currentDateTime >= C.StartDate
                          && currentDateTime <= C.EndDate
                          && C.CouponCode == Ecoupon
                          select new MarketingCouponDTO
                          {
                              Id = C.Id,
                              CouponCode = C.CouponCode,
                              CouponValue = C.CouponValue,
                              TotalLimit = C.VoucherType,
                              TotalUsage = C.TotalUsage

                          }
                           ).ToList();

            return coupon;
        }

        public List<returnmsg> updateMarketingCoupon(UpdateMarketingCouponDTO model)
        {
            return updateMarketingCoupon(model.CouponUserId, model.CouponCode, (AccountType)model.PlatformType, model.Waybill);
        }

        public List<returnmsg> updateMarketingCoupon(string couponUserID, string couponCode, AccountType PlatformType, string waybill)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                CouponManagement cm = new CouponManagement();

                cm.CouponUserId = couponUserID;
                cm.CouponCode = couponCode;
                cm.UsedDate = DateTime.Now;
                cm.PlatformType = (int?)PlatformType;
                cm.Waybill = waybill;
                cm.IsDeleted = false;

                db.CouponManagements.InsertOnSubmit(cm);
                db.SubmitChanges();

                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Added";
                retmsgs.Add(rtmsg);

                //AddTotalUsage(cm.CouponCode, 0);


                if (cm.CouponCode != null)
                {
                    var coupon = (from c in db.Coupons where c.CouponCode == cm.CouponCode select c).FirstOrDefault();
                    //Update Coupon 

                    if (coupon != null && coupon.TotalUsage < coupon.VoucherType)
                    {
                        coupon.TotalUsage += 1;
                        if (coupon.TotalUsage == coupon.VoucherType)
                        {
                            coupon.Validity = false;
                            coupon.IsUsed = true;
                            coupon.DateUsed = DateTime.Now.Date;
                            coupon.LastModificationTime = DateTime.Now;
                        }
                    }

                    db.SubmitChanges();
                    //return Ok(true);
                }



                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }

        #endregion

        #region referrals
        public Task<string> AddReferralCode(Referral referral)
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            bool personHasRef = true;
            try
            {
                //if person already has a code
                if (!String.IsNullOrEmpty(referral.Email))
                {
                    personHasRef = ((from r in db.Referrals where r.Email == referral.Email select r).FirstOrDefault()) == null ? false : true; ;
                }
                else if (!String.IsNullOrEmpty(referral.PhoneNumber))
                {
                    personHasRef = ((from r in db.Referrals where r.PhoneNumber == referral.PhoneNumber select r).FirstOrDefault()) == null ? false : true;
                }

                //if referral code already exists
                bool referralCodeAlreadyExists = true;
                while (referralCodeAlreadyExists)
                {
                    referral.ReferralCode = CommonHelper.GenerateRandonAlphaNumeric();
                    referralCodeAlreadyExists = ((from r in db.Referrals where r.ReferralCode == referral.ReferralCode select r).FirstOrDefault()) == null ? false : true;
                };
                if (!personHasRef)
                {

                    var entity = new Referral
                    {
                        Email = referral.Email,
                        PhoneNumber = referral.PhoneNumber,
                        ReferralCode = referral.ReferralCode,
                        UserType = referral.UserType
                    };

                    db.Referrals.InsertOnSubmit(entity);
                    db.SubmitChanges();
                    return Task.FromResult(referral.ReferralCode);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task<bool> ExistAsync(Expression<Func<Referral, bool>> predicate)
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            return Task.FromResult(db.Referrals.Any(predicate));
        }

        public async Task<Referral> GetReferralByEmail(string emailOrPhone)
        {
            liblogisticsDataContext db = new liblogisticsDataContext();
            var referral = await (from r in db.Referrals where r.Email == emailOrPhone || r.PhoneNumber == emailOrPhone select r).FirstOrDefaultAsync();

            return new Referral
            {
                Email = referral.Email,
                PhoneNumber = referral.PhoneNumber,
                ReferralCode = referral.ReferralCode,
                Id = referral.Id,
                UserType = referral.UserType
            };
        }
        #endregion

        public async Task<decimal> ComputeTotalShipment(ShipmentModel model)
        {
            var zoneId = GetZoneID(model.departureLocationId ?? 0, model.destinationId);

            //delivery type cost
            var deliveryFee = GetDeliveryFee(model.deliveryTypeId ?? 0, zoneId);

            //shipment item cost (normalShipmentPrice + genericShipmentPrice)
            decimal getShipPrice = 0;
            foreach (var item in model.Items)
            {
                if (Convert.ToInt32(item.ItemType) != -1)//(generic)
                {
                    //decimal price = GetGenericPricebyZoneIDspkId(zoneId, Convert.ToInt32(item.ItemType));
                    //getShipPrice += item.Quantity * price;
                    getShipPrice += await ComputeGenericShipmentItemprice(model.departureLocationId ?? 0, model.destinationId, Convert.ToInt32(item.ItemType), item.Quantity);
                }
                else
                {//normal
                    getShipPrice += await ComputeNormalShipmentItemPrice(model.departureLocationId ?? 0, model.destinationId, item.ItemWeight);
                }
            }

            //packaging cost
            liblogisticsDataContext db = new liblogisticsDataContext();
            decimal PackagingFee = model.packagingfee ?? 0;

            //insurance Amount
            if (model.deClearedValue < 20000) { model.deClearedValue = 20000; }
            decimal insuranceAmount = ((GetInsuranceAmt() / 100) * model.deClearedValue ?? 0);

            var shipmentCost = getShipPrice + deliveryFee + insuranceAmount;

            decimal vat = ((GetVat() / 100) * (shipmentCost + PackagingFee));
            return shipmentCost + vat + PackagingFee;
        }
        public async Task<decimal> ComputeNormalShipmentItemPrice(int departureLocationId, int destinationId, decimal weight, int SpecialShipmentPriceTypeId = 0)
        {
            var zoneId = GetZoneID(departureLocationId, destinationId);
            decimal getShipPrice = 0;

            if ((SpecialShipmentPriceTypeId != 0))
            {
                getShipPrice = GetSpecialpricingById(SpecialShipmentPriceTypeId);
                getShipPrice *= weight;
            }
            else if (weight > 10)
            { getShipPrice = GetPricePerKGbyZoneID(zoneId, weight); }
            else
            {
                getShipPrice = GetRegPricebyZoneID(zoneId, Math.Round(weight, MidpointRounding.ToEven));
            }
            return _ = await Task.FromResult(getShipPrice);
        }
        public async Task<decimal> ComputeGenericShipmentItemprice(int departureLocationId, int destinationId, int itemTypeId, int quantity)
        {
            var zoneId = GetZoneID(departureLocationId, destinationId);
            decimal price = GetGenericPricebyZoneIDspkId(zoneId, itemTypeId);
            var getShipPrice = quantity * price;
            return _ = await Task.FromResult(getShipPrice);
        }
        public async Task<List<ShipmentModel>> GetShipmentHistory(string userName)
        {
            try
            {
                LibDataClass libData = new LibDataClass();
                var userStore = new UserStore<IdentityUser>();
                var userManager = new UserManager<IdentityUser>(userStore);
                var user = userManager.FindByName(userName) ?? userManager.FindByEmail(userName);
                if (user != null)
                {
                    using (liblogisticsDataContext db = new liblogisticsDataContext())
                    {
                        var resp = await Task.FromResult((from s in db.shipments where s.UserId == user.Id orderby s.Id ascending select s).ToList().ConvertAll(si => (ShipmentModel)si));
                        return resp;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public IList<ShipmentModel> GetCreditShipment(int locationId, DateTime fromdate, DateTime todate, CreditStatus creditStatus)
        {
            List<ShipmentModel> resp = new List<ShipmentModel>();
            using (liblogisticsDataContext db = new liblogisticsDataContext())
            {
                switch (creditStatus)
                {
                    case CreditStatus.PaidCredit:
                        resp = (from sh in db.shipments
                                join lt in db.Locations on sh.destinationId equals lt.id
                                join shl in db.ShipmentCollections on sh.Waybill equals shl.wayBillNumber
                                //join gm in db.GroupWaybillNumMappings on sh.groupId equals gm.GroupWaybillNumber
                                //join mm in db.ManifestMappings on gm.GroupWaybillNumber equals mm.GroupWaybillNumber
                                //join m in db.Manifests on mm.ManifestId equals m.ManifestId
                                //join v in db.Vehicles on m.VehicleId equals v.id
                                where sh.isCancelled == false /*&& sh.destinationId == locationId*/ && sh.DateCreated >= fromdate.Date && sh.DateCreated <= todate.Date.AddDays(1).AddMilliseconds(-1) && sh.IsCredit == false
                                select new ShipmentModel
                                {
                                    Id = sh.Id,
                                    Location = new LocationModel { locationName = lt.locationName },
                                    DateCreated = sh.DateCreated,
                                    destinationId = sh.destinationId,
                                    Waybill = sh.Waybill,
                                    totalTopay = sh.totalTopay,
                                    createdBy = sh.createdBy,
                                    IsCredit = sh.IsCredit,
                                    CreditPaymentDate = shl.DateCreated,
                                    CreditReceiversname = shl.receiverName,
                                    CreditReceiversPhoneNo = shl.phoneNumber,
                                    CreditReleaseby = shl.releasedBy,
                                    VehicleRegNumber = null,//v.regNumber,
                                    TellerNumber = sh.TellerNumber,
                                    IsVerified = sh.IsVerified,
                                    VerifiedAmount = sh.VerifiedAmount
                                }).GroupBy(s => s.Id).Select(group => group.FirstOrDefault()).ToList();
                        return resp;

                    case CreditStatus.UnpaidCredit:
                        resp = (from sh in db.shipments
                                join lt in db.Locations on sh.departureLocationId equals lt.id
                                //join gm in db.GroupWaybillNumMappings on sh.groupId equals gm.GroupWaybillNumber
                                //join mm in db.ManifestMappings on gm.GroupWaybillNumber equals mm.GroupWaybillNumber
                                //join m in db.Manifests on mm.ManifestId equals m.ManifestId
                                //join v in db.Vehicles on m.VehicleId equals v.id
                                where sh.isCancelled == false /*&& sh.destinationId == locationId*/ && sh.DateCreated >= fromdate.Date && sh.DateCreated <= todate.Date.AddDays(1).AddMilliseconds(-1) && sh.IsCredit == true
                                select new ShipmentModel
                                {
                                    Id = sh.Id,
                                    Location = new LocationModel { locationName = lt.locationName },
                                    DateCreated = sh.DateCreated,
                                    destinationId = sh.destinationId,
                                    Waybill = sh.Waybill,
                                    totalTopay = sh.totalTopay,
                                    createdBy = sh.createdBy,
                                    IsCredit = sh.IsCredit,
                                    CreditPaymentDate = null,
                                    CreditReceiversname = null,
                                    CreditReceiversPhoneNo = null,
                                    CreditReleaseby = null,
                                    VehicleRegNumber = null,//v.regNumber,
                                    TellerNumber = sh.TellerNumber,
                                    IsVerified = sh.IsVerified,
                                    VerifiedAmount = sh.VerifiedAmount
                                }).GroupBy(s => s.Id).Select(group => group.FirstOrDefault()).ToList();
                        return resp;

                    default: /*CreditStatus.AllCredit:*/
                        resp = (from sh in db.shipments
                                join lt in db.Locations on sh.departureLocationId equals lt.id
                                //join gm in db.GroupWaybillNumMappings on sh.groupId equals gm.GroupWaybillNumber
                                //join mm in db.ManifestMappings on gm.GroupWaybillNumber equals mm.GroupWaybillNumber
                                //join m in db.Manifests on mm.ManifestId equals m.ManifestId
                                //join v in db.Vehicles on m.VehicleId equals v.id
                                where sh.isCancelled == false /*&& sh.destinationId == locationId*/ && sh.DateCreated >= fromdate.Date && sh.DateCreated <= todate.Date.AddDays(1).AddMilliseconds(-1) && (sh.IsCredit == true || sh.IsCredit == false)
                                select new ShipmentModel
                                {
                                    Id = sh.Id,
                                    Location = new LocationModel { locationName = lt.locationName },
                                    DateCreated = sh.DateCreated,
                                    destinationId = sh.destinationId,
                                    Waybill = sh.Waybill,
                                    totalTopay = sh.totalTopay,
                                    createdBy = sh.createdBy,
                                    IsCredit = sh.IsCredit,
                                    CreditPaymentDate = null,
                                    CreditReceiversname = null,
                                    CreditReceiversPhoneNo = null,
                                    CreditReleaseby = null,
                                    VehicleRegNumber = null,//v.regNumber,
                                    TellerNumber = sh.TellerNumber,
                                    IsVerified = sh.IsVerified,
                                    VerifiedAmount = sh.VerifiedAmount
                                }).GroupBy(s => s.Id).Select(group => group.FirstOrDefault()).ToList();
                        return resp;

                }
                //return resp;
            }

        }
        /// <summary>
        /// Used to calculate the charge for picking (from sender address) & / dropping shipment parcel at receiver location 
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="stateName"></param>
        /// <param name="vehicleType"></param>
        /// <param name="dispatchType"></param>
        /// <returns></returns>
        public CustomerGoodsPickupResponse GetCustomerGoodsPickupDetails(double distance, string stateName, VehicleType vehicleType, DispatchType dispatchType = DispatchType.Pickup)
        {
            var errorResponse = new CustomerGoodsPickupResponse
            {
                Price = 0
            };
            try

            {
                var priceRow = (from pr in db.PriceCalculators where pr.State == stateName select pr).FirstOrDefault();
                if (priceRow == null) { return errorResponse; }
                decimal price = 0;
                if (dispatchType == DispatchType.Pickup)
                {
                    if (priceRow.IsDefaultCost == true)
                    {
                        if (vehicleType == VehicleType.Bike) { price = priceRow.DefaultBikePrice ?? 0; }
                        else if (vehicleType == VehicleType.Van) { price = priceRow.DefaultVanPrice ?? 0; }
                        else if (vehicleType == VehicleType.Truck) { price = priceRow.DefaultTruckPrice ?? 0; }
                    }
                    else
                    {
                        if (vehicleType == VehicleType.Bike) { price = priceRow.PriceTDRforBike ?? 0; }
                        else if (vehicleType == VehicleType.Van) { price = priceRow.PriceTDRforVan ?? 0; }
                        else if (vehicleType == VehicleType.Truck) { price = priceRow.PriceTDRforTruck ?? 0; }
                    }
                }
                else if (dispatchType == DispatchType.DropOff)
                {
                    if (priceRow.IsDefaultCost == true)
                    {
                        if (vehicleType == VehicleType.Bike) { price = priceRow.DefaultBikePrice ?? 0; }
                        else if (vehicleType == VehicleType.Van) { price = priceRow.DefaultVanPrice ?? 0; }
                        else if (vehicleType == VehicleType.Truck) { price = priceRow.DefaultTruckPrice ?? 0; }
                    }
                    else
                    {
                        if (vehicleType == VehicleType.Bike) { price = priceRow.DropOffPriceforBike ?? 0; }
                        else if (vehicleType == VehicleType.Van) { price = priceRow.DropOffPriceforVan ?? 0; }
                        else if (vehicleType == VehicleType.Truck) { price = priceRow.DropOffPriceforTruck ?? 0; }
                    }
                }
                var dispatchPrice = price * Convert.ToDecimal((distance / 1000));//cos distance received is in meter

                if (priceRow.MaximumPickUpCost != null && priceRow.MaximumPriceTrigger != null)
                {
                    var maxVal = (priceRow.MaximumPickUpCost ?? 0);
                    var MaxTrigger = (priceRow.MaximumPriceTrigger ?? 0);
                    if (dispatchPrice > MaxTrigger)
                    {
                        dispatchPrice = maxVal;
                    }
                }

                if (priceRow.MinimumPickUpCost != null && priceRow.MinimumPriceTrigger != null)
                {
                    var minVal = (priceRow.MinimumPickUpCost ?? 0);
                    var MinTrigger = (priceRow.MinimumPriceTrigger ?? 0);
                    if (dispatchPrice < MinTrigger)
                    {
                        dispatchPrice = minVal;
                    }
                }

                var model = new CustomerGoodsPickupResponse
                {
                    Price = dispatchPrice
                };
                return model;
                /*
                 assumptions:
                 1  we'd be be calling this method differently for pickup & dropOff
                 2  the default price applies to both the pickup & dropOff (and that they are using the same default prices)
                 3  the minimum & maximum value applies to both the pickup and drofOff (and that they are using the same minimum & maximum value)
                 4  the api is getting states corresponding to the pickup and dropOff state 
                 */
            }
            catch (Exception ex)
            {
                return errorResponse;
            }
        }
        public CustomerGoodsPickupResponse GetDispatchPriceDetails(CustomerGoodsPickupRequest model)//CustomerGoodsPickupResponse
        //(double distance, string stateName, VehicleType vehicleType, DispatchType dispatchType = DispatchType.Pickup)
        {
            CustomerGoodsPickupResponse resp = new CustomerGoodsPickupResponse();
            CustomerGoodsPickupResponse actualPrice = new CustomerGoodsPickupResponse();

            if (model.DispatchType == DispatchType.PickupAndDropOff)
            {
                var pickupPrice = GetCustomerGoodsPickupDetails(model.PickupDistance, model.PickupStateName, model.VehicleType, DispatchType.Pickup).Price;
                var dropOffPrice = GetCustomerGoodsPickupDetails(model.DropOffDistance, model.DropOffStateName, model.VehicleType, DispatchType.DropOff).Price;
                resp.Price = (pickupPrice + dropOffPrice);
            }
            else if (model.DispatchType == DispatchType.Pickup)
            {
                resp.Price = GetCustomerGoodsPickupDetails(model.PickupDistance, model.PickupStateName, model.VehicleType, DispatchType.Pickup).Price;
            }
            else if (model.DispatchType == DispatchType.DropOff)
            {
                resp.Price = GetCustomerGoodsPickupDetails(model.DropOffDistance, model.DropOffStateName, model.VehicleType, DispatchType.DropOff).Price;
            }
            return resp;
        }
        public MerchantGoodsPickupResponse GetMerchantGoodsPickupDetails(string merchantUserName, double distance, string pickUpState, string dropUpState, int? weightRangeId, MerchantPriceType priceType, DispatchType dispatchType = DispatchType.Pickup)
        {

            var destinationState = (from S in db.States where S.name == dropUpState select S).FirstOrDefault();
            var departureState = (from S in db.States where S.name == pickUpState select S).FirstOrDefault();
            bool IsInterState = true;
            if (destinationState.name == departureState.name)
            {//Intra-state (within a state)
                IsInterState = false;
            }
            else
            {//Inter-state (between states)
                IsInterState = true;
            }

            var errorResponse = new MerchantGoodsPickupResponse
            {
                Price = 0
            };
            try
            {
                //var merchantRow = (from ms in db.MerchantSignups where ms.emailladdress == merchantUserName select ms).FirstOrDefault();
                var merchantRow = (from ms in db.MerchantSignups
                                   join mwp in db.MerchantWeightRangePrices on ms.id equals mwp.MerchantId
                                   where ms.emailladdress == merchantUserName
                                   select ms).FirstOrDefault();

                var weightRange = (from wr in db.MerchantWeightRangePrices where wr.MerchantId == merchantRow.id && wr.WeightRangeId == weightRangeId select wr).FirstOrDefault();

                if (merchantRow == null) { return errorResponse; }
                decimal price = 0;
                if (dispatchType == DispatchType.Pickup)
                {
                    if (priceType == MerchantPriceType.Fixed)
                    {
                        if (IsInterState == false)
                        {
                            price = merchantRow.FixedPrice ?? 0;
                        }
                        else
                        {
                            price = merchantRow.FixedPriceInterState ?? 0;
                        }
                    }
                    else if (priceType == MerchantPriceType.Weight)
                    {
                        if (IsInterState == false)
                        {
                            if (weightRange.WeightRangeId == weightRangeId)
                            {
                                var weightPrice = weightRange.WeightPrice;
                                var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                                price = weightPrice + pricePerKmPrice ?? 0;
                            }
                        }
                        else
                        {
                            if (weightRange.WeightRangeId == weightRangeId)
                            {
                                var weightPrice = weightRange.WeightPriceInterState;
                                var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                                price = weightPrice + pricePerKmPrice ?? 0;
                            }
                        }
                    }
                    else if (priceType == MerchantPriceType.FixedandKM)
                    {
                        if (IsInterState == false)
                        {
                            var fixedPrice = merchantRow.FixedPrice ?? 0;
                            var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                            price = fixedPrice + pricePerKmPrice ?? 0;
                        }
                        else
                        {
                            var fixedPrice = merchantRow.FixedPriceInterState ?? 0;
                            var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                            price = fixedPrice + pricePerKmPrice ?? 0;
                        }
                    }
                    else
                    {
                        price = 0;
                    }
                }
                else if (dispatchType == DispatchType.DropOff)
                {
                    if (priceType == MerchantPriceType.Fixed)
                    {
                        if (IsInterState == false)
                        {
                            price = merchantRow.FixedPrice ?? 0;
                        }
                        else
                        {
                            price = merchantRow.FixedPriceInterState ?? 0;
                        }
                    }
                    else if (priceType == MerchantPriceType.Weight)
                    {
                        if (IsInterState == false)
                        {
                            if (weightRange.WeightRangeId == weightRangeId)
                            {
                                var weightPrice = weightRange.WeightPrice;
                                var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                                price = weightPrice + pricePerKmPrice ?? 0;
                            }
                        }
                        else
                        {
                            if (weightRange.WeightRangeId == weightRangeId)
                            {
                                var weightPrice = weightRange.WeightPriceInterState;
                                var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                                price = weightPrice + pricePerKmPrice ?? 0;
                            }
                        }
                    }
                    else if (priceType == MerchantPriceType.FixedandKM)
                    {
                        if (IsInterState == false)
                        {
                            var fixedPrice = merchantRow.FixedPrice ?? 0;
                            var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                            price = fixedPrice + pricePerKmPrice ?? 0;
                        }
                        else
                        {
                            var fixedPrice = merchantRow.FixedPriceInterState ?? 0;
                            var pricePerKmPrice = merchantRow.PricePerKm * Convert.ToDecimal((distance / 1000));//cos distance received is in meter
                            price = fixedPrice + pricePerKmPrice ?? 0;
                        }
                    }
                    else
                    {
                        price = 0;
                    }
                }


                var dispatchPrice = price;

                var model = new MerchantGoodsPickupResponse
                {
                    Price = dispatchPrice
                };
                return model;
                /*
                 assumptions:
                 1  we'd be be calling this method differently for pickup & dropOff
                 2  the default price applies to both the pickup & dropOff (and that they are using the same default prices)
                 3  the minimum & maximum value applies to both the pickup and drofOff (and that they are using the same minimum & maximum value)
                 4  the api is getting states corresponding to the pickup and dropOff state 
                 */
            }
            catch (Exception ex)
            {
                return errorResponse;
            }
        }
        public MerchantGoodsPickupResponse GetMerchantDispatchPriceDetails(MerchantGoodsPickupRequest model)
        {
            MerchantGoodsPickupResponse resp = new MerchantGoodsPickupResponse();
            if (model.DispatchType == DispatchType.PickupAndDropOff)
            {
                var pickupPrice = GetMerchantGoodsPickupDetails(model.MerchantUserName, model.PickupDistance, model.PickupStateName, model.DropOffStateName, model.WeightRangeId, model.PriceType, DispatchType.Pickup).Price;
                var dropOffPrice = GetMerchantGoodsPickupDetails(model.MerchantUserName, model.DropOffDistance, model.DropOffStateName, model.DropOffStateName, model.WeightRangeId, model.PriceType, DispatchType.DropOff).Price;
                resp.Price = (pickupPrice + dropOffPrice);
            }
            else if (model.DispatchType == DispatchType.Pickup)
            {
                resp.Price = GetMerchantGoodsPickupDetails(model.MerchantUserName, model.DropOffDistance, model.DropOffStateName, model.DropOffStateName, model.WeightRangeId, model.PriceType, DispatchType.DropOff).Price;
            }
            else if (model.DispatchType == DispatchType.DropOff)
            {
                resp.Price = GetMerchantGoodsPickupDetails(model.MerchantUserName, model.DropOffDistance, model.DropOffStateName, model.DropOffStateName, model.WeightRangeId, model.PriceType, DispatchType.DropOff).Price;
            }
            return resp;
        }

        public CustomerGoodsPickupResponse GetMerchantPriceDetails(CustomerGoodsPickupRequest model)
        {
            CustomerGoodsPickupResponse resp = new CustomerGoodsPickupResponse();

            return resp;
        }
        public double ComputeDistanceBetweenCoordinates(double sLatitude, double sLongitude, double eLatitude, double eLongitude)
        {
            var radiansOverDegrees = (Math.PI / 180.0);

            var sLatitudeRadians = sLatitude * radiansOverDegrees;
            var sLongitudeRadians = sLongitude * radiansOverDegrees;
            var eLatitudeRadians = eLatitude * radiansOverDegrees;
            var eLongitudeRadians = eLongitude * radiansOverDegrees;

            var dLongitude = eLongitudeRadians - sLongitudeRadians;
            var dLatitude = eLatitudeRadians - sLatitudeRadians;

            var result1 = Math.Pow(Math.Sin(dLatitude / 2.0), 2.0) +
                          Math.Cos(sLatitudeRadians) * Math.Cos(eLatitudeRadians) *
                          Math.Pow(Math.Sin(dLongitude / 2.0), 2.0);

            //Using 3956 as the radius of the earth (in miles actually)
            //var result2 = 3956.0 * 2.0 * Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));

            //in km 
            var result2 = 6366.56486 * 2.0 * Math.Atan2(Math.Sqrt(result1), Math.Sqrt(1.0 - result1));
            return result2;
        }
        public bool AddQuotation(QuotationModel model)
        {
            try
            {
                if (model == null) { return false; }
                liblogisticsDataContext db = new liblogisticsDataContext();
                var data = new Quotation
                {
                    Comment = model.Comment,
                    CreatedBy = model.CreatedBy,
                    DateCreated = DateTime.Now,
                    DestinationAddress = model.DestinationAddress,
                    EmailAddress = model.EmailAddress,
                    Id = model.Id,
                    IsDeleted = false,
                    Name = model.Name,
                    NumberOfItems = model.NumberOfItems,
                    NumberOfVehicles = model.NumberOfVehicles,
                    PackageDescription = model.PackageDescription,
                    PhoneNumber = model.PhoneNumber,
                    PickupAddress = model.PickupAddress,
                    PickUpDateTime = model.PickUpDateTime,
                    VehicleType = (int?)model.VehicleType,
                    Weight = model.Weight
                };
                db.Quotations.InsertOnSubmit(data);
                db.SubmitChanges();
                //then there is the part of sending a message (sms/email) to the operations dept
                string msg = $"{model.Name} sent a quotation for shipment. Please review and response";
                SendSms("09062547031", msg);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public IList<QuotationModel> GetQuotations()
        {
            using (var db = new liblogisticsDataContext())
            {
                var itemdata = (from q in db.Quotations
                                select new QuotationModel
                                {
                                    Id = q.Id,
                                    Name = q.Name,
                                    EmailAddress = q.EmailAddress,
                                    PhoneNumber = q.PhoneNumber,
                                    PickupAddress = q.PickupAddress,
                                    DestinationAddress = q.DestinationAddress,
                                    PickUpDateTime = q.PickUpDateTime,
                                    PackageDescription = q.PackageDescription,
                                    DateCreated = q.DateCreated

                                }).ToList();
                return itemdata;
            }

        }
        public int? GetShipmentStatus(string waybill)
        {
            try
            {
                if (string.IsNullOrEmpty(waybill)) { return null; }
                liblogisticsDataContext db = new liblogisticsDataContext();
                var shipment = (from s in db.shipments where s.Waybill == waybill select s).ToList().FirstOrDefault();
                if (shipment == null) { return null; }
                if (shipment.AccountType == 0 || shipment.AccountType == 1)
                {
                    var splstatus = (from sparcel in db.ShipmentParcels where sparcel.Waybill == waybill select sparcel.ParcelStatus).FirstOrDefault();
                    if (splstatus != ((int)ParcelStatus.IsCompleted).ToString())
                    {
                        return splstatus != null ? (int?)Convert.ToInt32(splstatus) : null;
                    }
                }
                var manifest = (from m in db.Manifests where m.shipment.Waybill == waybill select m).FirstOrDefault();
                //var manifest = (from m in db.Manifests where m.ManifestMappings. == waybill select m).FirstOrDefault();
                if (manifest?.IsDispatched != true) { return ((int)ParcelStatus.Waiting); }
                if (manifest.IsDispatched == true && manifest.IsReceived != true) { return ((int)ParcelStatus.InTransit); }
                if (manifest.IsDispatched == true && manifest.IsReceived == true) { return ((int)ParcelStatus.Delivered); }
                return null;
            }
            catch (Exception)
            {
                return null;
            }

        }
        public string DispatchRiderName(string id)
        {
            try
            {
                if (id == null) { return null; }
                //get dispatch rider data
                liblogisticsDataContext db = new liblogisticsDataContext();
                var dispatchRiderRowId = (from r in db.AspNetRoles where r.Name == "Dispatch Rider" select r.Id).FirstOrDefault();
                List<Employee> dispatchRiders = (from disp in db.Employees where disp.AspNetUser.AspNetUserRoles.Any(ar => ar.RoleId == dispatchRiderRowId) select disp).ToList();
                var dr = dispatchRiders.Where(emp => emp.id.ToString() == id).FirstOrDefault();
                if (dr == null) { return null; }
                return $"{dr.FirstName} {dr.LastName}";
            }
            catch (Exception)
            {
                return null;
            }
        }
        public List<returnmsg> CreateWallet(string userId, string userType)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                //var user = Thread.CurrentPrincipal;
                liblogisticsDataContext db = new liblogisticsDataContext();
                var exist = (from w in db.Wallets where w.UserId == userId select w).FirstOrDefault() != null;
                if (exist)
                {
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Already exist!!";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                #region InsertWallet

                var walletNumber = GenerateNextValidWalletNumber();

                var insertWalletNumber = new WalletNumber
                {
                    CreationTime = DateTime.Now,
                    IsActive = true,
                    WalletPan = walletNumber.WalletPan
                };
                db.WalletNumbers.InsertOnSubmit(insertWalletNumber);
                db.SubmitChanges();

                var wallet = new Wallet
                {
                    WalletNumber = walletNumber.WalletPan,
                    Balance = 0.00M,
                    UserType = userType,//UserType.Employee.ToString(),
                    UserId = userId,
                    IsDeleted = false,
                    IsReset = false,
                    CreationTime = DateTime.Now
                };
                db.Wallets.InsertOnSubmit(wallet);
                db.SubmitChanges();
                #endregion
                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "successfully Added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.completed = false;
                rtmsg.code = "Error";
                rtmsg.successmsg = ex?.ToString();
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
        }
        public UserType? GetUserType(string userId)
        {
            try
            {
                if ((from c in db.customers where c.UserId == userId select c).FirstOrDefault() != null)
                {
                    return UserType.Customer;
                }
                else if ((from m in db.MerchantSignups where m.UserId == userId select m).FirstOrDefault() != null)
                {
                    return UserType.Merchant;
                }
                else if ((from e in db.Employees where e.UserId == userId select e).FirstOrDefault() != null)
                {
                    return UserType.Employee;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// get the amount to pay the dispatch rider for carrying the parcel to the terminal (or receiver if )
        /// </summary>
        /// <param name="dispatchRiderId"></param>
        /// <param name="shipmentParcelId"></param>
        /// <returns></returns>
        public decimal GetDispatchRiderCommission(int dispatchRiderId, int shipmentParcelId)
        {
            //get the right price calculator row based on price and vehicle type
            var dr = (from d in db.Employees where d.id == dispatchRiderId select d).FirstOrDefault();
            var spl = (from sp in db.ShipmentParcels where sp.Id == shipmentParcelId select sp).FirstOrDefault();
            var price = (from pc in db.PriceCalculators where pc.State == dr.Location.State.name select pc).FirstOrDefault();
            var vType = (from v in db.Vehicles where v.regNumber == spl.Vehicle select v.VehicleType).FirstOrDefault();

            decimal commission = 0.00m;
            if (vType == (int)VehicleType.Bike)
            {
                commission = price.CommissionforBike ?? 0;
            }
            else if (vType == (int)VehicleType.Van)
            {
                commission = price.CommissionforVan ?? 0;
            }
            else if (vType == (int)VehicleType.Truck)
            {
                commission = price.CommissionforTruck ?? 0;
            }
            spl.DispatchRiderCommission = commission;
            db.SubmitChanges();
            return commission;
        }
        public string GetVehicleByShipment()
        {
            return default;
        }
        /// <summary>
        /// get the first relevant farecalendar
        /// </summary>
        /// <param name="accountType"></param>
        /// <returns></returns>
        public FareCalendarModel GetRequiredFareCalendarForMobile(AccountType accountType, VehicleType? vehicleType)
        {
            var fareCalendar = (from fc in db.FareCalendars
                                where (fc.AccountType == (int?)accountType)
                                && fc.IsActive == true
                                && DateTime.Now >= fc.DateFrom && DateTime.Now <= fc.DateTo
                                select fc).ToList();
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    fareCalendar = fareCalendar.Where(f => f.Monday == true).ToList();
                    break;
                case DayOfWeek.Tuesday:
                    fareCalendar = fareCalendar.Where(f => f.Tuesday == true).ToList();
                    break;
                case DayOfWeek.Wednesday:
                    fareCalendar = fareCalendar.Where(f => f.Wednesday == true).ToList();
                    break;
                case DayOfWeek.Thursday:
                    fareCalendar = fareCalendar.Where(f => f.Thursday == true).ToList();
                    break;
                case DayOfWeek.Friday:
                    fareCalendar = fareCalendar.Where(f => f.Friday == true).ToList();
                    break;
                case DayOfWeek.Saturday:
                    fareCalendar = fareCalendar.Where(f => f.Saturday == true).ToList();
                    break;
                case DayOfWeek.Sunday:
                    fareCalendar = fareCalendar.Where(f => f.Sunday == true).ToList();
                    break;
            }
            if (vehicleType != null)
            {
                switch (vehicleType)
                {
                    case VehicleType.Bike:
                        fareCalendar = fareCalendar.Where(f => f.IsBikeable == true).ToList();
                        break;
                    case VehicleType.Van:
                        fareCalendar = fareCalendar.Where(f => f.IsVanable == true).ToList();
                        break;
                    case VehicleType.Truck:
                        fareCalendar = fareCalendar.Where(f => f.IsTruckable == true).ToList();
                        break;
                }
            }
            FareCalendarModel resp = fareCalendar.FirstOrDefault();
            return resp;
        }

        #region Push Notification (For Dispatch rider & Logistics)
        public string GetDispatchExpressDeviceIdByUserName(string userName)
        {
            var deviceId = (from a in db.AspNetUsers where a.UserName == userName select a.DispatchExpressDeviceId).FirstOrDefault();
            return deviceId;
        }
        /// <summary>
        /// Send message from server to dispatch rider device 
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="deviceId"></param>
        /// <returns></returns>
        public FCMPushNotificationResponseModel SendNotification(string title, string message, string deviceId, string serverKey, string senderId)
        {
            FCMPushNotificationResponseModel result = new FCMPushNotificationResponseModel();
            try
            {
                result.Successful = true;
                result.Error = null;
                //string serverKey = Constant.FCM_Express_Dispatch_Server_Key;
                //string senderId = Constant.FCM_Express_Dispatch_Sender_Id;
                var requestUri = "https://fcm.googleapis.com/fcm/send";

                RestClient client = new RestClient(requestUri);
                RestRequest request = new RestRequest()
                {
                    Method = Method.POST,
                    RequestFormat = DataFormat.Json
                };
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Authorization", $"key={serverKey}");
                request.AddHeader("Sender", $"id={senderId}");

                var data = new
                {
                    to = deviceId,
                    priority = "high",
                    notification = new
                    {
                        title = title,
                        body = message,
                        show_in_foreground = "true",
                        icon = "myicon"
                    }
                };
                request.AddJsonBody(data);
                IRestResponse response = client.Execute(request);
                result.Response = response.Content;
                result.StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                result.Successful = false;
                result.Response = null;
                result.Error = ex;
            }
            return result;
        }
        #endregion
        #region Discount
        public decimal OnboardingDiscount(ShipmentModel shipmentModel, string userid)
        {
            //first check if the user has not already used his On-boarding Discount
            var userOnboardingDiscount = (from a in db.AspNetUsers where a.Id == userid && a.IsOnboardingDiscountUsed != true select a).FirstOrDefault();
            if (userOnboardingDiscount == null)
            {
                return 0;
            }
            var data = (from dt in db.DiscountTypes
                        where dt.Name == DiscountTypesEnum.OnboardingDiscount.ToString()
                        join d in db.Discounts on dt.Id equals d.DiscountType
                        where d.IsActive == true
                        select new DiscountModel
                        {
                            Name = dt.Name,
                            DiscountValueType = d.DiscountValueType,
                            ValueAmount = d.ValueAmount,
                        }
                        ).FirstOrDefault();
            decimal onboardingDiscount = 0;
            /*
             try to calculate the total price by yourself!
             */
            if (data != null)
            {
                if (data.DiscountValueType == (int)ConvertRate.Percentage)
                {
                    onboardingDiscount = Convert.ToDecimal(shipmentModel.GrandTotalValue) * (data.ValueAmount ?? 0) * 0.01m;//HARDCORE ALERT!!
                    userOnboardingDiscount.IsOnboardingDiscountUsed = true;
                    db.SubmitChanges();
                    return onboardingDiscount;
                }
                else if (data.DiscountValueType == (int)ConvertRate.Value)
                {
                    onboardingDiscount = data.ValueAmount ?? 0;
                    userOnboardingDiscount.IsOnboardingDiscountUsed = true;
                    db.SubmitChanges();
                    return onboardingDiscount;
                }
            }
            return onboardingDiscount;
        }
        public decimal TransactionalDiscount(decimal price, string userId)
        {
            //check if the person has Transactional Discount(s)
            var userTransactionalDiscount = (from a in db.AspNetUsers where a.Id == userId && a.TransactionalDiscount > 0 select a).FirstOrDefault();
            if (userTransactionalDiscount == null)
            {
                return 0;
            }
            var data = (from dt in db.DiscountTypes
                        where dt.Name == DiscountTypesEnum.TransactionalDiscount.ToString()
                        join d in db.Discounts on dt.Id equals d.DiscountType
                        where d.IsActive == true
                        select new DiscountModel
                        {
                            Name = dt.Name,
                            DiscountValueType = d.DiscountValueType,
                            ValueAmount = d.ValueAmount,
                        }
                        ).FirstOrDefault();
            decimal TransactionalDiscount = 0;
            if (data != null)
            {
                if (data.DiscountValueType == (int)ConvertRate.Percentage)
                {
                    TransactionalDiscount = price * (data.ValueAmount ?? 0) * 0.01m;
                    userTransactionalDiscount.TransactionalDiscount -= 1;
                    db.SubmitChanges();
                    return TransactionalDiscount;
                }
                else if (data.DiscountValueType == (int)ConvertRate.Value)
                {
                    TransactionalDiscount = data.ValueAmount ?? 0;
                    userTransactionalDiscount.TransactionalDiscount -= 1;
                    db.SubmitChanges();
                    return TransactionalDiscount;
                }
            }
            return TransactionalDiscount;
            /*
             - This assumes that in the active row with discountType TransactionalDiscount, value amount would be set to 5(dynamic) and the value type be set to percentage(i.e. 0 dynamic) on the discount table to conform with the standard set this type of discount
             - also that at the point of funding wallet, a value of 1 is added for each 5000(would be dynamic) added to the wallet.
             */
        }
        public decimal ReferralDiscount(decimal price, string userId)
        {
            //check if the user has enough referral point to get the referral discount
            var userReferralPoint = (from a in db.AspNetUsers where a.Id == userId && a.ReferralPoint > 0 select a).FirstOrDefault();
            if (userReferralPoint == null || userReferralPoint.ReferralPoint < 5)
            {
                return 0;
            }
            var data = (from dt in db.DiscountTypes
                        where dt.Name == DiscountTypesEnum.ReferralDiscount.ToString()
                        join d in db.Discounts on dt.Id equals d.DiscountType
                        where d.IsActive == true
                        select new DiscountModel
                        {
                            Name = dt.Name,
                            DiscountValueType = d.DiscountValueType,
                            ValueAmount = d.ValueAmount,
                        }
                        ).FirstOrDefault();
            decimal ReferralDiscount = 0;
            if (data != null)
            {
                if (data.DiscountValueType == (int)ConvertRate.Percentage)
                {
                    ReferralDiscount = price * (data.ValueAmount ?? 0) * 0.01m;
                    userReferralPoint.ReferralPoint -= 5;
                    db.SubmitChanges();
                    return ReferralDiscount;
                }
                else if (data.DiscountValueType == (int)ConvertRate.Value)
                {
                    ReferralDiscount = data.ValueAmount ?? 0;
                    userReferralPoint.ReferralPoint -= 5;
                    db.SubmitChanges();
                    return ReferralDiscount;
                }
            }
            return ReferralDiscount;
            /*
            - This assumes that in the active row with discountType ReferralDiscount, value amount would be set to 10(dynamic) and the value type be set to percentage(i.e. 0 dynamic) on the discount table to conform with the standard set this type of discount
            - also that at the point of registration, if the new user uses a referral code from someone, that person gets 1 ReferralDiscount
            */
        }


        //Customer(Merchant) Acquisition Discount Types
        //public decimal VolumeDiscount(decimal price, string userId)
        //{


        //}
        #endregion
        //public void Update


        //Random Coupon Codes

        public string GetRandomCouponCode()
        {

            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var stringChars = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;

        }
        //abayomi addded this lines of code

        public IList<ServiceTypeModel> GetServiceTypes()
        {

            using (var db = new liblogisticsDataContext())
            {
                var servicedata = (from s in db.ServiceTypes
                                   where s.IsDeleted == false
                                   select new ServiceTypeModel
                                   {
                                       ID = s.ID,
                                       ServiceType = s.ServiceType1,
                                       CreatorUserId = s.CreatorUserId,
                                       CreationTime = s.CreationTime,
                                       LastModificationDate = s.LastModificationDate,
                                       LastModificationUserId = s.LastModificationUserId,
                                       IsDeleted = s.IsDeleted
                                   }).ToList();
                return servicedata;
            }
        }

        //Added by abayomi 22 March 2021
        public List<returnmsg> AddServiceType(ServiceTypeModel model)
        {
            return AddServiceType(model.ID, model.ServiceType, model.CreatorUserId, model.CreationTime, model.LastModificationUserId, model.LastModificationDate, model.IsDeleted);
        }
        public List<returnmsg> AddServiceType(int id, string serviceType, string creatorUserId, DateTime creationTime, string LastModificationUserId, DateTime? lastModificationDate, bool IsDeleted)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var servicetype = from s in db.ServiceTypes
                                  where s.ServiceType1.ToLower() == serviceType.ToLower()
                                  select s;
                if (servicetype.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Service Type already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {

                    ServiceType serviceTypes = new ServiceType();
                    serviceTypes.ID = id;
                    serviceTypes.ServiceType1 = serviceType;
                    serviceTypes.CreatorUserId = creatorUserId;
                    serviceTypes.CreationTime = creationTime;
                    serviceTypes.LastModificationUserId = LastModificationUserId;
                    serviceTypes.LastModificationDate = lastModificationDate;
                    serviceTypes.IsDeleted = IsDeleted;




                    db.ServiceTypes.InsertOnSubmit(serviceTypes);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Service Type successfully Added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> UpdateServiceType(ServiceTypeModel model)
        {
            return UpdateServiceType(model.ID, model.ServiceType, model.CreatorUserId, model.CreationTime, model.LastModificationUserId, model.LastModificationDate, model.IsDeleted);
        }
        public List<returnmsg> UpdateServiceType(int id, string serviceType, string creatorUserId, DateTime creationTime, string LastModificationUserId, DateTime? lastModificationDate, bool IsDeleted)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();

                ServiceType serviceTypes = (from c in dt.ServiceTypes
                                            where c.ID == id
                                            select c).ToList().FirstOrDefault();
                if (serviceTypes == null)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No platform to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    serviceTypes.ServiceType1 = serviceType;
                    serviceTypes.CreatorUserId = creatorUserId;
                    serviceTypes.CreationTime = creationTime;
                    serviceTypes.LastModificationDate = lastModificationDate;
                    serviceTypes.LastModificationUserId = LastModificationUserId;
                    serviceTypes.IsDeleted = IsDeleted;

                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Service Type successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> DeleteServicTypeById(int Id)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<ServiceType> servicetypes = (from c in db.ServiceTypes
                                                  where c.ID == Id
                                                  select c).ToList();
                if (servicetypes.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No Service Type to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (ServiceType s in servicetypes)
                    {
                        s.LastModificationDate = DateTime.Now;
                        s.IsDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Service Type successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        //added by abayomi march 22 2021


        public IList<CustomerPlatformTypeModel> GetCustomerPlatformType()
        {

            using (var db = new liblogisticsDataContext())
            {
                var platformdata = (from c in db.CustomerPlatformTypes
                                    where c.IsDeleted == false
                                    select new CustomerPlatformTypeModel
                                    {
                                        ID = c.ID,
                                        PlatformType = c.PlatformType,
                                        CreatorUserId = c.CreatorUserId,
                                        CreationTime = c.CreationTime,
                                        LastModificationDate = c.LastModificationDate,
                                        LastModificationUserId = c.LastModificationUserId,
                                        IsDeleted = c.IsDeleted
                                    }).ToList();
                return platformdata;
            }
        }

        //Adding Customer Platform to db
        public List<returnmsg> AddCustomerPlatformType(CustomerPlatformTypeModel model)
        {
            return AddCustomerPlatformType(model.ID, model.PlatformType, model.CreatorUserId, model.CreationTime, model.LastModificationUserId, model.LastModificationDate, model.IsDeleted);
        }
        public List<returnmsg> AddCustomerPlatformType(int id, string platFormType, string creatorUserId, DateTime creationTime, string LastModificationUserId, DateTime? lastModificationDate, bool IsDeleted)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var platform = from c in db.CustomerPlatformTypes
                               where c.PlatformType == platFormType
                               select c;
                if (platform.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "PlatformType already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    CustomerPlatformType customerPlatformType = new CustomerPlatformType();
                    customerPlatformType.ID = id;
                    customerPlatformType.PlatformType = platFormType;
                    customerPlatformType.CreatorUserId = creatorUserId;
                    customerPlatformType.CreationTime = creationTime;
                    customerPlatformType.LastModificationUserId = LastModificationUserId;
                    customerPlatformType.LastModificationDate = lastModificationDate;
                    customerPlatformType.IsDeleted = IsDeleted;




                    db.CustomerPlatformTypes.InsertOnSubmit(customerPlatformType);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "PlatformType successfully Added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        //Adding Customer Platform to db


        //updating Customer Platform type


        public List<returnmsg> UpdateCustomerPlatformType(CustomerPlatformTypeModel model)
        {
            return UpdateCustomerPlatformType(model.ID, model.PlatformType, model.CreatorUserId, model.CreationTime, model.LastModificationUserId, model.LastModificationDate, model.IsDeleted);
        }
        public List<returnmsg> UpdateCustomerPlatformType(int id, string platFormType, string creatorUserId, DateTime creationTime, string LastModificationUserId, DateTime? lastModificationDate, bool IsDeleted)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {
                liblogisticsDataContext dt = new liblogisticsDataContext();

                CustomerPlatformType customerPlatformType = (from c in dt.CustomerPlatformTypes
                                                             where c.ID == id
                                                             select c).ToList().FirstOrDefault();
                if (customerPlatformType == null)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No platform to update";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    customerPlatformType.PlatformType = platFormType;
                    customerPlatformType.CreatorUserId = creatorUserId;
                    customerPlatformType.CreationTime = creationTime;
                    customerPlatformType.LastModificationDate = lastModificationDate;
                    customerPlatformType.LastModificationUserId = LastModificationUserId;
                    customerPlatformType.IsDeleted = IsDeleted;

                    dt.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Platfrom successfully updated";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }
        //updating Customer Platform type

        //deleting Customer Platform Type

        public List<returnmsg> DeleteCustomerPlatformById(int Id)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<CustomerPlatformType> customerPlatformTypes = (from c in db.CustomerPlatformTypes
                                                                    where c.ID == Id
                                                                    select c).ToList();
                if (customerPlatformTypes.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No Platform to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (CustomerPlatformType c in customerPlatformTypes)
                    {
                        c.LastModificationDate = DateTime.Now;
                        c.IsDeleted = true;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Platform Type successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        //deleting Customer Platform Type
        //GET customerplatform type and Service Type
        public IList<CustomerPlatformTypeModel> GetCustomerPlatformTypes()
        {

            using (var db = new liblogisticsDataContext())
            {
                var platformdata = (from c in db.CustomerPlatformTypes
                                    where c.IsDeleted == false
                                    select new CustomerPlatformTypeModel
                                    {
                                        ID = c.ID,
                                        PlatformType = c.PlatformType,
                                        CreatorUserId = c.CreatorUserId,
                                        CreationTime = c.CreationTime,
                                        LastModificationUserId = c.LastModificationUserId,
                                        LastModificationDate = c.LastModificationDate,
                                        IsDeleted = c.IsDeleted

                                    }).ToList();
                return platformdata;
            }

        }

        public IList<ServiceTypeModel> GetCustomerServiceType()
        {

            var servicetypedata = (from s in db.ServiceTypes
                                   where s.IsDeleted == false
                                   select new ServiceTypeModel
                                   {
                                       ID = s.ID,
                                       ServiceType = s.ServiceType1,
                                       CreatorUserId = s.CreatorUserId,
                                       CreationTime = s.CreationTime,
                                       LastModificationUserId = s.LastModificationUserId,
                                       LastModificationDate = s.LastModificationDate,
                                       IsDeleted = s.IsDeleted

                                   }).ToList();
            return servicetypedata;

        }


        //GET customerplatform type and Service Type
        // Adding Coupons to db

        public List<returnmsg> AddCoupon(CouponModel model)
        {
            return AddCoupon(model.Id, model.CouponCode, model.CouponType, model.CouponValue, model.CreationTime, model.Duration, model.DurationType, model.IsUsed, model.Validity, model.VoucherNote, model.LastModifierUserId, model.CreatorUserId, model.TotalUsage, model.VoucherType, model.CouponValueLimit, model.StartDate, model.EndDate, model.PhoneNumber, model.LastModificationTime, model.DateUsed);
        }
        public List<returnmsg> AddCoupon(Guid id, string couponCode, int coupontype, decimal couponvalue, DateTime creationtime, int duration, int durationType, bool isused, bool validity, string vouchernote, string lastmodifieruserid, string creatoruserid, int? totalusage, int? vouchertype, decimal? couponvaluelimit, DateTime? startdate = null, DateTime? enddate = null, string phonenumber = null, DateTime? lastmodifiedtime = null, DateTime? dateused = null)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var coupons = from c in db.Coupons
                              where c.CouponCode == couponCode
                              select c;
                if (coupons.Count() > 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Coupon already exist";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }

                else
                {
                    Coupon coupon = new Coupon();
                    coupon.Id = id;
                    coupon.CouponCode = couponCode;
                    coupon.CouponType = coupontype;
                    coupon.CouponValue = couponvalue;
                    coupon.CreationTime = creationtime;
                    coupon.PhoneNumber = phonenumber;
                    coupon.LastModificationTime = lastmodifiedtime;
                    coupon.LastModifierUserId = lastmodifieruserid;
                    coupon.CreatorUserId = creatoruserid;
                    coupon.DateUsed = dateused;
                    coupon.Duration = duration;
                    coupon.DurationType = 1;
                    coupon.IsUsed = isused;
                    coupon.Validity = true;
                    coupon.VoucherNote = vouchernote;
                    coupon.VoucherType = (int)vouchertype;
                    coupon.CouponValueLimit = (decimal)couponvaluelimit;
                    coupon.StartDate = startdate;
                    coupon.EndDate = enddate;
                    coupon.TotalUsage = (int)totalusage;


                    db.Coupons.InsertOnSubmit(coupon);
                    db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Coupon successfully Added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception ex)
            {
                ex.InnerException.ToString();
                throw;
            }
        }


        public void AddCouponManagement(CouponManagementModel model)
        {
            var couponManagment = new CouponManagement()
            {
                CouponCode = model.CouponCode,
                CouponUserId = model.CouponUserId,
                Waybill = model.WayBill,
                UsedDate = Convert.ToDateTime(model.UsedDate),
                PlatformType = Convert.ToInt32(model.PlatformType),
                IsDeleted = model.IsDeleted
            };
            db.CouponManagements.InsertOnSubmit(couponManagment);
            db.SubmitChanges();


        }

        public void AddMerchantShipment(MerchantShipmentModel model)
        {
            var merchantShipment = new MerchantShipment()
            {

                MerchantID = model.MerchantID,
                TotalGrandTotal = model.TotalGrandTotal,
                TotalVat = model.TotalVat,
                TotalInsured = model.TotalInsured,
                DateCreated = model.DateCreated,
                IsDeleted = model.IsDeleted
            };
            db.MerchantShipments.InsertOnSubmit(merchantShipment);
            db.SubmitChanges();

        }

        public int GetMerchantId(int MerchantId)
        {
            var merchantID = (from m in db.MerchantSignups
                              where m.id == MerchantId
                              select m.id).FirstOrDefault();
            return merchantID;
        }
        public int GetMerchantShipmentId()
        {
            var merchantShipment = db.MerchantShipments.Select(e => e).OrderByDescending(e => e.DateCreated).First();

            return merchantShipment.ID;
        }
        public IList<CouponModel> GetCoupons()
        {

            using (var db = new liblogisticsDataContext())
            {
                var coupon = (from c in db.Coupons
                              where c.Validity == true
                              select new CouponModel
                              {
                                  Id = c.Id,
                                  CouponCode = c.CouponCode,
                                  CouponType = c.CouponType,
                                  CouponValue = c.CouponValue,
                                  CreationTime = c.CreationTime,
                                  DateUsed = c.DateUsed,
                                  CreatorUserId = c.CreatorUserId,
                                  Duration = c.Duration,
                                  DurationType = c.DurationType,
                                  LastModificationTime = c.LastModificationTime,
                                  LastModifierUserId = c.LastModifierUserId,
                                  PhoneNumber = c.PhoneNumber,
                                  IsUsed = c.IsUsed,
                                  VoucherNote = c.VoucherNote,
                                  VoucherType = c.VoucherType,
                                  CouponValueLimit = c.CouponValueLimit,
                                  // EndDate = Convert.ToDateTime(c.EndDate),
                                  // StartDate =Convert.ToDateTime(c.StartDate),
                                  TotalUsage = c.TotalUsage,
                                  Validity = c.Validity ? true : false

                              }).OrderBy(x => x.CreationTime).ToList();
                return coupon;
            }
        }

        public bool AddTotalUsage(string couponCode)
        {

            var coupons = (from c in _context.Coupons
                           where c.CouponCode == couponCode
                           select c).FirstOrDefault();


            if (coupons == null)
            {
                return false;
            }
            else
            {
                CouponModel coupon = new CouponModel();
                coupon.Id = coupons.Id;
                coupon.CouponCode = coupons.CouponCode;
                coupon.CouponType = coupons.CouponType;
                coupon.CouponValue = coupons.CouponValue;
                coupon.CreationTime = coupons.CreationTime;
                coupon.PhoneNumber = coupons.PhoneNumber;
                coupon.LastModificationTime = DateTime.Now;
                coupon.LastModifierUserId = coupons.LastModifierUserId;
                coupon.CreatorUserId = coupons.CreatorUserId;
                coupon.DateUsed = coupons.DateUsed;
                coupon.Duration = coupons.Duration;
                coupon.DurationType = 1;
                coupon.IsUsed = coupons.IsUsed;
                coupon.Validity = true;
                coupon.VoucherNote = coupons.VoucherNote;
                coupon.VoucherType = (int)coupons.VoucherType;
                coupon.CouponValueLimit = (decimal)coupons.CouponValueLimit;
                coupon.StartDate = coupons.StartDate;
                coupon.EndDate = coupons.EndDate;
                coupon.TotalUsage = coupon.VoucherType > coupons.TotalUsage ? (coupons.TotalUsage + 1) : coupon.VoucherType;
                UpdateCoupon(coupon);
                return true;

            }


        }

        public decimal? GetIndividualPricePerVehicleType(string vehicletype)
        {
            var bikePrice = (from p in _context.PriceCalculators
                             where p.IsDefaultCost == true
                             select p.PriceTdrforBike).FirstOrDefault();
            var vanPrice = (from p in _context.PriceCalculators
                            where p.IsDefaultCost == true
                            select p.PriceTdrforVan).FirstOrDefault();
            var truckPrice = (from p in _context.PriceCalculators
                              where p.IsDefaultCost == true
                              select p.PriceTdrforTruck).FirstOrDefault();
            if (vehicletype == "Bike")
            {

                return bikePrice;
            }
            else if (vehicletype == "Van")
            {

                return vanPrice;
            }
            else if (vehicletype == "Truck")
            {

                return truckPrice;

            }
            else
            {
                return 0.0m;
            }

        }

        public int addAndGetCouponUseage(string couponCode)
        {
            var coupon = (from c in _context.Coupons
                          where c.CouponCode == couponCode
                          select c).FirstOrDefault();
            var couponsCount = (from c in _context.Coupons
                                where c.CouponCode == couponCode
                                select c).Count();
            var couponUseage = (from c in _context.Coupons
                                where c.CouponCode == couponCode
                                select c.TotalUsage);
            if (coupon == null)
            {
                return 0;
            }
            else
            {
                var usage = (int)coupon.TotalUsage;
                if (couponsCount > 0)
                {
                    return usage += 1;
                }
                else
                {
                    return 0;
                }
            }
        }


        public int GetCouponUsage(string couponCode)
        {
            var coupon = (from c in _context.Coupons
                          where c.CouponCode == couponCode
                          select c).FirstOrDefault();
            var coupons = (from c in _context.Coupons
                           where c.CouponCode == couponCode
                           select c).Count();
            var couponUseage = (from c in _context.Coupons
                                where c.CouponCode == couponCode
                                select c.TotalUsage);
            // var usage = (int)coupon.TotalUsage;
            if (coupon == null)
            {
                return 0;
            }
            if (coupons > 0)
            {
                var usage = (int)coupon.TotalUsage;
                var voucher = (int)coupon.VoucherType;
                if (voucher > usage)
                {
                    return usage += 1;
                }
                else
                {
                    return usage;
                }

            }
            else
            {
                var usage = (int)coupon.TotalUsage;
                return usage;

            }

        }

        public bool IsCouponValid(string couponCode)
        {
            var coupon = (from c in _context.Coupons
                          where c.CouponCode == couponCode
                          select c).FirstOrDefault();
            var couponCount = (from c in _context.Coupons
                               where c.CouponCode == couponCode
                               select c).Count();

            if (couponCount > 0)
            {
                var heoll = DateTime.Now.AddHours(-10);
                if (coupon.CouponType == 0 && coupon.Validity == true && DateTime.Now < coupon.EndDate && coupon.IsUsed == false)
                {
                    //coupon.StartDate <= DateTime.Now.AddHours(-10)
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool UpdateCouponUsedIs(string couponCode)
        {

            Coupon shpmt = new Coupon();
            try
            {
                var coupon = (from c in _context.Coupons
                              where c.CouponCode == couponCode
                              select c).FirstOrDefault();
                if (coupon != null)
                {

                    coupon.IsUsed = true;
                    // db.SubmitChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public decimal GetCouponValueQuickFix(string couponCode)
        {
            var coupon = (from c in _context.Coupons
                          where c.CouponCode == couponCode
                          select c).FirstOrDefault();
            var coupons = (from c in _context.Coupons
                           where c.CouponCode == couponCode
                           select c).Count();

            if (coupons > 0)
            {

                return coupon.CouponValue;
            }
            else
            {
                return 0.0M;
            }

        }
        public decimal GetCouponValue(string couponCode)
        {
            var coupon = (from c in _context.Coupons
                          where c.CouponCode == couponCode
                          select c).FirstOrDefault();
            var coupons = (from c in _context.Coupons
                           where c.CouponCode == couponCode
                           select c).Count();

            if (coupons > 0)
            {



                if (coupon.VoucherType == 1 && (coupon.VoucherType > coupon.TotalUsage) && (DateTime.Now <= coupon.EndDate))//0
                {
                    return coupon.CouponValue;
                }
                else if (coupon.VoucherType == 2 && (coupon.VoucherType > coupon.TotalUsage) && (DateTime.Now >= coupon.StartDate && DateTime.Now <= coupon.EndDate))//1
                {
                    return coupon.CouponValue;
                }
                else if (coupon.VoucherType == 3 && (coupon.VoucherType > coupon.TotalUsage) && (DateTime.Now >= coupon.StartDate && DateTime.Now <= coupon.EndDate))//2
                {
                    return coupon.CouponValue;
                }
                else if (coupon.VoucherType == 4 && (coupon.VoucherType > coupon.TotalUsage) && (DateTime.Now >= coupon.StartDate && DateTime.Now <= coupon.EndDate))//3
                {
                    return coupon.CouponValue;
                }
                else if (coupon.VoucherType == 5 && (coupon.VoucherType > coupon.TotalUsage) && (DateTime.Now >= coupon.StartDate && DateTime.Now <= coupon.EndDate))//4
                {
                    return coupon.CouponValue;
                }
                else if (coupon.VoucherType == 6 && (coupon.VoucherType > coupon.TotalUsage) && (DateTime.Now >= coupon.StartDate && DateTime.Now <= coupon.EndDate))//5
                {
                    return coupon.CouponValue;
                }
                else if (coupon.VoucherType == 7 && (coupon.VoucherType > coupon.TotalUsage) && (DateTime.Now >= coupon.StartDate && DateTime.Now <= coupon.EndDate))//6
                {
                    return coupon.CouponValue;
                }
                else
                {
                    return 0.0M;
                }

            }
            else
            {
                return 0.0M;
            }



            //using (var db = new liblogisticsDataContext())
            //{
            //    var coupons = (from c in db.Coupons
            //                  where c.CouponCode == coupon
            //                  select c.CouponValue).FirstOrDefault();
            //    return coupons;
            //    if (coupons)
            //    {

            //    }
            //}


        }
        public List<returnmsg> deleteCouponById(Guid couponId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<Coupon> coupons = (from c in _context.Coupons
                                        where c.Id == couponId
                                        select c).ToList();
                if (coupons.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No coupon to delete";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (Coupon c in coupons)
                    {
                        c.LastModificationTime = DateTime.Now;
                        c.Validity = false;
                        c.VoucherType = c.VoucherType;
                        c.CouponValueLimit = c.CouponValueLimit;
                        c.TotalUsage = c.TotalUsage;
                        c.CouponType = c.CouponType;
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Coupon successfully deleted";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }



        public int GetWeightRangeId(decimal weightRange)
        {

            int weightRangeId = (from w in _context.WeightRanges
                                 where weightRange >= w.LowerRange && weightRange <= w.UpperRange
                                 select w.Id).FirstOrDefault();



            List<int> listOfRangeId = (from w in _context.WeightRanges select w.Id).ToList();

            int maxDefaultId = listOfRangeId.Max();

            if (weightRangeId == 0)
            {
                return maxDefaultId;
            }
            else
            {
                return weightRangeId;
            }

        }

        public decimal GetWeightRangePricePerMerchantIntraState(decimal weightRange, int merchantId)
        {
            int weightRangeId = (from w in _context.WeightRanges
                                 where weightRange >= w.LowerRange && weightRange <= w.UpperRange
                                 select w.Id).FirstOrDefault();


            // List<decimal?> listOfUpperRange = (from w in db.WeightRanges select w.UpperRange).ToList();
            List<decimal?> listOfLowerRange = (from w in _context.WeightRanges select w.LowerRange).ToList();

            // decimal? maxUpperRange = listOfUpperRange.Max();
            decimal? maxLowerRange = listOfLowerRange.Max();

            List<int> listOfRangeId = (from w in _context.WeightRanges select w.Id).ToList();

            int maxDefaultId = listOfRangeId.Max();

            if (weightRange >= maxLowerRange)
            {
                decimal weightPrice = GetWeightRangePricePerMerchant(maxDefaultId, merchantId);

                decimal custweigthPrice = weightPrice * weightRange;
                return custweigthPrice;
            }
            else
            {
                decimal weightPrice = GetWeightRangePricePerMerchant(weightRangeId, merchantId);
                return weightPrice;

            }

        }

        public decimal GetWeightRangePricePerMerchantInterState(decimal weightRange, int merchantId)
        {
            int weightRangeId = (from w in _context.WeightRanges
                                 where weightRange >= w.LowerRange && weightRange <= w.UpperRange
                                 select w.Id).FirstOrDefault();

            List<decimal?> listOfLowerRange = (from w in _context.WeightRanges select w.LowerRange).ToList();
            decimal? maxLowerRange = listOfLowerRange.Max();
            List<int> listOfRangeId = (from w in _context.WeightRanges select w.Id).ToList();

            int maxDefaultId = listOfRangeId.Max();

            if (weightRange >= maxLowerRange)
            {
                decimal weightPrice = GetWeightRangePricePerMerchantInterState(maxDefaultId, merchantId);
                decimal custweigthPrice = weightPrice * weightRange;
                return custweigthPrice;
            }
            else
            {
                decimal weightPrice = GetWeightRangePricePerMerchantInterState(weightRangeId, merchantId);
                return weightPrice;

            }

        }

        public decimal GetWeightRangePricePerMerchant(int weightrangeId, int merchantId)
        {
            decimal priceForMerchant = (from p in _context.MerchantWeightRangePrices
                                        where p.MerchantId == merchantId &&
                                        p.WeightRangeId == weightrangeId
                                        select p.WeightPrice).FirstOrDefault();



            return priceForMerchant;
        }


        public decimal GetWeightRangePricePerMerchantInterState(int weightrangeId, int merchantId)
        {
            decimal priceForMerchant = (from p in _context.MerchantWeightRangePrices
                                        where p.MerchantId == merchantId &&
                                        p.WeightRangeId == weightrangeId
                                        select p.WeightPriceInterState).FirstOrDefault();



            return priceForMerchant;
        }

        public string GetUserId(string email)
        {
            string userId = (from a in _context.AspNetUsers
                             where a.Email == email
                             select a.Id).FirstOrDefault();
            return userId;
        }

        public string GetUserEmail(string userId)
        {
            string userEmail = (from a in _context.AspNetUsers
                                where a.Id == userId
                                select a.Email).FirstOrDefault();
            return userEmail;
        }

        public string GetMerchantEmail(int merchantId)
        {
            string merchantEmail = (from m in _context.MerchantSignups
                                    where m.id == merchantId
                                    select m.emailladdress).FirstOrDefault();
            return merchantEmail;
        }
        public bool GetTerminalDetailVerified(int walletID)
        {
            var isVerified = (from w in _context.WalletTerminals
                              where w.ID == walletID && w.IsVerfied == true
                              select w.IsVerfied).FirstOrDefault();
            return isVerified;
        }

        public bool GetTerminalDetailApproved(int walletID)
        {
            var isApproved = (from w in _context.WalletTerminals
                              where w.ID == walletID && w.IsApproved == true
                              select w.IsVerfied).FirstOrDefault();
            return isApproved;
        }

        public List<returnmsg> TerminalWalletTopUp(int merchantId, decimal amount, string paymentMethod, string creatorUserId, string posReference, string transferName, string transferDate)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            var merchantEmail = GetMerchantEmail(merchantId);
            var userId = GetUserId(merchantEmail);

            try
            {

                WalletTerminal wt = new WalletTerminal();
                wt.TransactionAmount = amount;
                wt.PaymentMethod = paymentMethod;
                wt.POSReferenceNO = posReference != null ? posReference : null;
                wt.TransferName = transferName != null ? transferName : null;
                wt.TransferDate = transferDate != null ? transferDate : null;
                wt.CreatorUserId = creatorUserId;
                wt.UserId = userId;
                wt.CreationTime = DateTime.UtcNow.AddHours(1);
                //db.WalletTerminals.InsertOnSubmit(wt);
                //db.SubmitChanges();
                rtmsg.code = "Success";
                rtmsg.completed = true;
                rtmsg.successmsg = "Entry successfully added";
                retmsgs.Add(rtmsg);
                return retmsgs;
            }
            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }

        public IList<WalletTerminalModel> GetWalletTerminal()
        {


            var libdata = (from w in _context.WalletTerminals
                           join a in _context.AspNetUsers on w.UserId equals a.Id
                           join m in _context.MerchantSignups on a.Email equals m.emailladdress
                           where w.IsDeleted == false
                           select new WalletTerminalModel
                           {
                               id = w.ID,
                               Name = m.businessname,
                               Amount = w.TransactionAmount,
                               PaymentMethod = w.PaymentMethod,
                               DateCreated = w.CreationTime,
                           }).ToList();
            return libdata;

        }

        public List<returnmsg> VerifyWalletTerminal(int itemId, string verifyUserId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<WalletTerminal> itemDataModel = (from w in db.WalletTerminals
                                                      where w.ID == itemId && w.IsVerfied == false && w.TransactionAmount != 0
                                                      select w).ToList();

                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to verify";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (WalletTerminal l in itemDataModel)
                    {
                        l.IsVerfiedByUserId = verifyUserId.ToString();
                        l.IsVerfied = true;
                        var usernameoremail = GetUserEmail(l.UserId);

                        GenerateWalletTerminalTransactionReference(TransactionType.Credit, l.UserId, usernameoremail, l.TransactionAmount);
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully verified";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<returnmsg> ApproveTerminalWallet(int itemId, string approveUserId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                List<WalletTerminal> itemDataModel = (from w in db.WalletTerminals
                                                      where w.ID == itemId && w.IsVerfied == true && w.IsApproved == false
                                                      select w).ToList();
                if (itemDataModel.Count() == 0)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "No data to approve";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {
                    foreach (WalletTerminal l in itemDataModel)
                    {
                        l.IsApprovedByUserId = approveUserId;
                        l.IsApproved = true;

                        FundTerminalWallet(l.UserId, GetUserEmail(l.UserId), l.TransactionAmount);
                    }
                    db.SubmitChanges();
                    rtmsg.completed = true;
                    rtmsg.code = "Success";
                    rtmsg.successmsg = "Data successfully approved";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public List<returnmsg> GenerateWalletTerminalTransactionReference(TransactionType transactionType, string userId, string usernameoremail, decimal TransactionAmount)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            var wallet = GetWalletDetails(userId);
            var refnum = RandomDigits(7);
            var referenceNumber = refnum + "WTF";

            //if it is credit
            try
            {
                if (transactionType == TransactionType.Debit)
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "Use WayBill Number as Reference";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
                else
                {


                    WalletTransaction wt = new WalletTransaction();
                    wt.Id = GenerateComb();
                    wt.CreatedBy = usernameoremail;
                    wt.CreationTime = DateTime.UtcNow.AddHours(1);
                    wt.IsDeleted = false;
                    wt.LineBalance = wallet.Balance + TransactionAmount;
                    wt.WalletId = wallet.Id;
                    wt.UserId = userId;
                    wt.TransactionType = transactionType.ToString();
                    wt.TransactionDate = DateTime.UtcNow.AddHours(1);
                    wt.TransactionAmount = TransactionAmount;
                    wt.TransactedBy = usernameoremail;
                    wt.TransactionDescription = "Payment Into Wallet";
                    wt.IsCompleted = false;
                    wt.Reference = referenceNumber;
                    //db.WalletTransactions.InsertOnSubmit(wt);
                    //db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }

            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }

        public List<returnmsg> FundTerminalWallet(string userId, string usernameoremail, decimal TransactionAmount)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();

            var wallet = GetWalletDetails(userId);

            try
            {
                if (wallet != null)
                {
                    var walet = _context.Wallets.Where(c => c.UserId == userId).FirstOrDefault();
                    var newBalance = walet.Balance + TransactionAmount;
                    walet.Balance = newBalance;
                    //  db.SubmitChanges();
                    rtmsg.code = "Success";
                    rtmsg.completed = true;
                    rtmsg.successmsg = "Entry successfully added";
                    retmsgs.Add(rtmsg);
                    return retmsgs;

                    #region to conform with requirement, add a point for the user transaction discount if the amount is more than #5,000 (hardcode alert)
                    //add transaction amount to discount
                    var transactionPoint = (int)(TransactionAmount / 5000);
                    var user = (from a in _context.AspNetUsers where a.Id == Convert.ToInt32(userId) select a).FirstOrDefault();
                    if (user != null)
                    {
                        user.TransactionalDiscount += transactionPoint;
                        // db.SubmitChanges();
                    }
                    #endregion
                }
                else
                {
                    rtmsg.completed = false;
                    rtmsg.code = "Error";
                    rtmsg.errormsg = "null";
                    retmsgs.Add(rtmsg);
                    return retmsgs;
                }
            }

            catch (Exception ex)
            {
                rtmsg.code = "Error";
                rtmsg.errormsg = ex.Message.ToString();
                rtmsg.completed = false;
                retmsgs.Add(rtmsg);
                ex.InnerException.ToString();
                throw;
            }
        }

        public string GetTerminalWalletBalance(string userId)
        {
            try
            {
                if (userId == null) { return null; }
                var walletBalance = (from r in db.Wallets where r.UserId == userId select r.Balance).FirstOrDefault();
                if (walletBalance == null) { return null; }
                return Convert.ToString(walletBalance);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public string MassUpdateWaybillStatus(string journeyCode, string userLocationName, string selectedStatus)
        {
            try
            {
                if (journeyCode == null) { return null; }
                var dtx = (from JM in _context.JourneyManagements
                           join MF in _context.Manifests on JM.Id equals MF.JourneyManagementId
                           join MP in _context.ManifestMappings on MF.ManifestId equals MP.ManifestId
                           join GWN in _context.GroupWayBillNumbers on MP.GroupWaybillNumber equals GWN.GroupWaybillCode
                           join GWNP in _context.GroupWaybillNumMappings on GWN.GroupWaybillCode equals GWNP.GroupWaybillNumber
                           where JM.JourneyCode == journeyCode
                           select GWNP.WaybillNumber);

                foreach (string WA in dtx)
                {
                    UpdateshipmentStatus(WA, userLocationName, selectedStatus + " => " + userLocationName);
                }

                return "";
            }
            catch (Exception)
            {
                return null;
            }
        }


        public string MassUpdateWaybillStatus_(string journeyCode, string userLocationName)
        {
            try
            {
                if (journeyCode == null) { return null; }

                var dtx = (from JM in _context.JourneyManagements
                           join MF in _context.Manifests on JM.Id equals MF.JourneyManagementId
                           join MP in _context.ManifestMappings on MF.ManifestId equals MP.ManifestId
                           join GWN in _context.GroupWayBillNumbers on MP.GroupWaybillNumber equals GWN.GroupWaybillCode
                           join GWNP in _context.GroupWaybillNumMappings on GWN.GroupWaybillCode equals GWNP.GroupWaybillNumber
                           where JM.JourneyCode == journeyCode
                           select GWNP.WaybillNumber);

                foreach (string WA in dtx)
                {
                    UpdateshipmentStatus(WA, userLocationName, LibmotExpressConstants.ShipmentTrackingStatus.InTransit);
                }

                return "";
            }
            catch (Exception)
            {
                return null;
            }
        }
        //THIS METHOD UPDATES A SINGLE WAYYBILL
        public string MassUpdateWaybillStatus2(string journeyCode, string userLocationName, string selectedStatus)
        {
            try
            {
                if (journeyCode == null) { return null; }

                var dtx = (from JM in _context.JourneyManagements
                           join MF in _context.Manifests on JM.Id equals MF.JourneyManagementId
                           join MP in _context.ManifestMappings on MF.ManifestId equals MP.ManifestId
                           join GWN in _context.GroupWayBillNumbers on MP.GroupWaybillNumber equals GWN.GroupWaybillCode
                           join GWNP in _context.GroupWaybillNumMappings on GWN.GroupWaybillCode equals GWNP.GroupWaybillNumber
                           where JM.JourneyCode == journeyCode
                           select GWNP.WaybillNumber);

                foreach (string WA in dtx)
                {
                    var trackingData = (from t in _context.ShipmentTrackings where t.Waybill == WA select t).FirstOrDefault();
                    trackingData.Status = selectedStatus;
                    trackingData.DateModified = DateTime.Now;
                    trackingData.Location = userLocationName;
                    // db.SubmitChanges();
                    //var departureLocationId = db.Locations.Where(l => l.id == d.departureLocationId).FirstOrDefault()?.locationName;
                    //UpdateshipmentStatus(WA, userLocationName, selectedStatus + " " + userLocationName);


                }

                return "";
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList<ManifestModel> GetJourneyManifest(int? JourneyId)
        {


            var ManifestData = (from m in _context.Manifests
                                where m.JourneyManagementId == JourneyId
                                select new ManifestModel
                                {
                                    ManifestId = m.ManifestId,
                                    ManifestNumber = m.ManifestNumber
                                }).OrderBy(n => n.ManifestNumber).ToList();
            return ManifestData;
        }

        public List<string> GetManifestNumberWithJourneyCode(string journeyCode)
        {

            var manifestNumbers = (from j in _context.JourneyManagements
                                   join m in _context.Manifests on j.Id equals m.JourneyManagementId
                                   where j.JourneyCode == journeyCode
                                   select m.ManifestNumber).ToList();

            return manifestNumbers;

        }

        public List<string> GetGroupWayBillNumbers(string manifestNumber)
        {

            var groupWayBillNumbers = (from mm in _context.ManifestMappings
                                       join m in _context.Manifests on mm.ManifestId equals m.ManifestId
                                       where m.ManifestNumber == manifestNumber
                                       select mm.GroupWaybillNumber).ToList();

            return groupWayBillNumbers;
        }

        public List<string> GetWayBillNumbers(string groupWayBillNo)
        {
            var wayBillNumbers = (from w in _context.GroupWaybillNumMappings
                                  where w.GroupWaybillNumber == groupWayBillNo
                                  select w.WaybillNumber).ToList();

            return wayBillNumbers;
        }

        public List<CustomerPhoneNumbersAndWayBill> GetPhoneNumbersWithWayBillNo(string WayBillNo)
        {

            var phoneNumbersAndWaybill = (from s in _co.shipments
                                          where s.Waybill == WayBillNo
                                          select new CustomerPhoneNumbersAndWayBill
                                          {
                                              SenderPhoneNumber = s.SenderPhoneNumber,
                                              ReceiverPhoneNumber = s.receiverPhoneNumber,
                                              WayBillNumber = s.Waybill
                                          }).ToList();

            return phoneNumbersAndWaybill;
        }

        //    public List<returnmsg> deleteWalletById(int itemId)
        //    {
        //        List<returnmsg> retmsgs = new List<returnmsg>();
        //        var rtmsg = new returnmsg();
        //        try
        //        {

        //            List<WalletTerminal> itemDataModel = (from p in db.WalletTerminals
        //                                                  where p.ID == itemId
        //                                                  select p).ToList();
        //            if (itemDataModel.Count() == 0)
        //            {
        //                rtmsg.completed = false;
        //                rtmsg.code = "Error";
        //                rtmsg.errormsg = "No data to delete";
        //                retmsgs.Add(rtmsg);
        //                return retmsgs;
        //            }
        //            else
        //            {
        //                foreach (WalletTerminal l in itemDataModel)
        //                {
        //                    l.IsApproved = true;
        //                    l.dat
        //                    l.IsDeleted = false;
        //                }
        //                db.SubmitChanges();
        //                rtmsg.completed = true;
        //                rtmsg.code = "Success";
        //                rtmsg.successmsg = "Data successfully deleted";
        //                retmsgs.Add(rtmsg);
        //                return retmsgs;
        //            }
        //        }
        //        catch (Exception)
        //        {

        //            throw;
        //        }
        //    }
        //}

        public List<returnmsg> deleteWalletById(int itemId)
        {
            List<returnmsg> retmsgs = new List<returnmsg>();
            var rtmsg = new returnmsg();
            try
            {

                var x = (from y in _context.WalletTerminals
                         where y.ID == itemId
                         select y).FirstOrDefault();
                ctx.WalletTerminals.DeleteOnSubmit(x);
                ctx.SubmitChanges();
                rtmsg.completed = true;
                rtmsg.code = "Success";
                rtmsg.successmsg = "Data successfully deleted";
                retmsgs.Add(rtmsg);
                return retmsgs;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }

}

