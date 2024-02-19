namespace Models.Models
{
    public partial class VehiclePart
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
        public int CheckThreshold { get; set; }
        public int RefillThreshold { get; set; }
        public int HubCheckThreshold { get; set; }
        public int HubRefillThreshold { get; set; }
        public int CentralCheckThreshold { get; set; }
        public int CentralRefillThreshold { get; set; }
        public int VehicleModelId { get; set; }
    }
}
