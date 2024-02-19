namespace Models.Models
{
    public partial class Vehiclemodel1
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int MakeId { get; set; }
        public int? VehicleType { get; set; }

        public virtual VehicleMake Make { get; set; } = null!;
    }
}
