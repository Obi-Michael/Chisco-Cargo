namespace Models.Models
{
    public partial class LocationHub
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public int HubId { get; set; }

        public virtual Location Hub { get; set; } = null!;
        public virtual Location Location { get; set; } = null!;
    }
}
