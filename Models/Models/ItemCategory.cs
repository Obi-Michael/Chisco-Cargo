namespace Models.Models
{
    public partial class ItemCategory
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public long? CreatorUserId { get; set; }
        public DateTime? LastModificationTime { get; set; }
        public long? LastModifierUserId { get; set; }
        public bool IsDeleted { get; set; }
        public long? DeleterUserId { get; set; }
        public DateTime? DeletionTime { get; set; }
        public string? ItemCategoryCode { get; set; }
        public int ItemFamilyId { get; set; }
        public string? CategoryName { get; set; }
        public string? CategoryDescription { get; set; }
        public string? CategoryLongDescription { get; set; }
        public string? CategoryPictureUrl { get; set; }
        public int CompanyId { get; set; }
    }
}
