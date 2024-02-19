namespace Models.Models
{
    public partial class Terminal
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
        public string? Code { get; set; }
        public string? Image { get; set; }
        public string? Address { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPersonNo { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public DateTime TerminalStartDate { get; set; }
        public int TerminalType { get; set; }
        public int StateId { get; set; }
        public bool IsNew { get; set; }
        public string? TerminalCode { get; set; }
        public bool IsCommision { get; set; }
        public bool IsOnlineDiscount { get; set; }
        public decimal OnlineDiscount { get; set; }
        public int? CompanyId { get; set; }
        public string? MappedId { get; set; }
        public string? CreatorUsername { get; set; }
        public string? LastModifierUsername { get; set; }
    }
}
