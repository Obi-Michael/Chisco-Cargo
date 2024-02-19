namespace Models.Models
{
    public partial class ServiceType
    {
        public int Id { get; set; }
        public string ServiceType1 { get; set; } = null!;
        public string? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string? LastModificationUserId { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
