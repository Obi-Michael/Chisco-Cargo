namespace Models.Models
{
    public partial class DiscountTransaction
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public int? DiscountPoint { get; set; }
        public DateTime? DateCreated { get; set; }
    }
}
