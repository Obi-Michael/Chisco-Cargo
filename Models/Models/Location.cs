namespace Models.Models
{
    public partial class Location
    {
        public Location()
        {
            Dispatches = new HashSet<Dispatch>();
            GroupWaybillNumMappingDepartures = new HashSet<GroupWaybillNumMapping>();
            GroupWaybillNumMappingDestinations = new HashSet<GroupWaybillNumMapping>();
            LocationHubHubs = new HashSet<LocationHub>();
            LocationHubLocations = new HashSet<LocationHub>();
            Shipments = new HashSet<Shipment>();
            ZoneMappingDepartutes = new HashSet<ZoneMapping>();
            ZoneMappingDestinations = new HashSet<ZoneMapping>();
        }

        public int Id { get; set; }
        public string LocationName { get; set; } = null!;
        public int StateId { get; set; }
        public int LocationType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
        public string? Address { get; set; }
        public string? Image { get; set; }
        public string? Code { get; set; }
        public float? Latitude { get; set; }
        public float? Longitude { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonNo { get; set; }
        public bool? IsCommision { get; set; }

        public virtual State State { get; set; } = null!;
        public virtual ICollection<Dispatch> Dispatches { get; set; }
        public virtual ICollection<GroupWaybillNumMapping> GroupWaybillNumMappingDepartures { get; set; }
        public virtual ICollection<GroupWaybillNumMapping> GroupWaybillNumMappingDestinations { get; set; }
        public virtual ICollection<LocationHub> LocationHubHubs { get; set; }
        public virtual ICollection<LocationHub> LocationHubLocations { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
        public virtual ICollection<ZoneMapping> ZoneMappingDepartutes { get; set; }
        public virtual ICollection<ZoneMapping> ZoneMappingDestinations { get; set; }
    }
}
