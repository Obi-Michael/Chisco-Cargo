namespace Models.Models
{
    public partial class Workshop
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public int VehicleStatus { get; set; }
        public int VehicleId { get; set; }
        public int VehicleLocationId { get; set; }
        public int WorkshopLocationId { get; set; }
        public bool WorkshopStatus { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? ReleaseUserId { get; set; }
        public string? WorkshopNote { get; set; }
        public string? ReleaseNote { get; set; }
        public int? ReleaseLocationId { get; set; }
    }
}
