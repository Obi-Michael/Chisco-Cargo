namespace Models.Models
{
    public partial class SpecialPackage
    {
        public SpecialPackage()
        {
            SpecialPackagepricings = new HashSet<SpecialPackagepricing>();
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal ItemWeight { get; set; }
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<SpecialPackagepricing> SpecialPackagepricings { get; set; }
    }
}
