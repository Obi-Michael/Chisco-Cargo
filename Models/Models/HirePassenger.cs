namespace Models.Models
{
    public partial class HirePassenger
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
        public string? PhoneNo { get; set; }
        public string? NokName { get; set; }
        public string? NokPhone { get; set; }
        public Guid HireBusId { get; set; }
    }
}
