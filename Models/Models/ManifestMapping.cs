namespace Models.Models
{
    public partial class ManifestMapping
    {
        public int ManifestMappingId { get; set; }
        public int? ManifestId { get; set; }
        public string GroupWaybillNumber { get; set; } = null!;
        public bool? IsActive { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
