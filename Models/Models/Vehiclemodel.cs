namespace Models.Models
{
    public partial class VehicleModel
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Name { get; set; }
        public int NumberOfSeats { get; set; }
        public string? VehicleModelTypeCode { get; set; }
        public int VehicleMakeId { get; set; }
    }
}
