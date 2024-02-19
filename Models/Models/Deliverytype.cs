namespace Models.Models
{
    public partial class Deliverytype
    {
        public Deliverytype()
        {
            DeliveryTypePrices = new HashSet<DeliveryTypePrice>();
            Shipments = new HashSet<Shipment>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public bool? IsActive { get; set; }
        public int CustomerType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual CustomerType CustomerTypeNavigation { get; set; } = null!;
        public virtual ICollection<DeliveryTypePrice> DeliveryTypePrices { get; set; }
        public virtual ICollection<Shipment> Shipments { get; set; }
    }
}
