namespace Models.Models
{
    public partial class CustomerType
    {
        public CustomerType()
        {
            Deliverytypes = new HashSet<Deliverytype>();
        }

        public int Id { get; set; }
        public string? CusType { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public virtual ICollection<Deliverytype> Deliverytypes { get; set; }
    }
}
