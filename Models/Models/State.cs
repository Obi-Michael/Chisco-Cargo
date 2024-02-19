namespace Models.Models
{
    public partial class State
    {
        public State()
        {
            Locations = new HashSet<Location>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public int RegionId { get; set; }
        public string? MappedId { get; set; }
        public bool? Instate { get; set; }

        public virtual Region Region { get; set; } = null!;
        public virtual ICollection<Location> Locations { get; set; }
    }
}
