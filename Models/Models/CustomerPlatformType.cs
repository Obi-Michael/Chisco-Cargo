namespace Models.Models
{
    public partial class CustomerPlatformType
    {
        public int Id { get; set; }
        public string PlatformType { get; set; } = null!;
        public string? CreatorUserId { get; set; }
        public DateTime CreationTime { get; set; }
        public string? LastModificationUserId { get; set; }
        public DateTime? LastModificationDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
