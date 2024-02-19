namespace Models.Models
{
    public partial class PassportType
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int RouteId { get; set; }
        public decimal AddOnFare { get; set; }
    }
}
