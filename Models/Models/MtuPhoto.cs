namespace Models.Models
{
    public partial class MtuPhoto
    {
        public int Id { get; set; }
        public string? FileName { get; set; }
        public int MtuReportModelId { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
    }
}
