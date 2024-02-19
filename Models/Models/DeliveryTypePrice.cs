namespace Models.Models
{
    public partial class DeliveryTypePrice
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int DeliveryTypeId { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual Deliverytype DeliveryType { get; set; } = null!;
        public virtual Zone Zone { get; set; } = null!;
    }
}
