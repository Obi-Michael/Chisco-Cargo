namespace Models.Models
{
    public partial class WayBill
    {
        public int Int { get; set; }
        public string WayBillCode { get; set; } = null!;
        public string Createdby { get; set; } = null!;
        public int LocationId { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public bool IsDeleted { get; set; }
    }
}
