namespace Models.Models
{
    public partial class FleetAsset
    {
        public int Id { get; set; }
        public string RegNumber { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
