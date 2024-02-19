namespace Models.Models
{
    public partial class ItemCondition
    {
        public int Id { get; set; }
        public string? ItemCondition1 { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
