namespace Models.Models
{
    public partial class VehiclePartPosition
    {
        public Guid Id { get; set; }
        public string? Position { get; set; }
        public int VehiclePartId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
