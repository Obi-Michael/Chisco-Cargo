namespace Models.Models
{
    public partial class ServiceTypePrice
    {
        public int Id { get; set; }
        public int ZoneId { get; set; }
        public int ServiceTypeId { get; set; }
        public decimal Price { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
