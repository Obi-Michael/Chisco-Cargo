namespace Models.Models
{
    public partial class VehicleMake
    {
        public VehicleMake()
        {
            Vehiclemodel1s = new HashSet<Vehiclemodel1>();
        }

        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? Name { get; set; }

        public virtual ICollection<Vehiclemodel1> Vehiclemodel1s { get; set; }
    }
}
